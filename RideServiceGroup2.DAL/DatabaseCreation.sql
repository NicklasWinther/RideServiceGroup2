USE RideServiceGroup2
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Reports')
DROP TABLE Reports
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Rides')
DROP TABLE Rides
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RideCategories')
DROP TABLE RideCategories

CREATE TABLE RideCategories(
	RideCategoryId INT PRIMARY KEY IDENTITY(1,1),
	Name nvarchar(50) NOT NULL,
	Description nvarchar(MAX) NOT NULL
);

CREATE TABLE Rides(
	RideId INT PRIMARY KEY IDENTITY(1,1),
	Name nvarchar(50) NOT NULL,
	ImgUrl nvarchar(50) NOT NULL,
	Description nvarchar(MAX) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES RideCategories(RideCategoryId) NOT NULL
);

CREATE TABLE Reports(
	ReportId INT PRIMARY KEY IDENTITY(1,1),
	Status INT NOT NULL,
	ReportTime DateTime2(7) NOT NULL,
	Notes nvarchar(MAX) NOT NULL,
	RideId INT FOREIGN KEY REFERENCES Rides(RideId) NOT NULL
);
GO
INSERT INTO RideCategories
VALUES('Vandforlystelse', 'Forlystelser med vand')
INSERT INTO RideCategories
VALUES('Pendulforlystele', 'Forlystelser der gynger frem og tilbage')
INSERT INTO RideCategories
VALUES('Rutsjebane', 'Forlystelser som best�r af en bane med stejle fald, hvor�r man k�rer i sm� vogne')

INSERT INTO Rides
VALUES('Orkanen', 'img/orkanen.jpg', 'Orkanen er en fantastisk og h�sbl�sende rutsjebane for hele familien. Den har alt hvad adrenalinhungrende fartdj�vle med hang til store h�jder og dybe fald t�r dr�mme om!', '3')
INSERT INTO Rides
VALUES('Vandcyklonen', 'img/vandcyklonen.jpg', 'Vandcyklonen er en rutsjebane, der med garanti g�r dig plaskhamrende rundtosset. Spring om bord i den store 2-personers badering og hold godt fast, n�r det g�r susende rundt i en k�mpem�ssig tragt, der pludselig ender brat i en� aaaaargh� stejl og m�rk tunnel!', '1')
INSERT INTO Rides
VALUES('Snurretr�et', 'img/snurretraeet.jpg', 'Tr�stammen vugger f�rst stille og roligt frem og tilbage, men pludselig begynder den ogs� at snurre rundt om sig selv - og s� er det bare om at holde godt fast. Aaargh!', '2')

INSERT INTO Reports
VALUES('2', '2018-07-26', 'Virker ikke', '1');
INSERT INTO Reports
VALUES('3', '2018-07-27', 'Vi arbejder p� sagen', '1');
INSERT INTO Reports
VALUES('1', '2018-07-30', 'Forlystelsen virker igen', '1');
INSERT INTO Reports
VALUES('2', '2019-04-12', 'G�et i smadder','3');

