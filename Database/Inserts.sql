-- Countries
INSERT INTO Countries (name_country) VALUES ('Colombia');

-- States
INSERT INTO States (state_name, id_country) VALUES ('Santander', 1);

-- Cities
INSERT INTO Cities (city_name, id_state) VALUES ('Bucaramanga', 1);

-- Addresses 
INSERT INTO Addresses (id_city, street_number, street_name) VALUES 
(1, '101', 'Cabecera'),
(1, '202', 'Centro'),
(1, '303', 'Sur');

-- Genders
INSERT INTO Genders (genre_name) VALUES 
('Masculino'),
('Femenino'),
('No Binario');

-- Careers
INSERT INTO Careers (career_name) VALUES ('Ingeniería en Sistemas');

-- InterestsCategory
INSERT INTO InterestsCategory (interest_category) VALUES 
('Tecnología'),
('Deportes');

-- Interests
INSERT INTO Interests (interest_name, id_category) VALUES 
('Programación', 1),
('Videojuegos', 1),
('Fútbol', 2),
('Ciclismo', 2);

-- Users
INSERT INTO Users (
    first_name, last_name, email, password, birth_date, id_gender, id_career, id_address, profile_phrase
) VALUES 
('Carlos', 'Ramírez', 'Carlos123@gmail.com', 'pass123', '2000-05-20', 1, 1, 1, 'Apasionado por la tecnología.'),
('María', 'Gómez', 'Maria456@gmail.com', 'pass123', '2001-03-15', 2, 1, 2, 'Me encanta programar y jugar videojuegos.'),
('Alex', 'Pérez', 'Alex789@gmail.com', 'pass123', '1999-11-10', 3, 1, 3, 'Fanático del ciclismo y los deportes.');

-- UsersInterests
INSERT INTO UsersInterests (id_user, id_interest) VALUES
(1, 1), 
(1, 3), 
(2, 1), 
(2, 2), 
(3, 4); 

-- InteractionCredits
INSERT INTO InteractionCredits (id_user) VALUES 
(1),
(2),
(3);

-- UserStatistics
INSERT INTO UserStatistics (id_user) VALUES 
(1),
(2),
(3);

-- Matches
INSERT INTO Matches (id_user1, id_user2) VALUES 
(1, 2),
(2, 3),
(3, 1);

-- Interactions
INSERT INTO Interactions (id_user_origin, id_user_target, interaction_type) VALUES 
(1, 2, 'like'),
(2, 3, 'dislike'),
(3, 1, 'like');