# Verificar y crear usuario en la base de datos
param([switch]$force)

$connectionString = "Server=localhost,1433;Database=Sistemas_Turnos;User Id=sa;Password=SqlPassword2025!;TrustServerCertificate=True;"

try {
    [System.Data.SqlClient.SqlConnection]$conn = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $conn.Open()
    Write-Host "✅ Conectado a SQL Server" -ForegroundColor Green
    
    # Verificar usuario
    $checkQuery = "SELECT Id_Usuarios, Email, Nombre_Completo FROM usuarios WHERE Email = 'test@clinica.com'"
    $cmd = New-Object System.Data.SqlClient.SqlCommand($checkQuery, $conn)
    $reader = $cmd.ExecuteReader()
    
    if ($reader.HasRows) {
        $reader.Read()
        Write-Host "✅ Usuario existe:" -ForegroundColor Green
        Write-Host "   ID: $($reader['Id_Usuarios'])"
        Write-Host "   Email: $($reader['Email'])"
        Write-Host "   Nombre: $($reader['Nombre_Completo'])"
    } else {
        Write-Host "❌ Usuario no existe. Creando..." -ForegroundColor Yellow
        $reader.Close()
        
        # Hash SHA256 de "Test123!"
        $passwordHash = "/V2Eb/D0vBDe4M2x1j6Bq7X3vYvG5+s1eHhQ2m5wJ9I="
        
        $insertQuery = "INSERT INTO usuarios (Nombre_Completo, Email, Password_Hash, Rol) VALUES ('Test Usuario', 'test@clinica.com', '$passwordHash', 'cliente')"
        
        $insertCmd = New-Object System.Data.SqlClient.SqlCommand($insertQuery, $conn)
        $insertCmd.ExecuteNonQuery()
        
        Write-Host "✅ Usuario creado" -ForegroundColor Green
    }
    
    $conn.Close()
}
catch {
    Write-Host "❌ Error: $_" -ForegroundColor Red
}
