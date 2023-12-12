CREATE DATABASE WSBGS;

USE WSBGS; 

CREATE TABLE Student (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(70),
);

CREATE TABLE Note (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	StudentId INT,
	Subject nvarchar(70),
	Note INT,

	CONSTRAINT FK__NOTE__ID FOREIGN KEY (StudentId) REFERENCES Student(Id),
);

INSERT INTO Student (Name) VALUES ('first');
INSERT INTO Student (Name) VALUES ('second');
INSERT INTO Student (Name) VALUES ('third');

INSERT INTO Note (StudentId, Subject, Note) VALUES (1, 'subj1', 9);
INSERT INTO Note (StudentId, Subject, Note) VALUES (1, 'subj2', 8);
INSERT INTO Note (StudentId, Subject, Note) VALUES (1, 'subj3', 7);

INSERT INTO Note (StudentId, Subject, Note) VALUES (2, 'subj1', 6);
INSERT INTO Note (StudentId, Subject, Note) VALUES (2, 'subj2', 5);
INSERT INTO Note (StudentId, Subject, Note) VALUES (2, 'subj3', 4);

INSERT INTO Note (StudentId, Subject, Note) VALUES (3, 'subj1', 3);
INSERT INTO Note (StudentId, Subject, Note) VALUES (3, 'subj2', 2);
INSERT INTO Note (StudentId, Subject, Note) VALUES (3, 'subj3', 1);