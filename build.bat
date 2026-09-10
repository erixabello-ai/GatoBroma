@echo off
cd /d "%~dp0"
title Gato Broma - Compilar

echo ==========================================
echo        GATO BROMA - COMPILACION
echo ==========================================
echo.
where dotnet >nul 2>nul
if errorlevel 1 (
    echo Este PC no tiene el comando dotnet.
    echo.
    echo NO necesitas instalar .NET SDK si vas a usar GitHub Actions.
    echo Consulta: CREAR_EXE_SIN_INSTALAR_SDK.md
    echo.
    pause
    exit /b 1
)

echo Publicando EXE independiente para Windows 64-bit...
echo.
dotnet publish src\DesktopPet\DesktopPet.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:EnableCompressionInSingleFile=true -o publish
if errorlevel 1 (
    echo.
    echo ERROR: No se pudo compilar.
    pause
    exit /b 1
)

echo.
echo LISTO: %~dp0publish\GatoBroma.exe
start "" "%~dp0publish"
pause
