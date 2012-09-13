/************************************************************
* Create the database and all of its tables.
************************************************************/
  
DROP DATABASE IF EXISTS PatternRecommender;
  
CREATE DATABASE PatternRecommender;
  
USE PatternRecommender;
  
CREATE TABLE User (
    username VARCHAR(250) NOT NULL,
    preferFree BOOLEAN NOT NULL DEFAULT 'false',
    preferDownloadable BOOLEAN NOT NULL DEFAULT 'false',
    strictness INT DEFAULT '0',
  
    PRIMARY KEY (username)
);
  
CREATE TABLE Bookmark(
    username INT NOT NULL,
    patternId INT NOT NULL,
	dateAdded DATETIME NOT NULL,
  
    PRIMARY KEY (patternId, username),
    FOREIGN KEY (username) REFERENCES User (username),
	FOREIGN KEY (patternId) REFERENCES Pattern (id)
);
  
CREATE TABLE Pattern(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(250) NOT NULL,
    author VARCHAR(250) NOT NULL,
    yarnId INT NOT NULL,
	yardage INT,
	needleSize INT,
	gauge DECIMAL(2,2),
	difficulty INT,
	rating INT,

  
    PRIMARY KEY (id),
    FOREIGN KEY (yarnId) REFERENCES Yarn (id),
);
  
CREATE TABLE Yarn(
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(150) NOT NULL UNIQUE DEFAULT '',
    weight enum('Thread','Cobweb','Lace','Light Fingering','Fingering','Sport','DK','Worsted','Aran','Bulky','Super Bulky') DEFAULT NULL,
    fiber VARCHAR(250) DEFAULT NULL,
  
    PRIMARY KEY (id)
);