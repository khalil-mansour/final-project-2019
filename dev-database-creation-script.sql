DROP TABLE IF EXISTS city CASCADE;
DROP TABLE IF EXISTS province CASCADE;
DROP TABLE IF EXISTS house_location CASCADE;
DROP TABLE IF EXISTS quote_request_house CASCADE;
DROP TABLE IF EXISTS quote_request_type_table CASCADE;
DROP TABLE IF EXISTS quote_request_type CASCADE;
DROP TABLE IF EXISTS quote_request CASCADE;
DROP TABLE IF EXISTS quote CASCADE;
DROP TABLE IF EXISTS comment CASCADE;
DROP TABLE IF EXISTS user_profession CASCADE;
DROP TABLE IF EXISTS profession_profile CASCADE;
DROP TABLE IF EXISTS profession CASCADE;
DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS user_type CASCADE;
DROP TABLE IF EXISTS document CASCADE;
DROP TABLE IF EXISTS document_type CASCADE;
DROP TABLE IF EXISTS house_type CASCADE;
DROP TABLE IF EXISTS quote_request_document CASCADE;

CREATE TABLE user_type (
	id serial PRIMARY KEY,
	type varchar(100) NOT NULL,
	description varchar(100)
);

CREATE TABLE users (
	id serial PRIMARY KEY,
	user_type_id integer NOT NULL,
	name varchar(100) NOT NULL,
	surname varchar(50),
	email varchar(50) NOT NULL,
	phone varchar(50),
	postalcode varchar(50),
	province varchar(3),
	birthday date, 
	
	CONSTRAINT user_type_id_fkey FOREIGN KEY (user_type_id)
      REFERENCES user_type (id) MATCH SIMPLE
);

CREATE TABLE profession (
	id serial PRIMARY KEY,
	name varchar(100) NOT NULL,
	description varchar(200) NOT NULL
);

CREATE TABLE profession_profile (
	id serial PRIMARY KEY,
	gender char(1) NOT NULL,
	photo varchar(200),
	business_name varchar(100) NOT NULL,
	business_phone varchar(100) NOT NULL,
	business_email varchar(100) NOT NULL,
	description varchar(500)
);

CREATE TABLE user_profession (
	id serial PRIMARY KEY,
	user_id integer NOT NULL,
	profession_profile_id integer NOT NULL,
	profession_id integer NOT NULL,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE,
	
	CONSTRAINT profession_profile_id_fkey FOREIGN KEY (profession_profile_id)
      REFERENCES profession_profile (id) MATCH SIMPLE,
	
	CONSTRAINT profession_id_fkey FOREIGN KEY (profession_id)
      REFERENCES profession (id) MATCH SIMPLE
);

CREATE TABLE quote_request_type (
	type char(3) PRIMARY KEY, -- 'HAB' pour habitation, 'AUT' pour auto, etc.
	description varchar(100)
);

CREATE TABLE quote_request (
	id serial PRIMARY KEY,
	user_id integer NOT NULL,
	quote_request_type_id char(3) NOT NULL,
	title varchar(100) NOT NULL,
	details varchar(500) NOT NULL,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE,
	
	CONSTRAINT quote_request_type_id_fkey FOREIGN KEY (quote_request_type_id)
      REFERENCES quote_request_type (type) MATCH SIMPLE
);

CREATE TABLE quote (
	id serial PRIMARY KEY,
	user_id integer NOT NULL,
	quote_request_id integer NOT NULL,
	quote_type_id char(3) NOT NULL,
	price numeric NOT NULL,
	details varchar(500),
	status char(1) NOT NULL,
	
	CONSTRAINT quote_request_id_fkey FOREIGN KEY (quote_request_id)
      REFERENCES quote_request (id) MATCH SIMPLE,
	
	CONSTRAINT quote_type_id_id_fkey FOREIGN KEY (quote_type_id)
      REFERENCES quote_request_type (type) MATCH SIMPLE,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE
);

