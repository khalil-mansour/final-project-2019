DROP TABLE IF EXISTS quote_request_house CASCADE;
DROP TABLE IF EXISTS comment CASCADE;
DROP TABLE IF EXISTS quote CASCADE;
DROP TABLE IF EXISTS user_profession CASCADE;
DROP TABLE IF EXISTS profession_profile CASCADE;
DROP TABLE IF EXISTS profession CASCADE;
DROP TABLE IF EXISTS user_type CASCADE;
DROP TABLE IF EXISTS document_type CASCADE;
DROP TABLE IF EXISTS house_type CASCADE;
DROP TABLE IF EXISTS quote_request_document CASCADE;
DROP TABLE IF EXISTS financial_capacity CASCADE;
DROP TABLE IF EXISTS quote_request CASCADE;
DROP TABLE IF EXISTS document CASCADE;
DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS quote_request_type_table CASCADE;
DROP TABLE IF EXISTS quote_request_type CASCADE;
DROP TABLE IF EXISTS house_location CASCADE;
DROP TABLE IF EXISTS city CASCADE;
DROP TABLE IF EXISTS province CASCADE;

CREATE TABLE user_type (
	id serial PRIMARY KEY,
	type varchar(100) NOT NULL,
	description varchar(100)
);

CREATE TABLE users (
	id varchar(200) PRIMARY KEY,
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
	user_id varchar(200) NOT NULL,
	profession_profile_id integer NOT NULL,
	profession_id integer NOT NULL,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE,
	
	CONSTRAINT profession_profile_id_fkey FOREIGN KEY (profession_profile_id)
      REFERENCES profession_profile (id) MATCH SIMPLE,
	
	CONSTRAINT profession_id_fkey FOREIGN KEY (profession_id)
      REFERENCES profession (id) MATCH SIMPLE
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
	city varchar(200) NOT NULL,
	province_id integer NOT NULL,
	address varchar(100) NOT NULL,
	apartment_unit varchar(5),
	

	CONSTRAINT province_id_fkey FOREIGN KEY (province_id)
      REFERENCES province (id) MATCH SIMPLE
);


CREATE TABLE quote_request_house (
	id serial PRIMARY KEY,
	user_id varchar(200) NOT NULL,
	house_type_id integer NOT NULL,
    house_location_id integer NOT NULL,
	listing integer NOT NULL,
	created_date timestamp NOT NULL,
	down_payment integer,
	offer integer,
	first_house boolean NOT NULL,
	description varchar(1000),
	municipal_evaluation varchar(100), -- Lien URL vers un document de l'évaluation municipale

	CONSTRAINT house_type_id_fkey FOREIGN KEY (house_type_id)
      REFERENCES house_type (id) MATCH SIMPLE,

	CONSTRAINT house_location_id_fkey FOREIGN KEY (house_location_id)
      REFERENCES house_location (id) MATCH SIMPLE
);

CREATE TABLE quote (
	id serial PRIMARY KEY,
	user_id varchar(200) NOT NULL,
	quote_request_house_id integer NOT NULL,
	price numeric NOT NULL,
	details varchar(500),
	status char(1) NOT NULL,
	
	CONSTRAINT quote_request_house_id_fkey FOREIGN KEY (quote_request_house_id)
      REFERENCES quote_request_house (id) MATCH SIMPLE,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE
);

CREATE TABLE comment (
	id serial PRIMARY KEY,
	quote_id integer NOT NULL,
	user_id varchar(200) NOT NULL,
	message varchar(500) NOT NULL,
	date_time timestamp NOT NULL,
	
	CONSTRAINT quote_id_fkey FOREIGN KEY (quote_id)
      REFERENCES quote (id) MATCH SIMPLE,
	
	CONSTRAINT user_id_fkey FOREIGN KEY (user_id)
      REFERENCES users (id) MATCH SIMPLE
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
	user_id varchar(200) NOT NULL,
	document_type_id integer NOT NULL,
	user_file_name varchar(200) NOT NULL,
	storage_file_id varchar (200) NOT NULL,
	created_date timestamp NOT NULL,
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
      REFERENCES quote_request_house (id) MATCH SIMPLE,
	
	CONSTRAINT document_id_fkey FOREIGN KEY (document_id)
      REFERENCES document (id) MATCH SIMPLE
);

CREATE TABLE financial_capacity
(
    uid character varying(200) NOT NULL PRIMARY KEY,
    annualincome integer NOT NULL,
    downpayment integer NOT NULL,
    mensualdebt integer NOT NULL,
    interestrate real NOT NULL,
    municipaltaxes integer NOT NULL,
    heatingcost integer NOT NULL,
    condofee integer NOT NULL,
    CONSTRAINT uid_fkey FOREIGN KEY (uid)
      REFERENCES users (id) MATCH SIMPLE
);

INSERT INTO user_type(type) VALUES ('client');
INSERT INTO user_type(type) VALUES ('broker');
INSERT INTO document_type(type) VALUES ('required_doc1');
INSERT INTO document_type(type) VALUES ('required_doc2');
INSERT INTO document_type(type) VALUES ('required_doc3');
INSERT INTO document_type(type) VALUES ('required_doc4');
INSERT INTO document_type(type) VALUES ('general');
INSERT INTO house_type(property_type) VALUES ('Condo');
INSERT INTO house_type(property_type) VALUES ('maison bi générationnelle');
INSERT INTO province(id, name) VALUES (1, 'AB');
INSERT INTO province(id, name) VALUES (2, 'BC');
INSERT INTO province(id, name) VALUES (3, 'MB');
INSERT INTO province(id, name) VALUES (4, 'NB');
INSERT INTO province(id, name) VALUES (5, 'NL');
INSERT INTO province(id, name) VALUES (6, 'NT');
INSERT INTO province(id, name) VALUES (7, 'NS');
INSERT INTO province(id, name) VALUES (8, 'NU');
INSERT INTO province(id, name) VALUES (9, 'ON');
INSERT INTO province(id, name) VALUES (10,'PE');
INSERT INTO province(id, name) VALUES (11,'QC');
INSERT INTO province(id, name) VALUES (12,'SK');
INSERT INTO province(id, name) VALUES (13,'YT');


INSERT INTO users  VALUES ('uv3dy6EmGYXu9gJcs5LL4POZbKf1', 2, 'Billy', 'Joe le courtier', 'courtier@admin.com', '8196445878', 'r3rw3w', 'qc');
INSERT INTO users  VALUES ('Xe96ZW433IRLemqork9dGvp2tjQ2', 1, 'Billy', 'Joe le client', 'client@admin.com', '8196445878', 'r3rw3w', 'qc');

INSERT INTO document(user_id, document_type_id, user_file_name, storage_file_id, created_date, visible) 
VALUES ('Xe96ZW433IRLemqork9dGvp2tjQ2',1,'lol', 'dsdsa', '2016-06-22 19:10:25-07', True)
