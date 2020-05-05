DROP TABLE IF EXISTS Checksin;
DROP TABLE IF EXISTS Friendswith;
DROP TABLE IF EXISTS Tips;
DROP TABLE IF EXISTS Hours;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Business;
DROP TABLE IF EXISTS Users;


CREATE TABLE Business	(
	business_id VARCHAR(50),
	name VARCHAR(64),
	address VARCHAR(256),
	state VARCHAR(32),
	postal_code INTEGER,
	city VARCHAR(32),
	longitude DECIMAL,
	latitude DECIMAL,
	stars DECIMAL,
	is_open INTEGER,
	checkin_count INTEGER DEFAULT(0),
	tip_count INTEGER DEFAULT(0),
	PRIMARY KEY (business_id)
);


CREATE TABLE Users	(
	user_id VARCHAR(50),
	name VARCHAR(64),
	yelping_since DATE,
	tip_count INTEGER DEFAULT(0),
	tip_like_count INTEGER DEFAULT(0),
	longitude DECIMAL,
	latitude DECIMAL,
	useful_count INTEGER,
	cool_count INTEGER,
	funny_count INTEGER,
	fans_count INTEGER,
	average_stars DECIMAL,
	PRIMARY KEY (user_id)
);

CREATE TABLE Checksin	(
	checkin_time TIMESTAMP,
	business_id VARCHAR(50),
	PRIMARY KEY (checkin_time, business_id),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE Friendswith	(
	user_id VARCHAR(50),
	friend_id VARCHAR(50),
	PRIMARY KEY (user_id, friend_id),
	FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE Tips	(
	user_id VARCHAR(50),
	business_id VARCHAR(50),
	date_created TIMESTAMP,
	like_count INTEGER,
	tip_text VARCHAR(512),
	PRIMARY KEY (user_id, business_id, date_created, tip_text),
	FOREIGN KEY (user_id) REFERENCES Users(user_id),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE Hours	(
	business_id VARCHAR(50),
	open_time VARCHAR(50),
	close_time VARCHAR(50),
	day VARCHAR(12),
	PRIMARY KEY (business_id, day),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE Categories	(
	business_id VARCHAR(50),
	category_name VARCHAR(64),
	PRIMARY KEY (business_id, category_name),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);
