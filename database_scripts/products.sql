-- CREATE DATABASE POS_database;

use POS_database


CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1, 1), -- start from 1, auto increase 1
    Name NVARCHAR(100) NOT NULL, 
    Price INT,
    Category NVARCHAR(50) NOT NULL CHECK (Category IN (N'Khai vị', N'Món chính', N'Tráng miệng', N'Nước giải khát', N'Đồ uống có cồn')),
    StockQuantity INT DEFAULT 0,
    IsAvailable VARCHAR(10) NOT NULL DEFAULT 'Y' CHECK (IsAvailable IN ('Y', 'N')),
    ImagePath NVARCHAR(255),
    Type NVARCHAR(10) NOT NULL CHECK (Type in (N'Đồ ăn', N'Đồ uống'))
);


INSERT INTO Product (Name, Price, Category, StockQuantity, IsAvailable, ImagePath, Type) VALUES
	(N'Gỏi cuốn', 30000, N'Khai vị', 100, 'Y', '', N'Đồ ăn'),
	(N'Phở bò', 50000, N'Món chính', 100, 'Y', '', N'Đồ ăn'),
	(N'Bánh mì', 25000, N'Món chính', 100, 'Y', '', N'Đồ ăn'),
	(N'Cơm tấm', 40000, N'Món chính', 100, 'Y', '', N'Đồ ăn'),
	(N'Bún chả', 60000, N'Món chính', 100, 'Y', '', N'Đồ ăn'),
	(N'Mì quảng', 55000, N'Món chính', 100, 'Y', '', N'Đồ ăn'),
	(N'Trái cây tươi', 15000, N'Tráng miệng', 100, 'Y', '', N'Đồ ăn'),
	(N'Trái cây dầm', 30000, N'Tráng miệng', 100, 'Y', '', N'Đồ ăn'),
	(N'Kem ốc quế', 25000, N'Tráng miệng', 100, 'Y', '', N'Đồ ăn'),
	(N'Sinh tố bơ', 20000, N'Nước giải khát', 100, 'Y', '', N'Đồ uống'),
	(N'Nước mía', 15000, N'Nước giải khát', 100, 'Y', '', N'Đồ uống'),
	(N'Nước ép', 20000, N'Nước giải khát', 100, 'Y', '', N'Đồ uống'),
	(N'Trà sữa', 30000, N'Nước giải khát', 100, 'Y', '', N'Đồ uống'),
	(N'Bia', 20000, N'Đồ uống có cồn', 100, 'Y', '', N'Đồ uống');


select * from Product
