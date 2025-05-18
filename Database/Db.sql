CREATE DATABASE db_campuslove;
\c db_campuslove;

-- Countries Table
CREATE TABLE Countries(
    id_country SERIAL PRIMARY KEY,
    name_country CHARACTER VARYING(50)
);

-- States Table
CREATE TABLE States(
    id_state SERIAL PRIMARY KEY,
    state_name CHARACTER VARYING(50),
    id_country INTEGER,
    FOREIGN KEY (id_country) REFERENCES Countries(id_country) ON DELETE CASCADE
);

-- Cities Table
CREATE TABLE Cities(
    id_city SERIAL PRIMARY KEY,
    city_name CHARACTER VARYING(50),
    id_state INTEGER,
    FOREIGN KEY (id_state) REFERENCES States(id_state) ON DELETE CASCADE
);

-- Addresses Table
CREATE TABLE Addresses(
    id_address SERIAL PRIMARY KEY,
    id_city INTEGER,
    street_number CHARACTER VARYING(10) NOT NULL,
    street_name CHARACTER VARYING(50) NOT NULL,
    FOREIGN KEY (id_city) REFERENCES Cities(id_city) ON DELETE CASCADE
);

-- Genres Table
CREATE TABLE Genders (
    id_gender SERIAL PRIMARY KEY,
    genre_name VARCHAR(50) NOT NULL UNIQUE
);

-- Careers Table
CREATE TABLE Careers (
    id_career SERIAL PRIMARY KEY,
    career_name VARCHAR(50) NOT NULL UNIQUE
);

-- InterestsCategory Table
CREATE TABLE InterestsCategory (
    id_category SERIAL PRIMARY KEY,
    interest_category VARCHAR(50) NOT NULL UNIQUE
);

-- Interests Table
CREATE TABLE Interests (
    id_interest SERIAL PRIMARY KEY,
    interest_name VARCHAR(50) NOT NULL UNIQUE,
    id_category INT NOT NULL,
    FOREIGN KEY (id_category) REFERENCES InterestsCategory(id_category) ON DELETE CASCADE
);

-- Users Table
CREATE TABLE Users (
    id_user SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(20) NOT NULL,
    birth_date DATE NOT NULL,
    id_gender INT NOT NULL,
    id_career INT NOT NULL,
    id_address INT NOT NULL,
    profile_phrase VARCHAR(200),
    FOREIGN KEY (id_address) REFERENCES Addresses(id_address) ON DELETE CASCADE,
    FOREIGN KEY (id_gender) REFERENCES Genders(id_gender) ON DELETE RESTRICT,
    FOREIGN KEY (id_career) REFERENCES Careers(id_career) ON DELETE RESTRICT
);

-- User-Interest Relationship Table 
CREATE TABLE UsersInterests (
    id_user INT NOT NULL,
    id_interest INT NOT NULL,
    PRIMARY KEY (id_user, id_interest),
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_interest) REFERENCES Interests(id_interest) ON DELETE CASCADE
);

-- Interaction Credits Table
CREATE TABLE InteractionCredits (
    id_user INT PRIMARY KEY,
    available_credits INT NOT NULL DEFAULT 10,
    last_update_date DATE NOT NULL DEFAULT (CURRENT_DATE),
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE
);

-- Interactions Table
CREATE TABLE Interactions (
    id_interaction SERIAL PRIMARY KEY,
    id_user_origin INT NOT NULL,
    id_user_target INT NOT NULL,
    interaction_type TEXT CHECK (interaction_type IN ('like', 'dislike')) NOT NULL,
    interaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CHECK (id_user_origin != id_user_target),
    FOREIGN KEY (id_user_origin) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_user_target) REFERENCES Users(id_user) ON DELETE CASCADE,
    UNIQUE (id_user_origin, id_user_target)
);

-- Matches Table
CREATE TABLE Matches (
    id_match SERIAL PRIMARY KEY,
    id_user1 INT NOT NULL,
    id_user2 INT NOT NULL,
    match_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_user1) REFERENCES Users(id_user) ON DELETE CASCADE,
    FOREIGN KEY (id_user2) REFERENCES Users(id_user) ON DELETE CASCADE,
    UNIQUE (id_user1, id_user2)
);

-- User Statistics Table
CREATE TABLE UserStatistics (
    id_user SERIAL PRIMARY KEY,
    received_likes INT DEFAULT 0,
    received_dislikes INT DEFAULT 0,
    sent_likes INT DEFAULT 0,
    sent_dislikes INT DEFAULT 0,
    total_matches INT DEFAULT 0,
    last_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_user) REFERENCES Users(id_user) ON DELETE CASCADE
);
CREATE TABLE Messages (
    id SERIAL PRIMARY KEY,
    sender_id INT NOT NULL,
    receiver_id INT NOT NULL,
    message_text VARCHAR(250) NOT NULL,
    message_time TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

