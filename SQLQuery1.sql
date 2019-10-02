SELECT * FROM Items

CREATE TABLE Customers(
Id INT IDENTITY(1,1),
Name VARCHAR(30),
Contact VARCHAR(11),
Address VARCHAR(200)
)

SELECT * FROM Customers

CREATE TABLE Orders(
Id INT IDENTITY(1,1),
CustomerName VARCHAR(30),
ItemName VARCHAR(30),
Quantity INT,
TotalPrice FLOAT
)

SELECT * FROM Orders
