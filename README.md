# PROYECTO TALLER DE LENGUAJES 2 - EZEQUIEL MARTINEZ
## ¿En que consiste el proyecto?
* El objetivo es realizar una aplicacion web siguiendo el patron MVC, trabajando con controladores, repositorios, vistas y modelos. 
* La aplicacion consiste en un "kanban", es decir, un gestor de proyectos o tareas en el cual un usuario puede crear tableros en donde se muestran las distintas tareas relacionadas con dicho tablero.
* Un usuario puede tener uno de los siguientes roles:
    - Administrador: Tiene permiso para basicamente todo. Crea, modifica y elimina tareas de cualquier tablero. Puede eliminar usuarios y cambiarles el rol.
    - Operador: Tiene permisos mas acotados. Solo puede crear, modificar y eliminar tareas en sus propios tableros. Solo puede modificar el estado de las tareas asignadas a él en tableros ajenos y solo puede leer tareas que no le pertenecen ni le corresponden.