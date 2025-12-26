$env:ASPNETCORE_ENVIRONMENT = "Development"
$env:ASPNETCORE_URLS = "http://localhost:5001;https://localhost:7001"

cd "d:\JEREMIAS\programacion\Repo\sistemas_turnos_nuevo\Back-end"
try {
    dotnet run --no-launch-profile 2>&1
}
catch {
    Write-Error "Error: $_"
}
