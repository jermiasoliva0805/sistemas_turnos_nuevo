$body = @{
    nombreCompleto = "Test User"
    email = "test@example.com"
    password = "Test123456"
} | ConvertTo-Json

Write-Host "Enviando solicitud de registro..."
Write-Host "Email: test@example.com"
Write-Host "Contraseña: Test123456"

try {
    $response = Invoke-WebRequest -Uri "http://localhost:5001/api/auth/register" `
        -Method Post `
        -ContentType "application/json" `
        -Body $body -ErrorAction Stop
    
    Write-Host "✅ Usuario creado exitosamente!" -ForegroundColor Green
    Write-Host $response.Content
} catch {
    Write-Host "❌ Error: $_" -ForegroundColor Red
}
