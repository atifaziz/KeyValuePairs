@echo off
pushd "%~dp0"
call :main %*
popd
exit /b %ERRORLEVEL%

:main
    dotnet restore ^
 && dotnet build --no-restore -c Debug ^
 && dotnet build --no-restore -c Release
exit /b %ERRORLEVEL%
