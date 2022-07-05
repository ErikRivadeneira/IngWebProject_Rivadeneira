Trabajo realizado por Erik Rivadeneira
******************************Descripción del Proyecto**************************
Este proyecto trata del uso de la arquitectura MVC para crear un Quiz interactivo
que cambia su dificultad a medida que se responden las preguntas que aparezcan
durante la actividad.
*************************Instrucciones de Uso y Credenciales********************
A pesar de que el programa maneja un sistema de roles y autentificación simple,
es posible acceder a la mayoría de componentes a través de la barra de navegación.

El sistema incluye los siguientes usuarios útiles:
* Admin, PW: admin
* Erik, PW: 12345

Con el objetivo de ver las diferentes secciones y CRUDs, se debería acceder a la
cuenta de Administrador (Admin) y entrar a través de la barra de navegación en 
la opción "Lista de CRUDS".

Con el objetivo de realizar la actividad Núcleo, se debería ingresar con la 
cuenta de usuario (Erik), y acceder por la barra de navegación en la opción 
"Contenido" y seleccionar la opción "Empezar quiz!". Una vez dentro, seleccione
la dificultad "Easy" o "Auto", Easy muestra las preguntas al azar del banco de 
preguntas fáciles. En el caso de Auto, se empieza el quiz descrito en la sección
de Descripción del Poryecto.

En caso de que se cree algún error, entrar con la cuenta de administrador, a través
de la barra de navegación en la lista de CRUDs, a la tabla de Maestría y editar
los porcentajes de dificultad para tener al menos un porcentaje cada 10% y luego 
volver a probar.
******************************Flujo del Sistema*********************************
El Flujo del sistema se resume en los siguientes pasos:
1. El usuario ingresa con sus credenciales.
2. El usuario entra a la sección contenido dentro de la barra de navegación
3. El usuario da clic en el enlace Empezar quiz!
4. El usuario escoge la dificultad Auto a través de un selector
5. Se selecciona una pregunta basándose en las puntuaciones anteriores del usuario
6. El usuario responde la pregunta que se muestra en la interfaz
7. Se actualiza la puntuación de combo dependiendo de la respuesta del usuario
8. Se escoge otra pregunta basándose en la puntuación anterior del usuario, el promedio
   de la maestría actual del usuario y las fallas o aciertos que haya presentado hasta 
   la pregunta actual y se la muestra al usuario.
9. Se repiten los pasos 6,7 y 8 hasta responder 10 preguntas.
10. Se actualiza la puntuación del usuario y sus puntuaciones de maestría de las preguntas
   respondidas. 
******************************Enlace de Código en GITHUB************************
El código se lo puede encontrar en GITHUB a través del siguiente
enlace>>>>https://github.com/ErikRivadeneira/IngWebProject_Rivadeneira
******************************Enlace de Código en APPHARBOR*********************
El programa desplegado se lo puede encontrar en APPHARBOR a través del siguiente
enlace>>>>http://ingwebfinalrivadeneira.apphb.com
Build URL>>>https://appharbor.com:443/applications/ingwebfinalrivadeneira/builds?authorization=RNV4klThzU%2bAB%2fdPIs4V5qiHt0wDfjRHjy9IgZlFrJQ%3d
******************************Información de Contacto***************************
Nombre: Erik Rivadeneira
Correo Intitucional: erik.rivadeneira@udla.edu.ec