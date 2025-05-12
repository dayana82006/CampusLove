CREATE DATABASE campus_love;
USE campus_love;


CREATE TABLE genero(
    id AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

CREATE TABLE carrera(
    id AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);


CREATE TABLE usuario(
    id AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    edad INT NOT NULL,
    genero_id INT NOT NULL,
    carrera_id INT NOT NULL, 
    frase_perfil VARCHAR(500) NOT NULL,
    likes_recibidos INT DEFAULT 0 NOT NULL,
    likes_disponibles DEFAULT 10 NOT NULL,
    FOREIGN KEY (genero_id) REFERENCES genero(id
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
);

CREATE TABLE interes(
    id AUTO_INCREMENT PRIMARY KEY,
);

CREATE TABLE usuario_interes (
    id AUTO_INCREMENT PRIMARY KEY,
);

 CREATE TABLE match(
    id AUTO_INCREMENT PRIMARY KEY,
);

 CREATE TABLE creditos(
    id AUTO_INCREMENT PRIMARY KEY,
);
