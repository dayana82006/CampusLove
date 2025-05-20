# 💘 Campus Love... Where is Love

## Descripción del proyecto

**Campus Love** es una aplicación de consola desarrollada en C#, diseñada para simular un sistema de emparejamiento entre estudiantes. El objetivo principal del proyecto es aplicar principios sólidos de diseño de software, tales como arquitectura limpia, principios SOLID y patrones de diseño, en el contexto de un sistema interactivo de tipo "dating app".

La aplicación permite a los usuarios registrarse, explorar perfiles de otros estudiantes, reaccionar con “like” o “dislike”, y visualizar coincidencias si el interés es mutuo. Además, incorpora una lógica de créditos de interacción diarios, también limitando la cantidad de likes por día.

## Características destacadas

- Registro de usuarios con datos personalizados (nombre, edad, género, carrera, intereses y frase de perfil).
- Exploración de perfiles y toma de decisiones (like/dislike).
- Detección y visualización de coincidencias entre usuarios.
- Sistema de créditos diarios para limitar likes, utilizando operaciones matemáticas y validaciones.
- Estadísticas dinámicas del sistema utilizando LINQ, como el ranking de usuarios con más likes o matches.
- Formateo cultural y presentación de datos con `CultureInfo` y `NumberFormatInfo`.
- Menú interactivo en consola con flujo claro y amigable.

## Objetivo

Este proyecto tiene como finalidad integrar los conocimientos adquiridos sobre programación orientada a objetos, diseño de software, patrones y estructuras de datos, aplicados en un contexto lúdico pero técnicamente desafiante. A través de esta simulación, se pone a prueba la capacidad de diseño, organización del código, uso de herramientas del lenguaje y análisis lógico.

## Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET Core 8.0
- **Base de datos:** MySQL o PostgreSQL
- **IDE recomendado:** Visual Studio Code

## Estructura del sistema

El sistema está compuesto por clases y servicios como:

- `Usuario`: Información personal y preferencias.
- `Interaccion`: Registro de likes/dislikes.
- `MatchService`: Lógica para detección de coincidencias.
- `GestorUsuarios`: Administración de los datos de usuarios.

Se incluyen diagramas UML (clases, DER, modelos lógico/físico) que apoyan la documentación técnica del sistema.

- Ejecuta [Database/db.sql](Database/Db.sql) para cargar la base de datos.
- Ejecuta [Database/Inserts.sql](Database/Inserts.sql) para cargar los datos iniciales.

## Diagramas UML
- Diagrama de clases
  ![alt text](Database/Diagramas/DiagramaClases.png)
- Diagrama DER
  ![alt text](Database/Diagramas/DiagramaDER.png)
- Modelo lógico
  ![alt text](Database/Diagramas/ModeloLogico.png)
- Modelo físico
  ![alt text](Database/Diagramas/ModeloFisico.png)

---

