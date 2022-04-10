CREATE TABLE USER(
    `guid` VARCHAR(32) PRIMARY KEY,
    `created_date` DATETIME(6) DEFAULT NOW(6)
);

CREATE TABLE EVENT(
    `guid` VARCHAR(32) PRIMARY KEY,
    `name` VARCHAR(32) NOT NULL,
    `content` TEXT NOT NULL,
	`created_date` DATETIME(6) DEFAULT NOW(6),
    `user_guid` VARCHAR(32),
    FOREIGN KEY(`user_guid`) REFERENCES USER(`guid`)
);