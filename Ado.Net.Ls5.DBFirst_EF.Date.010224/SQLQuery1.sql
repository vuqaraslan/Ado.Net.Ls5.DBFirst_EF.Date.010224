
CREATE DATABASE AirLanesDB
GO
USE AirLanesDB
GO

CREATE TABLE Cities
(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(MAX) NOT NULL
)
GO
INSERT INTO Cities([Name]) 
VALUES ('London'),('Baku'),('Paris'),('Vashington'),('Berlin'),('Madrid'),('Edinburgh')

GO

CREATE TABLE Schedules
(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[CityId] INT FOREIGN KEY REFERENCES Cities(Id) NOT NULL,
[Date] DATETIME NOT NULL
)
GO
INSERT INTO Schedules([CityId],[Date])
VALUES (1,'2024-01-14'),(2,'2024-01-20'),(3,'2024-01-30'),(4,'2024-01-31'),
(5,'2024-02-06'),(6,'2024-02-15'),(7,'2024-02-17')

GO


CREATE TABLE Pilots
(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[FullName] NVARCHAR(MAX) NOT NULL,
[Age] INT NOT NULL
)
GO
INSERT INTO Pilots([FullName],[Age])
VALUES ('Henry McCanney',25),('Jack Walker',24)

GO

CREATE TABLE AirPlanes
(
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[BrandName] NVARCHAR(MAX) NOT NULL,
[SchedulId] INT FOREIGN KEY REFERENCES Schedules(Id) NOT NULL,
[PilotId] INT FOREIGN KEY REFERENCES Pilots(Id) NOT NULL
)
GO
INSERT INTO AirPlanes([BrandName],[SchedulId],[PilotId])
VALUES ('Airbus',1,2),('Boeing',2,2),('Raytheon',3,2),('Northrop Grumman',4,2),
('General Electric',5,1),('Safran',6,1),('Leonardo',7,1)

SELECT *
FROM Cities

SELECT *
FROM Schedules

SELECT *
FROM Pilots

SELECT *
FROM AirPlanes


--SELECT *
--FROM AirPlanes AS A,Schedules AS S,Cities AS C
--WHERE A.SchedulId=S.Id AND S.CityId=C.Id

SELECT *
FROM AirPlanes AS A
INNER JOIN Schedules AS S
ON A.SchedulId=S.Id
	INNER JOIN Cities AS C
	ON S.CityId=C.Id

--SELECT *
--FROM AirPlanes