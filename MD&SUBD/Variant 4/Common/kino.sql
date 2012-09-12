DROP TABLE Ticket;
DROP TABLE Show;
DROP TABLE Film;

CREATE TABLE Film
(
	ID		NUMBER(5) NOT NULL PRIMARY KEY,
	FilmName	VARCHAR2(25) NOT NULL,
	YearOfRelease   NUMBER(4),
	FilmGenre	VARCHAR2(20),
	Duration	VARCHAR2(20),
	CashGathering	NUMBER(10)
);

CREATE TABLE Show
(
	ID		NUMBER(5) NOT NULL PRIMARY KEY,
	ShowDate	DATE,
	ShowTime 	VARCHAR2(20),
	Price		NUMBER(10),
	Film_ID	        NUMBER(5) REFERENCES Film(ID)
);

CREATE TABLE Ticket
(
	ID		NUMBER(5) NOT NULL PRIMARY KEY,
	Row1		NUMBER(5) NOT NULL,
	Place		NUMBER(5) NOT NULL,
	CashDepartment  NUMBER(5),
        Show_ID         NUMBER(5) REFERENCES Show(ID)  
);
-----------------------------------------------------

INSERT INTO Film VALUES (1,'Dead Man',1995,'Drama','1:55:00',150000000);
INSERT INTO Film VALUES	(2,'Sin City',2004,'Mystical drama','1:57:00',200000000);	
INSERT INTO Film VALUES	(3,'Im Juli',2001,'Comedy','1:35:00',NULL);	
INSERT INTO Film VALUES	(4,'The Wall',1992,'','1:35:00',NULL);
INSERT INTO Film VALUES	(5,'The Game',1990,'','1:49:00',205500000);
INSERT INTO Film VALUES	(6,'Vanila Sky',1999,'Drama','2:14:00',354250000);
INSERT INTO Film VALUES (7,'Saw',2000,'Thriller','1:50:10',301900000);
INSERT INTO Film VALUES (8,'Ameli',2002,'Melodrama','2:29:05',367140000);
INSERT INTO Film VALUES (9,'Sweet November',2001,'Melodrama','1:40:00',NULL);
INSERT INTO Film VALUES (10,'Fight Club',2005,'Psychological Drama','1:41:00',502750000);

------------------------------------------------------

INSERT INTO Show VALUES	(1,TO_DATE('02.10.2005','dd.mm.yyyy'),'21:00:00',15,4);
INSERT INTO Show VALUES	(2,TO_DATE('05.09.2005','dd.mm.yyyy'),'22:00:00',NULL,2);
INSERT INTO Show VALUES	(3,TO_DATE('12.10.2005','dd.mm.yyyy'),'19:00:00',10,3);
INSERT INTO Show VALUES	(4,TO_DATE('23.10.2005','dd.mm.yyyy'),'23:00:00',11,4);
INSERT INTO Show VALUES	(5,TO_DATE('30.09.2005','dd.mm.yyyy'),'18:00:00',5,5);
INSERT INTO Show VALUES	(6,TO_DATE('16.10.2005','dd.mm.yyyy'),'20:00:00',NULL,6);
INSERT INTO Show VALUES	(7,TO_DATE('10.10.2005','dd.mm.yyyy'),'20:00:00',8,2);
INSERT INTO Show VALUES	(8,TO_DATE('13.09.2005','dd.mm.yyyy'),'18:00:00',NULL,8);
INSERT INTO Show VALUES	(9,TO_DATE('29.10.2005','dd.mm.yyyy'),'23:00:00',6,10);
INSERT INTO Show VALUES	(10,TO_DATE('01.01.2006','dd.mm.yyyy'),'21:30:00',NULL,7);

------------------------------------------------------

INSERT INTO Ticket VALUES(1,13,21,3,2);
INSERT INTO Ticket VALUES(2,20,15,2,1);
INSERT INTO Ticket VALUES(3,1,13,3,3);
INSERT INTO Ticket VALUES(4,4,25,1,4);
INSERT INTO Ticket VALUES(5,10,4,NULL,2);
INSERT INTO Ticket VALUES(6,35,10,2,5);
INSERT INTO Ticket VALUES(7,7,12,NULL,7);
INSERT INTO Ticket VALUES(8,11,1,2,8);
INSERT INTO Ticket VALUES(9,5,33,1,3);
INSERT INTO Ticket VALUES(10,12,34,NULL,10);





