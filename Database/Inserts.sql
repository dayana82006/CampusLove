-- Countries
INSERT INTO Countries (country_name) VALUES ('Colombia');

-- States
INSERT INTO States (state_name, id_country) VALUES ('Santander', 1);

-- Cities
INSERT INTO Cities (city_name, id_state) VALUES ('Bucaramanga', 1);

-- Addresses 
INSERT INTO Addresses (id_city, street_number, street_name) VALUES 
(1, '101', 'Cabecera'),
(1, '202', 'Centro'),
(1, '303', 'Sur'),
(1, '404', 'La Victoria'),
(1, '505', 'San Francisco'),
(1, '606', 'Mutis'),
(1, '707', 'La Universidad'),
(1, '808', 'Real de Minas'),
(1, '909', 'Ciudadela'),
(1, '111', 'Provenza'),
(1, '112', 'Antonia Santos'),
(1, '113', 'La Concordia'),
(1, '114', 'El Prado'),
(1, '115', 'Alarcón'),
(1, '116', 'La Feria');

-- Genders
INSERT INTO Genders (genre_name) VALUES 
('Masculino'),
('Femenino'),
('No Binario');

-- Careers 
INSERT INTO Careers (career_name) VALUES 
('Ingeniería en Sistemas'),
('Medicina'),
('Derecho'),
('Psicología'),
('Administración de Empresas'),
('Arquitectura'),
('Diseño Gráfico'),
('Comunicación Social'),
('Biología'),
('Filosofía');

-- InterestsCategory 
INSERT INTO InterestsCategory (interest_category) VALUES 
('Tecnología'),
('Deportes'),
('Arte'),
('Música'),
('Cine'),
('Literatura'),
('Viajes'),
('Gastronomía'),
('Ciencia'),
('Medio Ambiente');

-- Interests 
INSERT INTO Interests (interest_name, id_category) VALUES 
('Programación', 1),
('Videojuegos', 1),
('Inteligencia Artificial', 1),
('Ciberseguridad', 1),
('Fútbol', 2),
('Ciclismo', 2),
('Natación', 2),
('Running', 2),
('Pintura', 3),
('Escultura', 3),
('Rock', 4),
('Jazz', 4),
('Cine de autor', 5),
('Documentales', 5),
('Novelas', 6),
('Poesía', 6),
('Turismo cultural', 7),
('Aventuras', 7),
('Cocina internacional', 8),
('Repostería', 8),
('Astronomía', 9),
('Biología', 9),
('Sostenibilidad', 10),
('Reciclaje', 10);

-- Users 
INSERT INTO Users (
    first_name, last_name, email, password, birth_date, id_gender, id_career, id_address, profile_phrase
) VALUES 
('Carlos', 'Ramírez', 'Carlos123@gmail.com', 'pass123', '2000-05-20', 1, 1, 1, 'Apasionado por la tecnología.'),
('María', 'Gómez', 'Maria456@gmail.com', 'pass123', '2001-03-15', 2, 1, 2, 'Me encanta programar y jugar videojuegos.'),
('Alex', 'Pérez', 'Alex789@gmail.com', 'pass123', '1999-11-10', 3, 1, 3, 'Fanático del ciclismo y los deportes.'),
('Lucía', 'Martínez', 'lucia.m@gmail.com', 'pass123', '1998-07-12', 2, 2, 4, 'Salvar vidas es mi pasión.'),
('Juan', 'López', 'juan.l@gmail.com', 'pass123', '1997-01-05', 1, 3, 5, 'Amo la justicia y el debate.'),
('Valentina', 'Ríos', 'valen.r@gmail.com', 'pass123', '2000-09-23', 2, 4, 6, 'Escucho y acompaño a quienes lo necesitan.'),
('Camilo', 'Suárez', 'cami.s@gmail.com', 'pass123', '1996-04-19', 1, 5, 7, 'Emprendedor de corazón.'),
('Laura', 'Ortiz', 'laura.o@gmail.com', 'pass123', '1995-06-11', 2, 6, 8, 'El arte está en cada rincón.'),
('Diego', 'Mendoza', 'diego.m@gmail.com', 'pass123', '1994-02-28', 1, 7, 9, 'El diseño es una forma de vida.'),
('Sofía', 'García', 'sofi.g@gmail.com', 'pass123', '1998-12-13', 2, 8, 10, 'Contar historias es mi superpoder.'),
('Esteban', 'Morales', 'esteban.m@gmail.com', 'pass123', '1997-03-22', 1, 9, 11, 'Descubriendo la vida con ciencia.'),
('Isabela', 'Luna', 'isabela.l@gmail.com', 'pass123', '1999-08-30', 2, 10, 12, 'Amante de los libros y la filosofía.'),
('Andrés', 'Torres', 'andres.t@gmail.com', 'pass123', '2001-01-25', 1, 1, 13, 'Curioso por naturaleza.'),
('Carla', 'Mejía', 'carla.m@gmail.com', 'pass123', '2000-10-10', 2, 3, 14, 'Creyente en un mundo más justo.'),
('Matías', 'Vargas', 'matias.v@gmail.com', 'pass123', '1996-11-11', 1, 6, 15, 'El arte y la arquitectura lo son todo.');

-- UsersInterests 
INSERT INTO UsersInterests (id_user, id_interest) VALUES
(1, 1), (1, 5),
(2, 1), (2, 2),
(3, 6),
(4, 8), (4, 21),
(5, 5), (5, 3),
(6, 16),
(7, 18),
(8, 9), (8, 10),
(9, 11), (9, 12),
(10, 13), (10, 14),
(11, 21), (11, 22),
(12, 15), (12, 16),
(13, 1), (13, 3),
(14, 5), (14, 7),
(15, 9), (15, 19);

-- InteractionCredits
INSERT INTO InteractionCredits (id_user) VALUES 
(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),
(11),(12),(13),(14),(15);

-- UserStatistics
INSERT INTO UserStatistics (id_user) VALUES 
(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),
(11),(12),(13),(14),(15);

-- Matches (likes mutuos)
INSERT INTO Matches (id_user1, id_user2) VALUES 
(1, 2),
(3, 6),
(4, 11),
(5, 14),
(7, 10);

-- Interactions 
INSERT INTO Interactions (id_user_origin, id_user_target, interaction_type) VALUES 
(1, 2, 'like'),
(2, 1, 'like'),
(3, 6, 'like'),
(6, 3, 'like'),
(4, 11, 'like'),
(11, 4, 'like'),
(5, 14, 'like'),
(14, 5, 'like'),
(7, 10, 'like'),
(10, 7, 'like'),
(1, 3, 'dislike'),
(2, 4, 'like');

-- Messages
INSERT INTO Messages (sender_id, receiver_id, message_text) VALUES 
(1, 2, 'Hola, ¡me gustó tu perfil!'),
(2, 1, '¡Gracias! El tuyo también.'),
(3, 6, '¿Te gusta el ciclismo también?'),
(6, 3, '¡Sí! Salgo a montar los domingos.'),
(4, 11, 'Hola Esteban, ¿también te interesa la ciencia?'),
(11, 4, '¡Mucho! Es mi pasión.'),
(5, 14, 'Qué interesante lo del derecho.'),
(14, 5, '¡Gracias! Me gusta mucho debatir.'),
(7, 10, '¿Qué tipo de películas te gustan?'),
(10, 7, 'Sobre todo documentales.');