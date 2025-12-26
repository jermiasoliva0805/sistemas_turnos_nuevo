@echo off
cd /d "d:\JEREMIAS\programacion\Repo\sistemas_turnos_nuevo\Back-end"
set ASPNETCORE_ENVIRONMENT=Development
set ASPNETCORE_URLS=http://localhost:5001;https://localhost:7001
echo Iniciando backend...
dotnet run --no-launch-profile
