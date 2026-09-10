# Crear GatoBroma.exe sin instalar .NET SDK en tu PC

Esta versión incluye un flujo de GitHub Actions. GitHub compila el proyecto en un servidor Windows que ya tiene las herramientas de compilación.

## Pasos

1. Crea un repositorio nuevo en GitHub (puede ser privado).
2. Sube **todo el contenido de esta carpeta** al repositorio, incluyendo `.github/workflows/build-gatobroma.yml`.
3. Ve a la pestaña **Actions**.
4. Abre **Crear GatoBroma.exe**.
5. Si aparece el botón **Run workflow**, ejecútalo. Si no, el workflow se ejecuta automáticamente después del primer commit.
6. Cuando termine en verde, abre la ejecución y busca **Artifacts**.
7. Descarga `GatoBroma-Windows-x64`.
8. Dentro estará `GatoBroma.exe`.

El EXE se publica como Windows x64, autocontenido y de archivo único, por lo que el PC donde se use no necesita tener Python ni .NET instalados.

## Nota

El EXE está pensado para Windows de 64 bits. El proyecto original de PixelPaws y su licencia se mantienen dentro del proyecto.
