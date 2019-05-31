@echo off
pushd "%~dp0"
call :main %*
popd
exit /b %ERRORLEVEL%

:main
    call build ^
 && call :test Debug -p:CollectCoverage=true ^
                     -p:CoverletOutputFormat=opencover ^
                     -p:Exclude=[NUnit*]* ^
 && call :test Release
exit /b %ERRORLEVEL%

:test
dotnet test --no-build tests -c %*
exit /b %ERRORLEVEL%
