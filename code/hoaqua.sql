-- Tạo CSDL (nếu cần)
use master
go
CREATE DATABASE CuaHangTraiCay;
go
USE CuaHangTraiCay;
go
-- Bảng HangSanXuat
CREATE TABLE HangSanXuat (
    MaHang INT PRIMARY KEY,
    TenHang NVARCHAR(100)
);

-- Bảng SanPham
CREATE TABLE SanPham (
    MaSP INT PRIMARY KEY,
    TenSP NVARCHAR(100),
    SoLuong INT,
    DonGia DECIMAL(10, 2),
    HinhAnh NVARCHAR(50),
    MaHang INT,
    FOREIGN KEY (MaHang) REFERENCES HangSanXuat(MaHang)
);
-- Thêm hãng sản xuất
INSERT INTO HangSanXuat (MaHang, TenHang) VALUES
(1, N'Hoa Quả Miền Bắc'),
(2, N'Nông Sản Miền Nam'),
(3, N'Vườn Trái Cây Xuất Khẩu');
INSERT INTO SanPham (MaSP, TenSP, SoLuong, DonGia, HinhAnh, MaHang) VALUES
(1, N'Táo', 100, 15000, '1.jpg', 1),
(2, N'Cam', 120, 18000, '2.jpg', 1),
(3, N'Chuối', 200, 12000, '3.jpg', 2),
(4, N'Dưa hấu', 80, 25000, '4.jpg', 2),
(5, N'Nho', 90, 30000, '5.jpg', 3),
(6, N'Xoài', 150, 22000, '6.jpg', 3),
(7, N'Dứa', 100, 17000, '7.jpg', 1),
(8, N'Lê', 110, 16000, '8.jpg', 2),
(9, N'Dâu tây', 75, 35000, '9.jpg', 3),
(10, N'Đu đủ', 95, 19000, '10.jpg', 1);

select * from HangSanXuat;
select * from SanPham;
