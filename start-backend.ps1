$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ASPNETCORE_URLS = "http://localhost:5001;https://localhost:7001"

Push-Location "d:\JEREMIAS\programacion\Repo\sistemas_turnos_nuevo\Back-end"

Write-Host "✅ Backend iniciando..." -ForegroundColor Green
Write-Host "   HTTP:  http://localhost:5001" -ForegroundColor Cyan
Write-Host "   HTTPS: https://localhost:7001" -ForegroundColor Cyan
Write-Host ""

dotnet run --no-launch-profile 2>&1 | ForEach-Object {
    Write-Host $_
}
