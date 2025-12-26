#!/usr/bin/env pwsh
# Diagnóstico del backend

Write-Host "🔍 Iniciando diagnóstico del backend..." -ForegroundColor Cyan

# 1. Verificar ambiente
Write-Host "`n✅ Variables de ambiente:" -ForegroundColor Green
$env:ASPNETCORE_ENVIRONMENT
$env:ASPNETCORE_URLS

# 2. Verificar puerto disponible
Write-Host "`n✅ Verificando puertos:" -ForegroundColor Green
Test-NetConnection localhost -Port 5001 -WarningAction SilentlyContinue | Select-Object TcpTestSucceeded
Test-NetConnection localhost -Port 7001 -WarningAction SilentlyContinue | Select-Object TcpTestSucceeded

# 3. Limpiar puertos
Write-Host "`n✅ Limpiando procesos anteriores:" -ForegroundColor Green
Get-Process dotnet -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

# 4. Ejecutar backend con logging completo
Write-Host "`n✅ Ejecutando backend..." -ForegroundColor Green
cd "d:\JEREMIAS\programacion\Repo\sistemas_turnos_nuevo\Back-end"
$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ASPNETCORE_URLS = "http://localhost:5001;https://localhost:7001"

# Ejecutar y capturar TODOSALIDA
& dotnet run 2>&1 | Tee-Object -FilePath "C:\temp\backend-diagnostics.log"