CREATE TABLE comment (
	id serial PRIMARY KEY,
	quote_id integer NOT NULL,
	user_id integer NOT NULL,
	message varchar(500) NOT NULL,
	date_time timestamp NOT NULL,
	
	CONSTRAINT quote_id_fkey FOREIGN KEY (quote_id)
      REFERENCES quote (id) MATCH SIMPLE,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE
);

CREATE TABLE quote_request_type_table (
	id serial PRIMARY KEY,
	quote_request_type_id char(3) UNIQUE NOT NULL,
	table_name varchar(50) NOT NULL,
	
	CONSTRAINT quote_request_type_id_fkey FOREIGN KEY (quote_request_type_id)
      REFERENCES quote_request_type (type) MATCH SIMPLE
);


CREATE TABLE city (
	id serial PRIMARY KEY,
	name varchar(200)
);

CREATE TABLE province (
	id serial PRIMARY KEY,
	name varchar(200)
);

CREATE TABLE house_type (
	id serial PRIMARY KEY,
	property_type varchar(100)
);

CREATE TABLE house_location (
	id serial PRIMARY KEY,
	postalcode varchar(100) NOT NULL,
	city_id integer NOT NULL,
	province_id integer NOT NULL,
	street varchar(100) NOT NULL,
	appartement_units integer NOT NULL,
	
	CONSTRAINT city_id_fkey FOREIGN KEY (city_id)
      REFERENCES city (id) MATCH SIMPLE,

	CONSTRAINT province_id_fkey FOREIGN KEY (province_id)
      REFERENCES province (id) MATCH SIMPLE
);


CREATE TABLE quote_request_house (
	id serial PRIMARY KEY,
	house_type_id integer NOT NULL,
    house_location_id integer NOT NULL,
	offer numeric NOT NULL,
	listing integer NOT NULL,
	down_payment numeric NOT NULL,
	first_house boolean NOT NULL,
	description varchar(1000),
	municipal_evaluation varchar(100), -- Lien URL vers un document de l'évaluation municipale

	CONSTRAINT house_type_id_fkey FOREIGN KEY (house_type_id)
      REFERENCES house_type (id) MATCH SIMPLE,

	CONSTRAINT house_location_id_fkey FOREIGN KEY (house_location_id)
      REFERENCES house_location (id) MATCH SIMPLE
);

CREATE TABLE document_type (
	id serial PRIMARY KEY,
	type varchar(100) NOT NULL
);

-- Si un utilisateur essaie d'uploader un fichier avec un (user_id, name)
-- déjà existant, lui demander confirmation pour écraser l'ancienne version.
-- Si (user_id, name, last_modified) pareil, est-ce qu'on assume que c'est
-- la même version déjà uploadée ou bien on confirme pareil avec lui?
CREATE TABLE document (
	id serial PRIMARY KEY,
	user_id integer NOT NULL,
	document_type_id integer NOT NULL,
	name varchar(100) NOT NULL,
	last_modified timestamp NOT NULL,
	url varchar(200) NOT NULL,
	visible boolean NOT NULL,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE,
	
	CONSTRAINT document_type_id_fkey FOREIGN KEY (document_type_id)
      REFERENCES document_type (id) MATCH SIMPLE
);

CREATE TABLE quote_request_document (
	id serial PRIMARY KEY,
	quote_request_id integer NOT NULL,
	document_id integer NOT NULL,
	
	CONSTRAINT quote_request_id_fkey FOREIGN KEY (quote_request_id)
      REFERENCES quote_request (id) MATCH SIMPLE,
	
	CONSTRAINT document_id_fkey FOREIGN KEY (document_id)
      REFERENCES document (id) MATCH SIMPLE
);

INSERT INTO user_type(type) VALUES ('broker');
INSERT INTO user_type(type) VALUES ('client');
INSERT INTO document_type(type) VALUES ('required_doc1');
INSERT INTO document_type(type) VALUES ('required_doc2');
INSERT INTO document_type(type) VALUES ('required_doc3');
INSERT INTO document_type(type) VALUES ('required_doc4');
INSERT INTO document_type(type) VALUES ('general');
