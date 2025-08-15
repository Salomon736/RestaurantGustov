# SUINT.Hub.Restaurant.Api
Restaurant configuration moduleee

## Explicación generals

Modulo de Academic desarrollado en Clean architecture que tiene la estructura como podemos ver en https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html. 
Clean Architecture es un enfoque de diseño de software que busca separar la lógica de negocio de los detalles de la implementación técnica. Su objetivo es crear una arquitectura sólida y mantenible que permita una fácil evolución y extensión del sistema con el tiempo.

Proyecto dividido el código en capas independientes de la implementación técnica, lo que permite una mayor independencia de la administración de la infraestructura y los detalles de implementación. Esto mejora la legibilidad del código y facilita la identificación de las partes de la aplicación que deben modificarse para efectuar cambios.

La arquitectura limpia se compone de cuatro capas: 
- La capa de las APIs de usuario.
- La capa de Application, que se encarga de la lógica de negocio de la aplicación.
- La capa de Domain, que contiene las reglas de negocio y los modelos.
- La capa de Infraestructure, que se encarga de la conexión de la aplicación con su entorno técnico.
- La capa de Test de pruebas unitarias.
  
## Estructura del proyecto 

Al igual que la descripción del sistema de capa debe especificar y describir que hace cada uno de estas, debemos trasladar a nuestro proyecto el mismo comportamiento. Para ello crearemos una serie de carpetas que contendrán la lógica de cada uno de estas capas.

### Principales carpetas

Esta seria la distribución de carpetas que contendrán nuestro proyecto.

```
├── Nombre.Domain
├── Nombre.Application
├── Nombre.Infrastructure
└── Nombre.Api
└── Test
    └─── Test.Aplacation
    └─── Test.Domain
```
## Visión detallada y descripción

Cada uno estos componentes tiene una función y no deben mezclarse entre ellos. Teniendo claro donde debe de ir cada una de las lógicas que implementemos.

```
| SUINT.Hub.Nombre.Api (Folder Soluction)
├── Nombre.Domain (Proyect Class Library)
|   ├── Adapters
|   ├── Common
|   ├── Exceptions
|   ├── Models
│   └── Repositories
├── Nombre.Application (Proyect Class Library)
|   ├── Common
|   ├── Exceptions
|   ├── Providers
│   └── Services
├── Nombre.Infrastructure (Proyect Class Library)
|   ├── Database
|   |   ├─── Dapper  
|   |   └─── EntityFramework  
|   |        ├─── Entities
|   |        ├─── Extensions
|   |        └─── Repositories
|   ├── Ioc
|   |   └─── Di  
|   ├── Providers
|   |   └─── Validators
├── Api (Folder Solution)
│   └── Nombre.Api (Proyect Web Api)
├── Test (Folder Solution)
│   └─── Nombre.Domain.Test  (Proyecto Unit Test)
│   └─── Nombre.Aplication.Test  (Proyecto Unit Test)
│        └─── Common
│        └─── Mock

```

## Uso de la arquitectura

![imagen arquitectura](README/arquitectura.png)

La imagen describe el proceso de obtención de información y como esta se transmite a lo largo de los modulos. 

## Autor SM

Equipo Desarrollo TI
