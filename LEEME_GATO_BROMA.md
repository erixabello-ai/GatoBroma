# Gato Broma

Versión basada en el motor animado de PixelPaws.

## Opciones
- 5 segundos
- 1 minuto
- 5 minutos
- 15 minutos
- 30 minutos
- Primer click

Al programar, la ventana desaparece. En "Primer click", el clic que
se usó para programar NO cuenta: el hook global se instala después
de cerrar la ventana de programación.

Cuando se dispara:
1. Aparece un aviso de Windows con título "Error Windows".
2. Mensaje: "Tu nivel de estupidez es muy alto."
3. Al pulsar Aceptar, aparece el gato animado.
4. El gato entra caminando desde fuera de la pantalla.
5. Llega al centro y se sienta/queda en posición de descanso.

## Compilación

Requiere .NET 8 SDK solamente en el PC que compila.

Ejecuta `build.bat`.

El resultado es un `GatoBroma.exe` self-contained para Windows 64-bit.
El PC donde se use el EXE no necesita Python ni .NET instalado.

## Nota

Los assets y código originales permanecen bajo sus licencias respectivas.


## Mensaje personalizado
La ventana de programación ahora incluye un campo "Mensaje del error". El texto escrito ahí será el mensaje mostrado en el falso aviso "Error Windows" cuando se dispare la broma.
