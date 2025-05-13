CREATE DATABASE campus_love;
USE campus_love;
-- Tabla pais
CREATE TABLE pais (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

-- Tabla region
CREATE TABLE region (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50),
    paisId INT,
    FOREIGN KEY (paisId) REFERENCES pais(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- Tabla ciudad
CREATE TABLE ciudad (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50),
    regionId INT,
    FOREIGN KEY (regionId) REFERENCES region(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE direccion(
    id INT AUTO_INCREMENT PRIMARY KEY,  -- Cambiado a AUTO_INCREMENT
    ciudadId INT,
    calleNumero VARCHAR(12),
    calleNombre VARCHAR(50),
    FOREIGN KEY (ciudadId) REFERENCES ciudad(id)
);
CREATE TABLE genero(
   id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

CREATE TABLE carrera(
   id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)
);

CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,  -- Añadido
    clave VARCHAR(100) NOT NULL,         -- Añadido
    usuario_name VARCHAR(50) UNIQUE,     -- Añadido
    edad INT NOT NULL,
    genero_id INT NOT NULL,
    carrera_id INT NOT NULL,
    direccion_id INT NOT NULL,
    frase_perfil VARCHAR(500) NOT NULL,
    likes_recibidos INT DEFAULT 0 NOT NULL,
    likes_disponibles INT DEFAULT 10 NOT NULL,
    FOREIGN KEY (genero_id) REFERENCES genero(id),
    FOREIGN KEY (carrera_id) REFERENCES carrera(id),
    FOREIGN KEY (direccion_id) REFERENCES direccion(id)
);


CREATE TABLE interes(
   id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE usuario_interes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id int, 
    interes_id int,
    FOREIGN KEY (usuario_id) REFERENCES usuario(id)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
    FOREIGN KEY(interes_id) REFERENCES interes(id)
    ON DELETE RESTRICT
    ON UPDATE CASCADE

);

CREATE TABLE matches (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario1_id INT,
    usuario2_id INT,
    fecha DATETIME NOT NULL,
    FOREIGN KEY (usuario1_id) REFERENCES usuario(id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    FOREIGN KEY (usuario2_id) REFERENCES usuario(id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    CONSTRAINT UC_Match UNIQUE (usuario1_id, usuario2_id)
);

CREATE TABLE user_like (
    id INT AUTO_INCREMENT PRIMARY KEY,
    emisor INT,
    receptor INT,
    esLike BIT NOT NULL, -- 1 = Like, 0 = Dislike
    fecha DATETIME NOT NULL,
    FOREIGN KEY (emisor) REFERENCES usuario(id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    FOREIGN KEY (receptor) REFERENCES usuario(id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE
);
