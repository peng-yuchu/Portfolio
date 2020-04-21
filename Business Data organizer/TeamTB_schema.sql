CREATE TABLE user	(
	user_id INTEGER,
	name VARCHAR() NOT NULL,
	latitude INTEGER,
	longitude INTEGER,
	yelping_since DATETIME NOT NULL,
	average_stars DECIMAL(2),
	fans_count INTEGER DEFAULT(0),
	useful_count INTEGER DEFAULT(0),
	cool_count INTEGER DEFAULT(0),
	funny_count INTEGER DEFAULT(0),
	tip_count INTEGER DEFAULT(0),
	tip_like_count INTEGER DEFAULT(0),
	PRIMARY KEY (user_id),
);

CREATE TABLE friends	(
	user_id INTEGER,
	friend_id INTEGER,
	PRIMARY KEY (user_id, friend_id),
	FOREIGN KEY (user_id) REFERENCES user(user_id)
);

CREATE TABLE business	(
	business_id INTEGER,
	name VARCHAR NOT NULL,
	address VARCHAR NOT NULL,
	city VARCHAR NOT NULL,
	state VARCHAR NOT NULL,
	postal_code INTEGER,
	latitude INTEGER,
	longitude INTEGER,
	stars DECIMAL(2) DEFAULT(0),
	tip_count INTEGER DEFAULT(0),
	checkin_count DEFAULT(0),
	is_open BOOLEAN,
	PRIMARY KEY (business_id)
);

CREATE TABLE check_ins	(
	business_id INTEGER,
	checkin_time DATETIME,
	PRIMARY KEY (business_id, checkin_time),
	FOREIGN KEY (business_id) REFERENCES business(business_id)
);

CREATE TABLE hours	(
	business_id INTEGER,
	day VARCHAR(9),
	open_time DATETIME,
	close_time DATETIME,
	PRIMARY KEY (business_id, day),
	FOREIGN KEY (business_id) REFERENCES business(business_id)
);

CREATE TABLE categories	(
	business_id INTEGER,
	category_name VARCHAR,
	PRIMARY KEY (business_id, category_name),
	FOREIGN KEY (business_id) REFERENCES business(business_id)
);

CREATE TABLE tips	(
	user_id INTEGER,
	business_id INTEGER,
	date_created DATETIME NOT NULL,
	like_count INTEGER DEFAULT(0),
	tip_text VARCHAR NOT NULL,
	PRIMARY KEY (user_id, business_id),
	FOREIGN KEY (user_id) REFERENCES user(user_id),
	FOREIGN KEY (business_id) REFERENCES business(business_id)
);
