# ArandaApi
API REST FULL que implementa un CRUD utilizando EntityFramework como ORM y como tecnologia base .net Framework
## Iniciando

A continuación, se mencionan algunos datos a tener en cuenta de este proyecto para que pueda funcionar en su maquina local.

### Prerrequisitos y recomendaciones

Para poder ejecutar este proyecto de forma correcta y poder hacer pruebas satisfactorias se debe tener en cuenta lo siguiente

* El proyecto base se realizó en .Net Framework 4.7.2
* Se utilizó EntityFramewok(v6.4.4) como ORM
* Internamente las fuentes estan apuntando a una instacia de SQL SERVER LocalDB la cual es una BD ligera que se instala con SQL Server Express(Para este ejercicio se utilizo SQL SERVER 2016)
* Se agregaron los paquetes System.IdentityModel.Tokens.Jwt, Microsoft.Owin.Security.Jwt, Microsoft.AspNet.WebApi.Owin, Microsoft.Owin.Host.SystemWeb para implementar JWT
* Contar con Postman o herramienta similar que permita el consumo de Apis de forma sencilla

## Uso

Antes de iniciar el proyecto se debe crear una base de datos con el nombre ArandaDB, y allí crear dos tablas que usará el Api, el script de las tablas se encuentra adjunto en el repositorio en una carpeta llamada Scripts.

La connectionString tiene el nombre de ArandaDBModelEntity y esta definida en los archivos Web.config y App.Config de cada proyecto que tiene esta solución de visual studio

Despues de haber creado las tablas de adjuntas en los Scripts el proyecto puede ser ejecutado.
