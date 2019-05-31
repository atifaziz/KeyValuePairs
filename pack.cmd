@echo off
pushd "%~dp0"
call :main %*
popd
exit /b %ERRORLEVEL%

:main
setlocal
set VERSION_SUFFIX=
if not "%~1"=="" set VERSION_SUFFIX=--version-suffix %~1
call build && dotnet pack --no-build -c Release %VERSION_SUFFIX% src
exit /b %ERRORLEVEL%
