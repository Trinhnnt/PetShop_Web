-- Cơ sở dữ liệu: website_PetShop
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'website_PetShop')
    DROP DATABASE website_PetShop;
GO

CREATE DATABASE website_PetShop;
GO

USE website_petShop;
GO

-- Cấu trúc bảng cho bảng binhluan
CREATE TABLE binhluan (
    MaBL int IDENTITY(1,1) NOT NULL,
    MaTTSP int NOT NULL,
    MaKH int NOT NULL,
    NoiDung nvarchar(max) NULL,
    PRIMARY KEY (MaBL)
);

-- Đang đổ dữ liệu cho bảng binhluan
SET IDENTITY_INSERT binhluan ON;
INSERT INTO binhluan (MaBL, MaTTSP, MaKH, NoiDung) VALUES
(1, 3, 1, N'Nhân viên không rành về các chương trình khuyến mãi. Tuy nhiên thái độ phục vụ rất tốt. Thức ăn cho mèo chất lượng cao, mèo nhà mình rất thích.'),
(2, 3, 4, N'Mới mua được vài tháng thì thấy chất lượng thức ăn không ổn định. Mèo nhà mình không thích ăn như lúc đầu. Liên hệ với shop gần 1 tháng không được giải quyết thỏa đáng.'),
(3, 3, 2, N'Thức ăn rất tốt cho mèo nhà mình. Lông mèo mượt hơn, ít rụng. Mèo ăn rất ngon miệng và không kén. Đặc biệt là không có mùi hôi sau khi ăn. Mua từ tháng 7/2024 tới giờ vẫn dùng thường xuyên. Đáng mua sử dụng lâu dài nha mọi người.'),
(4, 3, 2, N'Mua được 5 tháng thì thấy bao bì không đảm bảo, thức ăn bị ẩm mốc. Mua gói bảo hành 6 tháng liên hệ cả tháng không giải quyết. Chắc đợi hết tháng hết bảo hành là xong.'),
(5, 1, 2, N'Cát vệ sinh cho mèo của shop rất tốt, khử mùi hiệu quả, mèo nhà mình thích dùng. Tuy nhiên gần đây mình thấy cát có màu hơi khác so với lần mua trước, shop có thể kiểm tra giúp mình không?'),
(6, 1, 2, N'Cát vệ sinh này thấm hút kém hơn lần mua trước, mình đã kiểm tra kỹ và thấy chất lượng không đồng đều, phần cát màu sáng thì tốt còn phần cát màu tối thì không thấm hút.'),
(7, 1, 2, N'Dạ nếu sản phẩm mình mua mới tại PetShop.com từ ngày 1/10/2024 trong vòng 3 tháng gần đây, còn nguyên bao bì, không bị ẩm mốc, còn giữ nguyên tình trạng ban đầu thì bên em có chính sách đổi trả ạ. Mình vui lòng cung cấp số điện thoại mua hàng để bên em hỗ trợ kiểm tra đơn hàng giúp mình ạ!'),
(8, 1, 2, N'Dạ sản phẩm cát vệ sinh hạt nhỏ hiện tạm hết hàng tại khu vực của mình, nếu quan tâm, mình thông tin thêm khu vực phường/xã bên em hỗ trợ kiểm tra sản phẩm có chuyển hàng về được hay không giúp mình nhé. Mong nhận phản hồi từ anh.'),
(9, 4, 4, N'Đồ chơi cho chó rất bền, chó nhà mình cắn mãi không hỏng, dịch vụ tư vấn rất tốt'),
(12, 6, 4, N'Lồng cho chim rất đẹp, rộng rãi và chắc chắn, shop phục vụ nhiệt tình'),
(13, 13, 2, N'Thuốc tẩy giun cho mèo rất hiệu quả, mèo uống không bị nôn và khỏe mạnh hơn rõ rệt.');
SET IDENTITY_INSERT binhluan OFF;


-- Cấu trúc bảng cho bảng dssanpham_mua
CREATE TABLE dssanpham_mua (
    MaSPM int IDENTITY(1,1) NOT NULL,
    MaHD int NOT NULL,
    MaKH int NOT NULL,
    MaTTSP int NOT NULL,
    SoLuong int NOT NULL,
    ThanhTien int NOT NULL,
    PRIMARY KEY (MaSPM)
);

-- Cấu trúc bảng cho bảng cthd
CREATE TABLE cthd (
    MaHD int NOT NULL,
    MaTTSP int NOT NULL,
    SoLuong int NOT NULL,
    ThanhTien int NULL,
    PRIMARY KEY (MaHD, MaTTSP)
);

-- Đang đổ dữ liệu cho bảng cthd (đã điều chỉnh giá tiền và số lượng phù hợp với sản phẩm thú cưng)
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(1, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(4, 3, 3, 405000),              -- Thức ăn cho mèo cao cấp
(4, 10, 1, 1450000),            -- Lồng mèo cao cấp
(5, 1, 3, 180000),              -- Cát vệ sinh cho mèo
(5, 4, 2, 400000),              -- Đồ chơi cho chó
(6, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(6, 10, 1, 1450000),            -- Lồng mèo cao cấp
(7, 6, 1, 2250000),             -- Lồng chim cao cấp
(8, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(9, 7, 2, 500000),              -- Thức ăn cho chó
(10, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(10, 5, 1, 850000),             -- Nhà cho mèo
(11, 4, 3, 600000),             -- Đồ chơi cho chó
(12, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(13, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(14, 11, 1, 1190000),           -- Balo vận chuyển thú cưng
(15, 10, 1, 1450000),           -- Lồng mèo cao cấp
(16, 6, 1, 2250000),            -- Lồng chim cao cấp
(16, 13, 2, 300000);            -- Thuốc tẩy giun cho mèo

-- Cấu trúc bảng cho bảng ctpn
CREATE TABLE ctpn (
    MaPN int NOT NULL,
    MaTTSP int NOT NULL,
    GiaNhap int NOT NULL,
    SoLuong int NOT NULL,
    ThanhTien int NULL,
    PRIMARY KEY (MaPN, MaTTSP)
);

-- Đang đổ dữ liệu cho bảng ctpn (đã điều chỉnh giá nhập và số lượng phù hợp với sản phẩm thú cưng)
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(1, 1, 50000, 50, 2500000),       -- Cát vệ sinh cho mèo
(1, 6, 1700000, 10, 17000000),    -- Lồng chim cao cấp
(2, 1, 50000, 30, 1500000),       -- Cát vệ sinh cho mèo
(2, 6, 1700000, 5, 8500000),      -- Lồng chim cao cấp
(2, 10, 1000000, 20, 20000000),   -- Lồng mèo cao cấp
(3, 5, 600000, 15, 9000000),      -- Nhà cho mèo
(3, 11, 800000, 12, 9600000),     -- Balo vận chuyển thú cưng
(4, 3, 100000, 100, 10000000),    -- Thức ăn cho mèo cao cấp
(4, 7, 200000, 50, 10000000),     -- Thức ăn cho chó
(5, 4, 150000, 30, 4500000),      -- Đồ chơi cho chó
(5, 13, 120000, 50, 6000000);     -- Thuốc tẩy giun cho mèo


-- Cấu trúc bảng cho bảng giaohang
CREATE TABLE giaohang (
    MaHD int NOT NULL,
    MaNV int NULL,
    TinhTrangGH int NOT NULL
);

-- Đang đổ dữ liệu cho bảng giaohang
INSERT INTO giaohang (MaHD, MaNV, TinhTrangGH) VALUES
(4, 2, 1),  -- Đơn hàng thức ăn mèo và lồng mèo đã giao thành công
(5, NULL, 0),  -- Đơn hàng cát vệ sinh và đồ chơi chó đang chờ giao
(13, 6, 1),  -- Đơn hàng thức ăn mèo cao cấp đã giao thành công
(15, 5, 1),  -- Đơn hàng lồng mèo cao cấp đã giao thành công
(1, 5, 1),  -- Đơn hàng thức ăn mèo đã giao thành công
(16, 5, 1);  -- Đơn hàng lồng chim và thuốc tẩy giun đã giao thành công


-- Cấu trúc bảng cho bảng giohang
CREATE TABLE giohang (
    MaTTSP int NOT NULL,
    SoLuong int NOT NULL,
    MaKH int NOT NULL,
    PRIMARY KEY (MaTTSP, MaKH)
);

-- Đang đổ dữ liệu cho bảng giohang
INSERT INTO giohang (MaTTSP, SoLuong, MaKH) VALUES
(6, 1, 2),   -- Khách hàng 2 đang có 1 lồng chim cao cấp trong giỏ hàng
(11, 2, 1),  -- Khách hàng 1 đang có 2 balo vận chuyển thú cưng trong giỏ hàng
(13, 3, 2),  -- Khách hàng 2 đang có 3 thuốc tẩy giun cho mèo trong giỏ hàng
(1, 2, 3),   -- Khách hàng 3 đang có 2 gói cát vệ sinh cho mèo trong giỏ hàng
(3, 1, 1),   -- Khách hàng 1 đang có 1 gói thức ăn cho mèo cao cấp trong giỏ hàng
(5, 1, 4),   -- Khách hàng 4 đang có 1 nhà cho mèo trong giỏ hàng
(7, 2, 3);   -- Khách hàng 3 đang có 2 gói thức ăn cho chó trong giỏ hàng


-- Cấu trúc bảng cho bảng hoadon
CREATE TABLE hoadon (
    MaHD int IDENTITY(1,1) NOT NULL,
    MaKH int NOT NULL,
    MaKM int NULL,
    DiaChiGH nvarchar(max) NULL,
    TongTienTT int NOT NULL,
    NgayLapHD date NOT NULL,
    TinhTrangHD int NOT NULL,
    TinhTrangTT int NULL,
    SoTienNhan int NULL,
    SoTienTra int NULL,
    PRIMARY KEY (MaHD)
);

-- Đang đổ dữ liệu cho bảng hoadon
SET IDENTITY_INSERT hoadon ON;
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(1, 1, 1, N'HCM', 270000, '2023-12-23', 1, 1, 270000, 0), -- Thức ăn cho mèo và phụ kiện
(4, 4, NULL, N'Đồng Nai', 664500, '2024-01-01', 1, 1, 700000, 35500), -- Lồng mèo cao cấp và thức ăn
(5, 4, NULL, N'Q1, HCM', 418000, '2024-01-02', 0, 0, 0, -418000), -- Cát vệ sinh và đồ chơi cho chó
(6, 2, NULL, N'Q1, HCM', 664500, '2024-01-02', 0, 0, 0, 0), -- Balo vận chuyển thú cưng và nhà cho mèo
(7, 4, 6, N'Thủ Đức, HCM', 207000, '2024-01-08', 0, 0, 0, 0), -- Thức ăn cho chó và vòng cổ
(8, 1, NULL, N'Q1, HCM', 270000, '2024-04-08', 1, 1, 270000, 0), -- Thuốc tẩy giun và vitamin cho mèo
(9, 2, NULL, N'Thủ Đức, Hồ Chí Min', 150000, '2024-04-15', 1, 1, 150000, 0), -- Cát vệ sinh cho mèo
(10, 4, NULL, N'Q9, Hồ Chí Minh', 355000, '2023-12-16', 1, 1, 355000, 0), -- Thức ăn cho chó lớn
(11, 2, NULL, N'Q7, HCM', 180000, '2024-10-17', 1, 1, 180000, 0), -- Đồ chơi và khay vệ sinh cho mèo
(12, 2, 6, N'Q1, HCM', 248400, '2024-01-10', 0, 0, 0, 0), -- Lồng chim và thức ăn cho chim
(13, 1, 6, N'Linh trung, Thủ Đức, HCM', 248400, '2024-01-11', 1, 0, 0, 0), -- Thức ăn cao cấp cho mèo
(14, 1, 6, N'Q1, HCM', 110308, '2024-01-11', 0, 0, 0, 0), -- Thuốc trị ve rận và sữa tắm cho chó
(15, 2, 6, N'Đồng Nai', 362940, '2024-11-02', 1, 1, 362940, 0), -- Lồng mèo cao cấp và phụ kiện
(16, 2, 6, N'Thủ Đức, HCM', 344908, '2024-01-13', 1, 1, 344908, 0); -- Lồng chim và thuốc tẩy giun
SET IDENTITY_INSERT hoadon OFF;


-- Cấu trúc bảng cho bảng khachhang
CREATE TABLE khachhang (
    MaKH int IDENTITY(1,1) NOT NULL,
    TenKH nvarchar(50) NOT NULL,
    GioiTinh nchar(10) NOT NULL,
    SDT nchar(12) NOT NULL,
    Email nvarchar(50) NOT NULL,
    MatKhau nvarchar(30) NOT NULL,
    CMND nchar(12) NOT NULL,
    DiaChi nvarchar(50) NULL,
    LoaiKH nvarchar(50) NOT NULL,
    PRIMARY KEY (MaKH)
);

-- Đang đổ dữ liệu cho bảng khachhang
SET IDENTITY_INSERT khachhang ON;
INSERT INTO khachhang (MaKH, TenKH, GioiTinh, SDT, Email, MatKhau, CMND, DiaChi, LoaiKH) VALUES
(1, N'Nguyễn Anh Dũng', N'Nam', N'000011112222', N'AnhDung@gmail.com', N'123456', N'272811122', N'Thủ đức, HCM', N'Khách VIP'),
(2, N'Nguyễn Đăng Khoa', N'Nam', N'033112223', N'DangKhoa@gmail.com', N'123456', N'271829304', N'Q1, HCM', N'Khách thường xuyên'),
(3, N'Trần Minh Tuấn', N'Nam', N'0909123456', N'MinhTuan@gmail.com', N'123456', N'272123456', N'Q7, HCM', N'Khách mới'),
(4, N'Đỗ Thị Mỹ Duyên', N'Nữ', N'09811112223', N'MyDuyen@gmail.com', N'123456', N'291112233', N'Q8, HCM', N'Khách thường xuyên'),
(5, N'Lê Hoàng Nam', N'Nam', N'0912345678', N'HoangNam@gmail.com', N'123456', N'273456789', N'Q2, HCM', N'Khách VIP'),
(6, N'Phạm Thị Thanh Hà', N'Nữ', N'0987654321', N'ThanhHa@gmail.com', N'123456', N'291234567', N'Bình Thạnh, HCM', N'Khách mới');
SET IDENTITY_INSERT khachhang OFF;

-- Cấu trúc bảng cho bảng khuyenmai
CREATE TABLE khuyenmai (
    MaKM int IDENTITY(1,1) NOT NULL,
    SoPTKM tinyint NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NOT NULL,
    TTienToiThieu int NULL,
    PRIMARY KEY (MaKM)
);

-- Đang đổ dữ liệu cho bảng khuyenmai
SET IDENTITY_INSERT khuyenmai ON;
INSERT INTO khuyenmai (MaKM, SoPTKM, TuNgay, DenNgay, TTienToiThieu) VALUES
(1, 10, '2024-11-01', '2024-11-30', 100000),
(2, 15, '2024-11-08', '2024-11-15', 200000),
(3, 12, '2024-11-26', '2024-12-01', 150000),
(4, 20, '2024-11-30', '2024-12-31', 300000),
(5, 8, '2024-12-15', '2024-12-25', 250000),
(6, 10, '2025-01-01', '2025-01-31', 180000);
SET IDENTITY_INSERT khuyenmai OFF;

-- Cấu trúc bảng cho bảng loaisp
CREATE TABLE loaisp (
    MaLoaiSP int IDENTITY(1,1) NOT NULL,
    TenLoaiSP nvarchar(100) NOT NULL,
    PRIMARY KEY (MaLoaiSP)
);

-- Đang đổ dữ liệu cho bảng loaisp
SET IDENTITY_INSERT loaisp ON;
INSERT INTO loaisp (MaLoaiSP, TenLoaiSP) VALUES
(1, N'Thức ăn cho chó'),
(2, N'Thức ăn cho mèo'),
(3, N'Phụ kiện thú cưng'),
(4, N'Đồ chơi thú cưng'),
(5, N'Thuốc và chăm sóc sức khỏe'),
(6, N'Chuồng, nhà và nội thất'),
(7, N'Vệ sinh và làm sạch'),
(8, N'Thức ăn cho cá cảnh'),
(9, N'Lồng và phụ kiện vận chuyển'),
(10, N'Thuốc phòng và trị bệnh'),
(11, N'Quần áo thú cưng');
SET IDENTITY_INSERT loaisp OFF;

-- Cấu trúc bảng cho bảng nhacc
CREATE TABLE nhacc (
    MaNCC int IDENTITY(1,1) NOT NULL,
    TenNCC nvarchar(50) NOT NULL,
    Email nvarchar(50) NOT NULL,
    SDT nchar(12) NOT NULL,
    DiaChi nvarchar(50) NULL,
    Website nvarchar(191) NULL,
    PRIMARY KEY (MaNCC)
);

-- Đang đổ dữ liệu cho bảng nhacc
SET IDENTITY_INSERT nhacc ON;
INSERT INTO nhacc (MaNCC, TenNCC, Email, SDT, DiaChi, Website) VALUES
(1, N'Royal Canin', N'contact@royalcanin.vn', N'0283123456', N'32 Cách Mạng Tháng 8, P. 6, Quận 3, TP HCM', N'https://www.royalcanin.com/vn'),
(2, N'Pedigree', N'cskh@pedigree.com.vn', N'0283456789', N'49 Nguyễn Văn Bá, KP3, P. Bình Thọ, Q. Thủ Đức', N'https://www.pedigree.com.vn'),
(3, N'Whiskas', N'info@whiskas.vn', N'0283789123', N'65A Hồ Xuân Hương, Phường 6, Quận 3, Tp.HCM', N'https://www.whiskas.vn'),
(4, N'Pet Accessories', N'sales@petacc.vn', N'0283456999', N'32 Lê Lợi, P. Bến Nghé, Quận 1, TP HCM', N'https://www.petacc.vn'),
(5, N'VetCare', N'info@vetcare.vn', N'0243921456', N'Cầu Giấy, Hà Nội', N'https://vetcare.vn'),
(7, N'PetToys Vietnam', N'sales@pettoys.vn', N'0243712345', N'Thanh Xuân, Hà Nội', N'https://pettoys.vn');
SET IDENTITY_INSERT nhacc OFF;


-- Cấu trúc bảng cho bảng nhanvien
CREATE TABLE nhanvien (
    MaNV int IDENTITY(1,1) NOT NULL,
    TenNV nvarchar(30) NOT NULL,
    NgayVL date NOT NULL,
    Luong int NOT NULL,
    SDT nchar(12) NOT NULL,
    Email nvarchar(50) NOT NULL,
    MatKhau nvarchar(30) NOT NULL,
    CMND nchar(12) NOT NULL,
    DiaChi nvarchar(50) NOT NULL,
    LoaiNV nvarchar(20) NOT NULL,
    PRIMARY KEY (MaNV)
);

-- Đang đổ dữ liệu cho bảng nhanvien
SET IDENTITY_INSERT nhanvien ON;
INSERT INTO nhanvien (MaNV, TenNV, NgayVL, Luong, SDT, Email, MatKhau, CMND, DiaChi, LoaiNV) VALUES
(2, N'Mai Văn Tiến', '2022-11-12', 7000000, N'0339004003', N'MVT@gmail.com', N'22222222', N'0455634579', N'Thủ Dức - Tp HCM', N'Bán hàng'),
(3, N'Nguyễn Thị Mai', '2022-11-15', 7000000, N'0339004345', N'NTM@gmail.com', N'33333333', N'0345942456', N' Quận 9 - Tp HCM', N'Tiếp tân'),
(4, N'Nguyễn Thị Thu', '2023-11-20', 7000000, N'0339493345', N'NTT@gmail.com', N'44444444', N'0445310024', N' Quận 9 - Tp HCM', N'Chăm sóc thú cưng'),
(5, N'Nguyễn Văn Quang', '2023-12-02', 6000000, N'0945600342', N'NVQ@gmail.com', N'55555555', N'0542247211', N' Quận 2 - Tp HCM', N'Giao hàng'),
(6, N'Nguyễn Hữu Thắng', '2021-12-21', 40000000, N'0123124123', N'thang@gm.com', N'123456', N'214213214', N'ABC', N'Quản lý'),
(7, N'Trần Minh Đức', '2023-01-15', 9000000, N'0912749213', N'duc@gmail.com', N'11111111', N'214213214', N'Quận 7 - Tp HCM', N'Bác sĩ thú y'),
(8, N'Hoàng Trí Tâm', '2022-01-12', 10000000, N'0379696428', N'hoangtam704@gmail.com', N'11111111', N'272801851', N'Đồng Nai', N'Quản lý'),
(9, N'Lê Thị Hương', '2023-05-10', 8000000, N'0387654321', N'huong@gmail.com', N'99999999', N'036198765', N'Bình Thạnh - Tp HCM', N'Huấn luyện thú cưng');
SET IDENTITY_INSERT nhanvien OFF;


-- Cấu trúc bảng cho bảng phieunhap
CREATE TABLE phieunhap (
    MaPN int IDENTITY(1,1) NOT NULL,
    TongTienTT int NOT NULL,
    NgayLapPN date NOT NULL,
    TinhTrangTT int NOT NULL,
    MaNCC int NOT NULL,
    MaNV int NOT NULL,
    PRIMARY KEY (MaPN)
);

-- Đang đổ dữ liệu cho bảng phieunhap
SET IDENTITY_INSERT phieunhap ON;
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(1, 12700000, '2023-12-23', 0, 1, 7),
(2, 8450000, '2023-11-20', 0, 2, 8),
(3, 15600000, '2024-03-24', 0, 3, 6),
(4, 7300000, '2024-04-15', 1, 4, 6),
(5, 9800000, '2024-05-02', 0, 5, 8);
SET IDENTITY_INSERT phieunhap OFF;


-- Cấu trúc bảng cho bảng thongtinsp

CREATE TABLE thongtinsp (
    MaTTSP int IDENTITY(1,1) NOT NULL,
    TenSP nvarchar(100) NOT NULL,
    HinhAnh nvarchar(191) NOT NULL,
    MaLoaiSP int NOT NULL,
    MaTH int NOT NULL,
    TenLoaiSP nvarchar(100) NOT NULL,  -- Thuộc tính mới thêm
    TenTH nvarchar(100) NOT NULL,      -- Thuộc tính mới thêm
    Gia int NOT NULL,
    GiaKM int NOT NULL,
    SoLuong int NULL,
    MauSac nvarchar(20) NULL,
    KhoiLuong float NULL,
    DoTuoi nvarchar(20) NULL,
    XuatXu nvarchar(50) NULL,
    KichThuoc nvarchar(30) NULL,
    ThanhPhan nvarchar(100) NULL,
    CongDung nvarchar(100) NULL,
    HuongDanSD nvarchar(200) NULL,
    PRIMARY KEY (MaTTSP)
);

-- Đang đổ dữ liệu cho bảng thongtinsp
SET IDENTITY_INSERT thongtinsp ON;
INSERT INTO thongtinsp (MaTTSP, TenSP, HinhAnh, MaLoaiSP, MaTH, TenLoaiSP, TenTH, Gia, GiaKM, SoLuong, MauSac, KhoiLuong, DoTuoi, XuatXu, KichThuoc, ThanhPhan, CongDung, HuongDanSD) VALUES
(1, N'Royal Canin Mini Adult', N'https://tse4.mm.bing.net/th?id=OIP.s40KU-LBNlocxmifhLaLxAHaHd&pid=Api&P=0&h=220', 1, 1, N'Thức ăn cho chó', N'Royal Canin', 200000, 180000, 44, N'Nâu', 1, N'Trưởng thành', N'Pháp', N'Hạt nhỏ', N'Thịt gà, gạo, ngô, chất béo động vật', N'Thức ăn cho chó trưởng thành cỡ nhỏ', N'Cho ăn 2 lần/ngày theo bảng định lượng'),
(2, N'Pedigree Adult vị bò và rau củ', N'https://petshopsaigon.vn/wp-content/uploads/2021/11/thuc-an-cho-cho-pedigree-vi-bo-va-rau-cu-1.jpg', 1, 2, N'Thức ăn cho chó', N'Pedigree', 309000, 299000, 20, N'Đỏ', 1.5, N'Trưởng thành', N'Thái Lan', N'Hạt vừa', N'Thịt bò, ngũ cốc, rau củ', N'Thức ăn cho chó trưởng thành', N'Cho ăn 2-3 lần/ngày theo cân nặng'),
(3, N'Whiskas vị cá ngừ', N'https://petshopsaigon.vn/wp-content/uploads/2021/12/thuc-an-cho-meo-whiskas-vi-ca-ngu-tui-1.2kg.jpg', 2, 3, N'Thức ăn cho mèo', N'Whiskas', 279000, 270000, 38, N'Tím', 1.2, N'Mọi lứa tuổi', N'Thái Lan', N'Hạt nhỏ', N'Cá ngừ, ngũ cốc, dầu cá', N'Thức ăn cho mèo mọi lứa tuổi', N'Cho ăn tự do hoặc 2-3 lần/ngày'),
(4, N'Lược chải lông cho chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2021/10/luoc-chai-long-cho-cho-meo.jpg', 3, 4, N'Phụ kiện thú cưng', N'Furminator', 40000, 0, 22, N'Xanh', 0.1, N'Mọi lứa tuổi', N'Trung Quốc', N'20x10 cm', N'Nhựa, kim loại', N'Chải lông, loại bỏ lông rụng', N'Chải nhẹ nhàng theo chiều mọc lông'),
(5, N'Cát vệ sinh mèo Catsan', N'https://petshopsaigon.vn/wp-content/uploads/2022/02/cat-ve-sinh-meo-catsan.jpg', 4, 5, N'Đồ chơi thú cưng', N'Catsan', 89999, 85000, 42, N'Trắng', 5, N'Mọi lứa tuổi', N'Đức', N'Túi 5kg', N'Đất sét tự nhiên', N'Hút ẩm, khử mùi hiệu quả', N'Thay cát 1-2 lần/tuần'),
(6, N'Vòng cổ chó có chuông', N'https://petshopsaigon.vn/wp-content/uploads/2022/03/vong-co-cho-co-chuong.jpg', 3, 6, N'Phụ kiện thú cưng', N'Ferplast', 59999, 50000, 26, N'Đỏ', 0.05, N'Trưởng thành', N'Việt Nam', N'Size S, M, L', N'Da tổng hợp, kim loại', N'Trang trí, nhận diện', N'Điều chỉnh vừa vặn, không quá chật'),
(7, N'Xương gặm sạch răng cho chó', N'https://petshopsaigon.vn/wp-content/uploads/2021/09/xuong-gam-sach-rang-cho-cho.jpg', 5, 7, N'Thuốc và chăm sóc sức khỏe', N'Whimzees', 59000, 50000, 21, N'Trắng', 0.1, N'Trưởng thành', N'Mỹ', N'Size M', N'Tinh bột ngô, protein', N'Làm sạch răng, giảm cao răng', N'Cho chó gặm 1 cái/ngày'),
(8, N'Đồ chơi chuột giả cho mèo', N'https://petshopsaigon.vn/wp-content/uploads/2021/08/do-choi-chuot-gia-cho-meo.jpg', 6, 8, N'Chuồng, nhà và nội thất', N'Kong', 32000, 0, 11, N'Xám', 0.03, N'Mọi lứa tuổi', N'Trung Quốc', N'7x3 cm', N'Vải nhung, catnip', N'Kích thích vận động cho mèo', N'Cho mèo chơi dưới sự giám sát'),
(9, N'Sữa tắm dưỡng lông cho chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2021/11/sua-tam-duong-long-cho-cho-meo.jpg', 7, 9, N'Vệ sinh và làm sạch', N'Bio-Groom', 90000, 0, 40, N'Hồng', 0.5, N'Trên 2 tháng', N'Thái Lan', N'Chai 500ml', N'Chiết xuất lô hội, vitamin E', N'Làm sạch, dưỡng lông mềm mượt', N'Pha loãng với nước ấm, tắm 2 lần/tháng'),
(10, N'Thức ăn cho cá cảnh Tetra', N'https://petshopsaigon.vn/wp-content/uploads/2022/01/thuc-an-cho-ca-canh-tetra.jpg', 8, 10, N'Thức ăn cho cá cảnh', N'Tetra', 45000, 0, 10, N'Đa màu', 0.1, N'Mọi lứa tuổi', N'Đức', N'Hộp 100g', N'Bột cá, tảo, vitamin', N'Thức ăn cho cá cảnh nhỏ', N'Cho ăn 2-3 lần/ngày, lượng vừa phải'),
(11, N'Lồng vận chuyển chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2022/02/long-van-chuyen-cho-meo.jpg', 9, 11, N'Lồng và phụ kiện vận chuyển', N'Savic', 299000, 279000, 19, N'Xanh dương', 1.5, N'Mọi lứa tuổi', N'Việt Nam', N'60x40x40 cm', N'Nhựa ABS cao cấp', N'Vận chuyển an toàn', N'Đặt thêm khăn mềm bên trong'),
(12, N'Royal Canin Kitten', N'https://petshopsaigon.vn/wp-content/uploads/2021/12/royal-canin-kitten.jpg', 2, 1, N'Thức ăn cho mèo', N'Royal Canin', 269000, 249000, 50, N'Nâu đỏ', 2, N'2-12 tháng', N'Pháp', N'Hạt nhỏ', N'Thịt gia cầm, cá, dầu cá', N'Thức ăn cho mèo con', N'Ngâm với nước ấm cho mèo dưới 3 tháng'),
(13, N'Pate cho chó Pedigree vị gà', N'https://petshopsaigon.vn/wp-content/uploads/2021/10/pate-cho-cho-pedigree-vi-ga.jpg', 1, 2, N'Thức ăn cho chó', N'Pedigree', 169000, 149000, 99, N'Vàng', 0.4, N'Trưởng thành', N'Thái Lan', N'Hộp 400g', N'Thịt gà, gan, rau củ', N'Thức ăn ướt cho chó', N'Cho ăn kèm với thức ăn khô'),
(14, N'Thuốc nhỏ gáy trị ve rận cho chó', N'https://petshopsaigon.vn/wp-content/uploads/2021/09/thuoc-nho-gay-tri-ve-ran-cho-cho.jpg', 10, 12, N'Thuốc phòng và trị bệnh', N'Frontline', 169000, 149000, 100, N'Xanh lá', 0.05, N'Trên 3 tháng', N'Pháp', N'Tuýp 1ml', N'Fipronil, S-methoprene', N'Phòng và trị ve, rận, bọ chét', N'Nhỏ vào gáy, tránh tắm trong 48h'),
(15, N'Bát ăn đôi cho chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2021/08/bat-an-doi-cho-cho-meo.jpg', 9, 13, N'Lồng và phụ kiện vận chuyển', N'Trixie', 149000, 0, 100, N'Đen', 0.3, N'Mọi lứa tuổi', N'Việt Nam', N'30x15x5 cm', N'Nhựa PP an toàn', N'Đựng thức ăn và nước', N'Rửa sạch sau mỗi lần sử dụng'),
(16, N'Dầu tắm trị ve rận cho chó', N'https://petshopsaigon.vn/wp-content/uploads/2021/11/dau-tam-tri-ve-ran-cho-cho.jpg', 7, 14, N'Vệ sinh và làm sạch', N'Beaphar', 179000, 159000, 100, N'Trắng', 0.5, N'Trên 3 tháng', N'Mỹ', N'Chai 500ml', N'Permethrin, dầu bạc hà', N'Diệt ve, rận, bọ chét', N'Pha loãng với nước ấm, tắm 2 tuần/lần'),
(17, N'Cát vệ sinh mèo đậu phụ', N'https://petshopsaigon.vn/wp-content/uploads/2022/03/cat-ve-sinh-meo-dau-phu.jpg', 4, 15, N'Đồ chơi thú cưng', N'Tofu', 239000, 0, 100, N'Vàng nhạt', 6, N'Mọi lứa tuổi', N'Nhật Bản', N'Túi 6kg', N'Đậu nành tự nhiên', N'Hút ẩm, khử mùi, có thể xả toilet', N'Thay cát khi bị bẩn hoặc có mùi'),
(18, N'Nhà cây cho mèo', N'https://petshopsaigon.vn/wp-content/uploads/2022/01/nha-cay-cho-meo.jpg', 9, 16, N'Lồng và phụ kiện vận chuyển', N'Cat Tree', 1399000, 0, 100, N'Xám', 10, N'Mọi lứa tuổi', N'Trung Quốc', N'60x40x120 cm', N'Gỗ, vải nhung, dây sisal', N'Nơi nghỉ ngơi, cào móng cho mèo', N'Đặt ở góc phòng thoáng mát'),
(19, N'Áo cho chó size nhỏ', N'https://petshopsaigon.vn/wp-content/uploads/2021/10/ao-cho-cho-size-nho.jpg', 11, 17, N'Quần áo thú cưng', N'Petkit', 99000, 89000, 105, N'Hồng', 0.1, N'Mọi lứa tuổi', N'Việt Nam', N'Size S, M', N'Cotton mềm', N'Giữ ấm, trang trí', N'Giặt tay với nước lạnh'),
(20, N'Vòng cổ chống ve rận cho mèo', N'https://petshopsaigon.vn/wp-content/uploads/2021/12/vong-co-chong-ve-ran-cho-meo.jpg', 10, 18, N'Thuốc phòng và trị bệnh', N'Bayer', 79000, 0, 76, N'Xanh dương', 0.05, N'Trên 3 tháng', N'Đức', N'Size S', N'Silicon, tinh dầu thiên nhiên', N'Đuổi ve, rận, bọ chét', N'Đeo liên tục, thay mới sau 3 tháng'),
(21, N'Cỏ mèo khô Catnip', N'https://petshopsaigon.vn/wp-content/uploads/2021/08/co-meo-kho-catnip.jpg', 6, 19, N'Chuồng, nhà và nội thất', N'Catnip Garden', 42000, 0, 56, N'Xanh', 0.05, N'Trưởng thành', N'Mỹ', N'Gói 10g', N'100% cỏ catnip tự nhiên', N'Kích thích vui chơi cho mèo', N'Rắc lên đồ chơi hoặc nơi nghỉ ngơi'),
(22, N'Thức ăn cho chim cảnh', N'https://petshopsaigon.vn/wp-content/uploads/2022/02/thuc-an-cho-chim-canh.jpg', 8, 20, N'Thức ăn cho cá cảnh', N'Bird Food', 54000, 0, 80, N'Đa màu', 0.5, N'Mọi lứa tuổi', N'Việt Nam', N'Túi 500g', N'Hạt kê, ngũ cốc, vitamin', N'Thức ăn cho chim cảnh nhỏ', N'Cho ăn 2 lần/ngày'),
(23, N'Vitamin tổng hợp cho chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2022/01/vitamin-tong-hop-cho-cho-meo.jpg', 10, 21, N'Thuốc phòng và trị bệnh', N'Pet Vitamin', 159000, 0, 120, N'Trắng', 0.2, N'Trên 2 tháng', N'Mỹ', N'Hộp 100 viên', N'Vitamin A, D, E, canxi', N'Bổ sung dinh dưỡng', N'1 viên/10kg cân nặng/ngày'),
(24, N'Khay vệ sinh cho mèo có nắp đậy', N'https://petshopsaigon.vn/wp-content/uploads/2021/09/khay-ve-sinh-cho-meo-co-nap-day.jpg', 4, 22, N'Đồ chơi thú cưng', N'Catidea', 259000, 239000, 76, N'Xanh lá', 1.5, N'Mọi lứa tuổi', N'Việt Nam', N'50x40x40 cm', N'Nhựa PP an toàn', N'Khay vệ sinh kín mùi', N'Đặt ở nơi yên tĩnh, riêng tư'),
(25, N'Dây dắt chó tự động', N'https://petshopsaigon.vn/wp-content/uploads/2021/11/day-dat-cho-tu-dong.jpg', 3, 23, N'Phụ kiện thú cưng', N'Flexi', 149000, 129000, 90, N'Đen', 0.3, N'Trưởng thành', N'Trung Quốc', N'5m', N'Dây nylon, nhựa ABS', N'Dắt chó đi dạo', N'Có thể khóa dây ở độ dài mong muốn'),
(26, N'Pate cho mèo Me-O vị cá ngừ', N'https://petshopsaigon.vn/wp-content/uploads/2021/12/pate-cho-meo-me-o-vi-ca-ngu.jpg', 2, 24, N'Thức ăn cho mèo', N'Me-O', 19000, 17000, 94, N'Xanh', 0.08, N'Mọi lứa tuổi', N'Thái Lan', N'Gói 80g', N'Cá ngừ, nước sốt', N'Thức ăn ướt cho mèo', N'Cho ăn 2-3 gói/ngày tùy cân nặng'),
(27, N'Thuốc nhỏ mắt cho chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2022/03/thuoc-nho-mat-cho-cho-meo.jpg', 10, 25, N'Thuốc phòng và trị bệnh', N'Ophthalmic', 94000, 0, 90, N'Trắng', 0.02, N'Mọi lứa tuổi', N'Pháp', N'Lọ 10ml', N'Nước muối sinh lý, vitamin A', N'Làm sạch, dưỡng ẩm mắt', N'Nhỏ 1-2 giọt/mắt, ngày 2 lần'),
(28, N'Xương gặm làm sạch răng cho chó', N'https://petshopsaigon.vn/wp-content/uploads/2022/02/xuong-gam-lam-sach-rang-cho-cho.jpg', 5, 26, N'Thuốc và chăm sóc sức khỏe', N'Dental Stick', 109000, 99000, 78, N'Xanh mint', 0.2, N'Trưởng thành', N'Mỹ', N'Size L', N'Tinh bột, protein thực vật', N'Làm sạch răng, giảm hôi miệng', N'Cho chó gặm dưới sự giám sát'),
(29, N'Thức ăn cho cá Koi', N'https://petshopsaigon.vn/wp-content/uploads/2021/10/thuc-an-cho-ca-koi.jpg', 8, 27, N'Thức ăn cho cá cảnh', N'Hikari', 139000, 129000, 85, N'Đa màu', 1, N'Mọi lứa tuổi', N'Nhật Bản', N'Túi 1kg', N'Bột cá, tảo spirulina, vitamin', N'Thức ăn nổi cho cá Koi', N'Cho ăn 2-3 lần/ngày, lượng vừa phải'),
(30, N'Balo vận chuyển chó mèo', N'https://petshopsaigon.vn/wp-content/uploads/2021/09/balo-van-chuyen-cho-meo.jpg', 9, 28, N'Lồng và phụ kiện vận chuyển', N'Pet Carrier', 390000, 0, 50, N'Hồng', 0.8, N'Dưới 5kg', N'Trung Quốc', N'40x30x45 cm', N'Vải canvas, lưới thoáng khí', N'Vận chuyển thú cưng nhỏ', N'Đặt thêm đệm mềm bên trong');
SET IDENTITY_INSERT thongtinsp OFF;



-- Cấu trúc bảng cho bảng thuonghieu
CREATE TABLE thuonghieu (
    MaTH int IDENTITY(1,1) NOT NULL,
    TenTH nvarchar(100) NOT NULL,
    PRIMARY KEY (MaTH)
);

-- Đang đổ dữ liệu cho bảng thuonghieu
SET IDENTITY_INSERT thuonghieu ON;
INSERT INTO thuonghieu (MaTH, TenTH) VALUES
(1, N'Royal Canin'),
(2, N'Pedigree'),
(3, N'Whiskas'),
(4, N'Furminator'),
(5, N'Catsan'),
(6, N'Ferplast'),
(7, N'Whimzees'),
(8, N'Kong'),
(9, N'Bio-Groom'),
(10, N'Tetra'),
(11, N'Savic'),
(12, N'Frontline'),
(13, N'Trixie'),
(14, N'Beaphar'),
(15, N'Tofu'),
(16, N'Cat Tree'),
(17, N'Petkit'),
(18, N'Bayer'),
(19, N'Catnip Garden'),
(20, N'Bird Food'),
(21, N'Pet Vitamin'),
(22, N'Catidea'),
(23, N'Flexi'),
(24, N'Me-O'),
(25, N'Ophthalmic'),
(26, N'Dental Stick'),
(27, N'Hikari'),
(28, N'Pet Carrier');
SET IDENTITY_INSERT thuonghieu OFF;

-- Cấu trúc bảng cho bảng tintuc
CREATE TABLE tintuc (
    MaTinTuc int IDENTITY(1,1) NOT NULL,
    HinhBia nvarchar(191) NULL,
    TieuDe nvarchar(200) NULL,
    NoiDung nvarchar(max) NULL,
    Link nvarchar(200) NULL,
    TrangThai int NOT NULL,
    PRIMARY KEY (MaTinTuc)
);

-- Đang đổ dữ liệu cho bảng tintuc
SET IDENTITY_INSERT tintuc ON;
INSERT INTO tintuc (MaTinTuc, HinhBia, TieuDe, NoiDung, Link, TrangThai) VALUES
(1, N'https://petshopsaigon.vn/wp-content/uploads/2023/05/cach-cham-soc-cho-con-moi-sinh.jpg', N'5 Bí quyết chăm sóc chó con mới sinh để bé khỏe mạnh và phát triển tốt', N'# 5 Bí quyết chăm sóc chó con mới sinh để bé khỏe mạnh và phát triển tốt

Chó con mới sinh rất yếu ớt và cần được chăm sóc đặc biệt trong những tuần đầu tiên của cuộc đời. Dưới đây là 5 bí quyết giúp bạn chăm sóc chó con mới sinh một cách hiệu quả.

## 1. Đảm bảo môi trường sống ấm áp và an toàn

Chó con mới sinh chưa thể tự điều chỉnh nhiệt độ cơ thể, vì vậy việc giữ ấm cho chúng rất quan trọng. Nhiệt độ lý tưởng cho chó sơ sinh là khoảng 29-32°C trong tuần đầu tiên, sau đó giảm dần xuống 26-27°C trong tuần thứ hai và ba.

Bạn có thể sử dụng:
- Đèn sưởi chuyên dụng (đặt ở khoảng cách an toàn)
- Thảm sưởi cho thú cưng
- Khăn mềm và ấm để lót ổ

Đảm bảo khu vực nuôi chó con không có gió lùa, không quá ẩm ướt và tránh xa các nguồn nước.

## 2. Dinh dưỡng đầy đủ và phù hợp

Sữa mẹ là nguồn dinh dưỡng tốt nhất cho chó con trong 3-4 tuần đầu đời. Nếu chó mẹ không đủ sữa hoặc không thể cho con bú, bạn cần:

- Sử dụng sữa thay thế dành riêng cho chó con (KHÔNG dùng sữa bò thông thường)
- Cho ăn bằng bình sữa chuyên dụng cho thú cưng
- Cho ăn mỗi 2-3 giờ trong tuần đầu tiên
- Từ tuần thứ 3-4, bắt đầu cho ăn dặm với thức ăn mềm, nghiền nhuyễn

Lưu ý: Cân nặng của chó con nên tăng đều đặn mỗi ngày. Nếu chó con không tăng cân hoặc giảm cân, hãy đưa đến bác sĩ thú y ngay lập tức.

## 3. Vệ sinh cơ thể thường xuyên

Chó mẹ thường liếm con để giúp chúng đi vệ sinh, nhưng nếu không có chó mẹ, bạn cần:

- Dùng khăn ấm, mềm lau nhẹ nhàng vùng bụng và hậu môn sau mỗi bữa ăn để kích thích đi vệ sinh
- Giữ cho chó con luôn sạch sẽ và khô ráo
- Thay đệm lót ổ thường xuyên
- Không tắm cho chó con trong 3-4 tuần đầu tiên

## 4. Theo dõi sức khỏe hàng ngày

Quan sát chó con mỗi ngày để phát hiện sớm các dấu hiệu bất thường:

- Kiểm tra mắt: không nên có ghèn nhiều hoặc sưng đỏ
- Kiểm tra mũi: nên khô và sạch, không chảy dịch
- Theo dõi phân: màu sắc, độ cứng mềm bất thường có thể là dấu hiệu của vấn đề tiêu hóa
- Quan sát hành vi: chó con khỏe mạnh sẽ ngủ nhiều nhưng khi thức sẽ năng động

Đưa chó con đến bác sĩ thú y để tiêm phòng và tẩy giun theo lịch.

## 5. Xã hội hóa sớm

Từ 3-12 tuần tuổi là giai đoạn quan trọng để xã hội hóa chó con:

- Cho chó con tiếp xúc với nhiều người khác nhau
- Làm quen dần với các âm thanh thường ngày
- Từ 8 tuần tuổi, cho tiếp xúc với các chó trưởng thành đã được tiêm phòng đầy đủ
- Dạy các kỹ năng cơ bản như đi vệ sinh đúng chỗ

Việc xã hội hóa sớm giúp chó con phát triển thành chó trưởng thành tự tin và thân thiện.

---

Tại PetShop Sài Gòn, chúng tôi cung cấp đầy đủ các sản phẩm chăm sóc chó con như sữa thay thế, bình sữa, thức ăn dặm, đệm lót ổ và nhiều phụ kiện khác. Hãy ghé thăm cửa hàng hoặc website của chúng tôi để được tư vấn chi tiết về cách chăm sóc thú cưng của bạn!', N'https://petshopsaigon.vn/tin-tuc/cham-soc-cho-con-moi-sinh', 1),
(2, N'https://petshopsaigon.vn/wp-content/uploads/2023/06/thuc-an-cho-meo-tot-nhat.jpg', N'Top 10 thức ăn cho mèo tốt nhất 2023 - Lựa chọn dinh dưỡng hàng đầu cho boss nhà bạn', N'# Top 10 thức ăn cho mèo tốt nhất 2023 - Lựa chọn dinh dưỡng hàng đầu cho boss nhà bạn

Việc lựa chọn thức ăn phù hợp cho mèo cưng là một trong những yếu tố quan trọng nhất để đảm bảo sức khỏe và tuổi thọ cho thú cưng của bạn. Dưới đây là danh sách 10 loại thức ăn cho mèo tốt nhất năm 2023, được đánh giá dựa trên thành phần dinh dưỡng, nguồn gốc nguyên liệu và phản hồi từ người dùng.

## 1. Royal Canin Indoor Adult

**Đặc điểm nổi bật:**
- Được thiết kế đặc biệt cho mèo sống trong nhà
- Giúp kiểm soát mùi phân và lông rụng
- Hỗ trợ duy trì cân nặng lý tưởng
- Công thức cân bằng protein và chất béo

**Phù hợp với:** Mèo trưởng thành sống trong nhà, mèo ít vận động

**Giá tham khảo:** 180.000đ - 850.000đ tùy khối lượng

## 2. Hill''s Science Diet Adult Indoor

**Đặc điểm nổi bật:**
- Giàu chất xơ tự nhiên giúp loại bỏ búi lông
- Công thức ít calo giúp duy trì cân nặng khỏe mạnh
- Bổ sung vitamin E và các chất chống oxy hóa
- Không chứa hương liệu, chất bảo quản nhân tạo

**Phù hợp với:** Mèo trưởng thành từ 1-6 tuổi, đặc biệt là mèo bị thừa cân

**Giá tham khảo:** 200.000đ - 900.000đ tùy khối lượng

## 3. Orijen Cat & Kitten

**Đặc điểm nổi bật:**
- Chứa 90% protein từ thịt động vật (gà, gà tây, cá, trứng)
- Công thức "Whole Prey" mô phỏng chế độ ăn tự nhiên của mèo
- Không chứa ngũ cốc, ít tinh bột
- Bổ sung DHA từ dầu cá hỗ trợ phát triển não bộ và thị lực

**Phù hợp với:** Mèo ở mọi lứa tuổi và giai đoạn phát triển

**Giá tham khảo:** 350.000đ - 1.500.000đ tùy khối lượng

## 4. Purina Pro Plan LiveClear

**Đặc điểm nổi bật:**
- Công nghệ độc đáo giúp giảm 47% dị nguyên trong lông mèo
- Giàu protein từ thịt gà thật
- Bổ sung prebiotic tự nhiên hỗ trợ tiêu hóa
- Công thức cân bằng pH giúp duy trì sức khỏe đường tiết niệu

**Phù hợp với:** Gia đình có người bị dị ứng với mèo

**Giá tham khảo:** 270.000đ - 1.200.000đ tùy khối lượng

## 5. Wellness CORE Grain-Free

**Đặc điểm nổi bật:**
- Không chứa ngũ cốc, phù hợp với chế độ ăn tự nhiên của mèo
- Giàu protein từ thịt gà, gà tây và cá
- Bổ sung probiotics hỗ trợ tiêu hóa
- Chứa axit béo omega từ dầu cá hồi và dầu hạt lanh

**Phù hợp với:** Mèo cần chế độ ăn không ngũ cốc, mèo bị dị ứng thực phẩm

**Giá tham khảo:** 300.000đ - 1.300.000đ tùy khối lượng

## 6. Blue Buffalo Wilderness

**Đặc điểm nổi bật:**
- Công thức giàu protein lấy cảm hứng từ chế độ ăn của mèo hoang dã
- Thịt gà thật là thành phần chính
- Bổ sung "LifeSource Bits" - hỗn hợp vitamin, khoáng chất và chất chống oxy hóa
- Không chứa ngũ cốc, thịt phụ phẩm, màu nhân tạo

**Phù hợp với:** Mèo năng động cần nhiều protein

**Giá tham khảo:** 280.000đ - 1.200.000đ tùy khối lượng

## 7. Acana Wild Atlantic

**Đặc điểm nổi bật:**
- Chứa 75% thành phần từ cá biển (cá trích, cá thu, cá tuyết, cá bơn...)
- Cá đánh bắt tự nhiên, nguyên liệu cấp độ con người
- Công thức ít carbohydrate
- Bổ sung rau củ quả tự nhiên cung cấp chất xơ

**Phù hợp với:** Mèo thích hương vị cá, mèo cần hỗ trợ da và lông

**Giá tham khảo:** 320.000đ - 1.400.000đ tùy khối lượng

## 8. Taste of the Wild Rocky Mountain

**Đặc điểm nổi bật:**
- Công thức không ngũ cốc với protein từ cá hồi và thịt thú săn
- Bổ sung hỗn hợp probiotics hỗ trợ hệ tiêu hóa
- Chứa các loại quả mọng giàu chất chống oxy hóa
- Axit béo omega-3 và omega-6 hỗ trợ da và lông khỏe mạnh

**Phù hợp với:** Mèo cần chế độ ăn tự nhiên, không ngũ cốc

**Giá tham khảo:** 250.000đ - 1.100.000đ tùy khối lượng

## 9. Instinct Original Grain-Free

**Đặc điểm nổi bật:**
- 81% thành phần từ động vật thực, 19% từ rau củ quả và các thành phần bổ sung
- Thịt gà thật là thành phần chính
- Phủ ngoài bằng dầu dừa và protein thô để tăng hương vị
- Không chứa ngũ cốc, khoai tây, đậu nành, chất bảo quản nhân tạo

**Phù hợp với:** Mèo kén ăn, mèo cần chế độ ăn tự nhiên

**Giá tham khảo:** 290.000đ - 1.250.000đ tùy khối lượng

## 10. Farmina N&D Quinoa

**Đặc điểm nổi bật:**
- Công thức độc đáo kết hợp protein động vật chất lượng cao với hạt quinoa
- 94% thành phần từ động vật
- Không chứa ngũ cốc thông thường (lúa mì, ngô)
- Có nhiều công thức đặc biệt hỗ trợ các vấn đề sức khỏe (da nhạy cảm, tiêu hóa...)

**Phù hợp với:** Mèo có nhu cầu dinh dưỡng đặc biệt, mèo bị dị ứng

**Giá tham khảo:** 320.000đ - 1.400.000đ tùy khối lượng

---

## Lưu ý khi chọn thức ăn cho mèo

1. **Tuổi tác và giai đoạn phát triển:** Mèo con, mèo trưởng thành và mèo già có nhu cầu dinh dưỡng khác nhau.

2. **Tình trạng sức khỏe:** Một số mèo cần chế độ ăn đặc biệt do các vấn đề sức khỏe như tiểu đường, bệnh thận, dị ứng...

3. **Mức độ hoạt động:** Mèo năng động cần nhiều calo hơn mèo ít vận động.

4. **Thành phần:** Ưu tiên thức ăn có protein động vật (thịt, cá) là thành phần chính.

5. **Chuyển đổi từ từ:** Khi thay đổi thức ăn, hãy chuyển đổi dần dần trong 7-10 ngày để tránh rối loạn tiêu hóa.

Tại PetShop Sài Gòn, chúng tôi cung cấp đầy đủ các loại thức ăn chất lượng cao cho mèo. Đội ngũ nhân viên của chúng tôi luôn sẵn sàng tư vấn giúp bạn lựa chọn loại thức ăn phù hợp nhất với nhu cầu của thú cưng. Ghé thăm cửa hàng hoặc website của chúng tôi ngay hôm nay!', N'https://petshopsaigon.vn/tin-tuc/thuc-an-cho-meo-tot-nhat', 1),
(3, N'https://petshopsaigon.vn/wp-content/uploads/2023/07/cach-tam-cho-cho-meo.jpg', N'Hướng dẫn chi tiết cách tắm cho chó mèo đúng cách tại nhà - An toàn và hiệu quả', N'# Hướng dẫn chi tiết cách tắm cho chó mèo đúng cách tại nhà - An toàn và hiệu quả

Tắm rửa là một phần quan trọng trong việc chăm sóc thú cưng. Tuy nhiên, không phải ai cũng biết cách tắm cho chó mèo đúng cách để đảm bảo an toàn và hiệu quả. Bài viết này sẽ hướng dẫn bạn chi tiết quy trình tắm cho cả chó và mèo tại nhà.

## Phần 1: Chuẩn bị trước khi tắm

### Dụng cụ cần chuẩn bị
- Sữa tắm chuyên dụng cho thú cưng (phù hợp với từng loại lông)
- Khăn tắm lớn (2-3 chiếc)
- Lược chải lông
- Bông gòn (để bảo vệ tai)
- Thảm chống trượt đặt trong bồn tắm
- Cốc hoặc vòi hoa sen cầm tay
- Máy sấy lông (tùy chọn)
- Đồ ăn thưởng (để khích lệ)

### Thời điểm tắm thích hợp
- **Chó:** Tùy theo giống và hoạt động, chó cần tắm 1-4 lần/tháng
- **Mèo:** 1-2 lần/tháng là đủ, mèo thường tự làm sạch
- Không tắm khi thú cưng đang đói hoặc vừa ăn xong
- Chọn thời điểm thú cưng bình tĩnh, không quá hưng phấn

### Chuẩn bị thú cưng
- Chải lông kỹ để loại bỏ lông rụng và gỡ các nút rối
- Kiểm tra và cắt móng nếu cần (tránh bị cào xước)
- Đặt bông gòn vào tai để ngăn nước (không nhét sâu)
- Cho thú cưng vận động nhẹ trước khi tắm để giảm stress

## Phần 2: Quy trình tắm cho chó

### Bước 1: Làm ướt lông
- Sử dụng nước ấm (37-38°C), không quá nóng hoặc quá lạnh
- Bắt đầu từ cổ và lưng, tránh vùng đầu và mặt
- Làm ướt từ từ đến chân và đuôi
- Nói chuyện nhẹ nhàng để trấn an chó

### Bước 2: Thoa sữa tắm
- Pha loãng sữa tắm theo hướng dẫn
- Thoa sữa tắm từ cổ xuống, xoa nhẹ nhàng tạo bọt
- Chú ý các vùng dễ bẩn: bụng, chân, đuôi
- Tránh để sữa tắm dính vào mắt và tai

### Bước 3: Rửa sạch
- Xả nước kỹ để loại bỏ hoàn toàn sữa tắm
- Kiểm tra kỹ các nếp gấp da và kẽ chân
- Nếu cần, lặp lại quá trình với vùng còn bẩn

### Bước 4: Làm sạch mặt
- Dùng khăn ẩm lau nhẹ vùng mặt
- Có thể dùng sữa tắm đặc biệt cho vùng mặt
- Cẩn thận khi lau quanh mắt và mũi

### Bước 5: Làm khô
- Dùng khăn thấm nước từ lông
- Với chó lông ngắn: có thể để tự khô sau khi lau
- Với chó lông dài: cần sấy khô với nhiệt độ thấp đến trung bình
- Chải lông trong quá trình sấy để tránh rối

## Phần 3: Quy trình tắm cho mèo

### Bước 1: Chuẩn bị môi trường
- Đóng cửa phòng tắm để tránh mèo chạy trốn
- Đặt thảm chống trượt trong bồn
- Chuẩn bị 2-3cm nước ấm trong bồn hoặc chậu

### Bước 2: Làm ướt lông
- Giữ mèo nhẹ nhàng nhưng chắc chắn
- Làm ướt từ cổ trở xuống, tránh đầu và mặt
- Nói giọng trấn an và khen ngợi mèo

### Bước 3: Thoa sữa tắm
- Sử dụng sữa tắm chuyên dụng cho mèo
- Thoa nhẹ nhàng và massage theo chiều mọc lông
- Tránh vùng mặt và tai

### Bước 4: Rửa sạch
- Xả nước thật kỹ, không để sữa tắm thừa
- Kiểm tra kỹ các vùng bụng, nách và đuôi

### Bước 5: Lau khô
- Bọc mèo trong khăn lớn, thấm nhẹ nhàng
- Giữ mèo ở nơi ấm áp, tránh gió lùa
- Với mèo lông dài: có thể cần sấy với nhiệt độ thấp
- Thưởng cho mèo sau khi tắm xong

## Phần 4: Lưu ý quan trọng

### Đối với chó
- Không tắm chó con dưới 8 tuần tuổi
- Chó già và chó có vấn đề về da cần sử dụng sữa tắm đặc biệt
- Một số giống chó như Husky, Samoyed cần chế độ tắm đặc biệt

### Đối với mèo
- Không ép buộc nếu mèo quá sợ hãi
- Có thể dùng khăn ướt lau thay vì tắm hoàn toàn
- Mèo con dưới 8 tuần không nên tắm nước

### Các dấu hiệu cần dừng tắm ngay
- Thú cưng thở gấp, run rẩy mạnh
- Da đỏ bất thường hoặc xuất hiện phát ban
- Thú cưng quá hoảng sợ hoặc hung hăng

---

Tại PetShop Sài Gòn, chúng tôi cung cấp đầy đủ các sản phẩm chăm sóc thú cưng chất lượng cao như sữa tắm đặc trị, dụng cụ tắm và chải lông chuyên nghiệp. Ngoài ra, chúng tôi còn có dịch vụ tắm và cắt tỉa lông chuyên nghiệp nếu bạn gặp khó khăn khi tắm cho thú cưng tại nhà. Hãy liên hệ với chúng tôi để được tư vấn chi tiết!', N'https://petshopsaigon.vn/tin-tuc/cach-tam-cho-cho-meo', 1);
SET IDENTITY_INSERT tintuc OFF;

-- Đổ thông tin cho bảng dssanpham_mua
INSERT INTO dssanpham_mua (MaHD, MaKH, MaTTSP, SoLuong, ThanhTien)
SELECT cthd.MaHD, hoadon.MaKH, cthd.MaTTSP, cthd.SoLuong, cthd.ThanhTien
FROM cthd
JOIN hoadon ON cthd.MaHD = hoadon.MaHD;

-- Thêm các ràng buộc khóa ngoại
-- Các ràng buộc cho bảng binhluan
ALTER TABLE binhluan ADD CONSTRAINT fk_bl_kh FOREIGN KEY (MaKH) REFERENCES khachhang (MaKH);
ALTER TABLE binhluan ADD CONSTRAINT fk_bl_ttsp FOREIGN KEY (MaTTSP) REFERENCES thongtinsp (MaTTSP);

-- Các ràng buộc cho bảng cthd
ALTER TABLE cthd ADD CONSTRAINT fk_cthd_hd FOREIGN KEY (MaHD) REFERENCES hoadon (MaHD);
ALTER TABLE cthd ADD CONSTRAINT fk_cthd_ttsp FOREIGN KEY (MaTTSP) REFERENCES thongtinsp (MaTTSP);

-- Các ràng buộc cho bảng ctpn
ALTER TABLE ctpn ADD CONSTRAINT fk_ctpn_pn FOREIGN KEY (MaPN) REFERENCES phieunhap (MaPN);
ALTER TABLE ctpn ADD CONSTRAINT fk_ctpn_ttsp FOREIGN KEY (MaTTSP) REFERENCES thongtinsp (MaTTSP);

-- Các ràng buộc cho bảng giaohang
ALTER TABLE giaohang ADD CONSTRAINT fk_giaohang_hd FOREIGN KEY (MaHD) REFERENCES hoadon (MaHD);
ALTER TABLE giaohang ADD CONSTRAINT fk_giaohang_nv FOREIGN KEY (MaNV) REFERENCES nhanvien (MaNV);

-- Các ràng buộc cho bảng giohang
ALTER TABLE giohang ADD CONSTRAINT fk_giohang_kh FOREIGN KEY (MaKH) REFERENCES khachhang (MaKH);
ALTER TABLE giohang ADD CONSTRAINT fk_giohang_ttsp FOREIGN KEY (MaTTSP) REFERENCES thongtinsp (MaTTSP);

-- Các ràng buộc cho bảng hoadon
ALTER TABLE hoadon ADD CONSTRAINT fk_hd_kh FOREIGN KEY (MaKH) REFERENCES khachhang (MaKH);
ALTER TABLE hoadon ADD CONSTRAINT fk_hd_km FOREIGN KEY (MaKM) REFERENCES khuyenmai (MaKM);

-- Các ràng buộc cho bảng phieunhap
ALTER TABLE phieunhap ADD CONSTRAINT fk_pn_ncc FOREIGN KEY (MaNCC) REFERENCES nhacc (MaNCC);
ALTER TABLE phieunhap ADD CONSTRAINT fk_pn_nv FOREIGN KEY (MaNV) REFERENCES nhanvien (MaNV);

-- Các ràng buộc cho bảng thongtinsp
ALTER TABLE thongtinsp ADD CONSTRAINT fk_ttsp_lsp FOREIGN KEY (MaLoaiSP) REFERENCES loaisp (MaLoaiSP);
ALTER TABLE thongtinsp ADD CONSTRAINT fk_ttsp_th FOREIGN KEY (MaTH) REFERENCES thuonghieu (MaTH);

-- Các ràng buộc cho bảng dssanpham_mua
ALTER TABLE dssanpham_mua ADD CONSTRAINT fk_dssp_hd FOREIGN KEY (MaHD) REFERENCES hoadon (MaHD);
ALTER TABLE dssanpham_mua ADD CONSTRAINT fk_dssp_kh FOREIGN KEY (MaKH) REFERENCES khachhang (MaKH);
ALTER TABLE dssanpham_mua ADD CONSTRAINT fk_dssp_ttsp FOREIGN KEY (MaTTSP) REFERENCES thongtinsp (MaTTSP);

-- Tạo các trigger cho SQL Server
-- Trigger cho bảng CTHD
GO
CREATE TRIGGER Before_Insert_CTHD
ON cthd
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaTTSP int, @SoLuong int, @MaHD int, @Gia int, @GiaKM int, @SoLuongHienCo int, @ThanhTien int;

    SELECT @MaTTSP = MaTTSP, @SoLuong = SoLuong, @MaHD = MaHD
    FROM inserted;

    -- Kiểm tra số lượng có đủ không
    SELECT @SoLuongHienCo = SoLuong 
    FROM thongtinsp 
    WHERE MaTTSP = @MaTTSP;

    IF (@SoLuongHienCo < @SoLuong)
    BEGIN
        RAISERROR('Số lượng không đủ', 16, 1);
        RETURN;
    END

    -- Cập nhật số lượng sản phẩm
    UPDATE thongtinsp 
    SET SoLuong = SoLuong - @SoLuong 
    WHERE MaTTSP = @MaTTSP;

    -- Lấy giá sản phẩm
    SELECT @Gia = Gia, @GiaKM = GiaKM 
    FROM thongtinsp 
    WHERE MaTTSP = @MaTTSP;

    -- Tính thành tiền
    IF (@GiaKM IS NOT NULL AND @GiaKM != 0)
        SET @ThanhTien = @GiaKM * @SoLuong;
    ELSE
        SET @ThanhTien = @Gia * @SoLuong;

    -- Thêm vào bảng CTHD
    INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien)
    VALUES (@MaHD, @MaTTSP, @SoLuong, @ThanhTien);
END;
GO

CREATE TRIGGER After_Insert_CTHD
ON cthd
AFTER INSERT
AS
BEGIN
    DECLARE @MaHD int, @MaTTSP int, @SoLuong int, @ThanhTien int;
    DECLARE @TongTien int, @TienGiam int = 0;
    DECLARE @SoPTKM tinyint, @NgayMua date, @TuNgay date, @DenNgay date, @TTienToiThieu int, @MaKM int;
    DECLARE @MaKH int;

    SELECT @MaHD = MaHD, @MaTTSP = MaTTSP, @SoLuong = SoLuong, @ThanhTien = ThanhTien
    FROM inserted;

    -- Tính tổng tiền trong hóa đơn
    SELECT @TongTien = SUM(ThanhTien)
    FROM cthd
    WHERE MaHD = @MaHD;

    UPDATE hoadon
    SET TongTienTT = @TongTien
    WHERE MaHD = @MaHD;

    -- Lấy thông tin từ hóa đơn
    SELECT @NgayMua = NgayLapHD, @MaKM = MaKM, @MaKH = MaKH
    FROM hoadon
    WHERE MaHD = @MaHD;

    -- Lưu thông tin sản phẩm vào bảng dssanpham_mua
    INSERT INTO dssanpham_mua (MaHD, MaKH, MaTTSP, SoLuong, ThanhTien)
    VALUES (@MaHD, @MaKH, @MaTTSP, @SoLuong, @ThanhTien);

    -- Lấy thông tin khuyến mãi
    IF (@MaKM IS NOT NULL)
    BEGIN
        SELECT @SoPTKM = SoPTKM, @TuNgay = TuNgay, @DenNgay = DenNgay, @TTienToiThieu = TTienToiThieu
        FROM khuyenmai
        WHERE MaKM = @MaKM;

        IF (@NgayMua >= @TuNgay AND @NgayMua <= @DenNgay AND @TongTien >= @TTienToiThieu)
        BEGIN
            SET @TienGiam = @TongTien * (@SoPTKM / 100.0);
            UPDATE hoadon
            SET TongTienTT = @TongTien - @TienGiam
            WHERE MaHD = @MaHD;
        END
    END
END;
GO

CREATE TRIGGER Before_Update_CTHD
ON cthd
INSTEAD OF UPDATE
AS
BEGIN
    DECLARE @MaTTSP int, @SoLuongMoi int, @SoLuongCu int, @MaHD int, @Gia int, @GiaKM int, @SoLuongHienCo int, @ThanhTien int;

    SELECT @MaTTSP = i.MaTTSP, @SoLuongMoi = i.SoLuong, @MaHD = i.MaHD
    FROM inserted i;

    SELECT @SoLuongCu = SoLuong
    FROM deleted;

    -- Kiểm tra số lượng có đủ không
    SELECT @SoLuongHienCo = SoLuong 
    FROM thongtinsp 
    WHERE MaTTSP = @MaTTSP;

    IF ((@SoLuongHienCo + @SoLuongCu) < @SoLuongMoi)
    BEGIN
        RAISERROR('Số lượng không đủ', 16, 1);
        RETURN;
    END

    -- Cập nhật số lượng sản phẩm
    UPDATE thongtinsp 
    SET SoLuong = SoLuong + @SoLuongCu - @SoLuongMoi 
    WHERE MaTTSP = @MaTTSP;

    -- Lấy giá sản phẩm
    SELECT @Gia = Gia, @GiaKM = GiaKM 
    FROM thongtinsp 
    WHERE MaTTSP = @MaTTSP;

    -- Tính thành tiền
    IF (@GiaKM IS NOT NULL AND @GiaKM != 0)
        SET @ThanhTien = @GiaKM * @SoLuongMoi;
    ELSE
        SET @ThanhTien = @Gia * @SoLuongMoi;

    -- Cập nhật bảng CTHD
    UPDATE cthd
    SET SoLuong = @SoLuongMoi, ThanhTien = @ThanhTien
    WHERE MaHD = @MaHD AND MaTTSP = @MaTTSP;
END;
GO

CREATE TRIGGER After_Update_CTHD
ON cthd
AFTER UPDATE
AS
BEGIN
    DECLARE @MaHD int, @MaTTSP int, @SoLuong int, @ThanhTien int;
    DECLARE @TongTien int, @TienGiam int = 0;
    DECLARE @SoPTKM tinyint, @NgayMua date, @TuNgay date, @DenNgay date, @TTienToiThieu int, @MaKM int;
    DECLARE @MaKH int;

    SELECT @MaHD = MaHD, @MaTTSP = MaTTSP, @SoLuong = SoLuong, @ThanhTien = ThanhTien
    FROM inserted;

    -- Tính tổng tiền trong hóa đơn
    SELECT @TongTien = SUM(ThanhTien)
    FROM cthd
    WHERE MaHD = @MaHD;

    UPDATE hoadon
    SET TongTienTT = @TongTien
    WHERE MaHD = @MaHD;

    -- Lấy thông tin từ hóa đơn
    SELECT @NgayMua = NgayLapHD, @MaKM = MaKM, @MaKH = MaKH
    FROM hoadon
    WHERE MaHD = @MaHD;

    -- Cập nhật thông tin sản phẩm vào bảng dssanpham_mua
    UPDATE dssanpham_mua 
    SET SoLuong = @SoLuong, ThanhTien = @ThanhTien 
    WHERE MaHD = @MaHD AND MaTTSP = @MaTTSP;

    -- Lấy thông tin khuyến mãi
    IF (@MaKM IS NOT NULL)
    BEGIN
        SELECT @SoPTKM = SoPTKM, @TuNgay = TuNgay, @DenNgay = DenNgay, @TTienToiThieu = TTienToiThieu
        FROM khuyenmai
        WHERE MaKM = @MaKM;

        IF (@NgayMua >= @TuNgay AND @NgayMua <= @DenNgay AND @TongTien >= @TTienToiThieu)
        BEGIN
            SET @TienGiam = @TongTien * (@SoPTKM / 100.0);
            UPDATE hoadon
            SET TongTienTT = @TongTien - @TienGiam
            WHERE MaHD = @MaHD;
        END
    END
END;
GO

CREATE TRIGGER After_Delete_CTHD
ON cthd
AFTER DELETE
AS
BEGIN
    DECLARE @MaHD int, @MaTTSP int, @SoLuong int;
    DECLARE @TongTien int, @TienGiam int = 0;
    DECLARE @SoPTKM tinyint, @NgayMua date, @TuNgay date, @DenNgay date, @TTienToiThieu int, @MaKM int;

    SELECT @MaHD = MaHD, @MaTTSP = MaTTSP, @SoLuong = SoLuong
    FROM deleted;

    -- Cập nhật số lượng
    UPDATE thongtinsp 
    SET SoLuong = SoLuong + @SoLuong 
    WHERE MaTTSP = @MaTTSP;

    -- Tính tổng tiền trong hóa đơn
    SELECT @TongTien = SUM(ThanhTien)
    FROM cthd
    WHERE MaHD = @MaHD;

    -- Nếu không còn sản phẩm nào trong hóa đơn
    IF @TongTien IS NULL
        SET @TongTien = 0;

    UPDATE hoadon
    SET TongTienTT = @TongTien
    WHERE MaHD = @MaHD;

    -- Lấy thông tin từ hóa đơn
    SELECT @NgayMua = NgayLapHD, @MaKM = MaKM 
    FROM hoadon
    WHERE MaHD = @MaHD;

    -- Lấy thông tin khuyến mãi
    IF (@MaKM IS NOT NULL)
    BEGIN
        SELECT @SoPTKM = SoPTKM, @TuNgay = TuNgay, @DenNgay = DenNgay, @TTienToiThieu = TTienToiThieu
        FROM khuyenmai
        WHERE MaKM = @MaKM;

        IF (@NgayMua >= @TuNgay AND @NgayMua <= @DenNgay AND @TongTien >= @TTienToiThieu)
        BEGIN
            SET @TienGiam = @TongTien * (@SoPTKM / 100.0);
            UPDATE hoadon
            SET TongTienTT = @TongTien - @TienGiam
            WHERE MaHD = @MaHD;
        END
    END
END;
GO

-- Trigger cho bảng CTPN
CREATE TRIGGER Tinh_ThanhTien_CTPN
ON ctpn
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaPN int, @MaTTSP int, @GiaNhap int, @SoLuong int, @ThanhTien int;

    SELECT @MaPN = MaPN, @MaTTSP = MaTTSP, @GiaNhap = GiaNhap, @SoLuong = SoLuong
    FROM inserted;

    SET @ThanhTien = @GiaNhap * @SoLuong;

    INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien)
    VALUES (@MaPN, @MaTTSP, @GiaNhap, @SoLuong, @ThanhTien);
END;
GO

CREATE TRIGGER Alter_Insert_CTPN
ON ctpn
AFTER INSERT
AS
BEGIN
    DECLARE @MaPN int, @MaTTSP int, @SoLuong int, @ThanhTien int = 0;

    SELECT @MaPN = MaPN, @MaTTSP = MaTTSP, @SoLuong = SoLuong
    FROM inserted;

    -- Cập nhật lại số lượng sản phẩm
    UPDATE thongtinsp
    SET SoLuong = SoLuong + @SoLuong
    WHERE MaTTSP = @MaTTSP;

    -- Tính tổng tiền trong phiếu nhập
    SELECT @ThanhTien = SUM(ThanhTien)
    FROM ctpn
    WHERE MaPN = @MaPN;

    UPDATE phieunhap
    SET TongTienTT = @ThanhTien
    WHERE MaPN = @MaPN;
END;
GO

CREATE TRIGGER Before_Updated_CTPN
ON ctpn
INSTEAD OF UPDATE
AS
BEGIN
    DECLARE @MaPN int, @MaTTSP int, @GiaNhap int, @SoLuong int, @ThanhTien int;

    SELECT @MaPN = MaPN, @MaTTSP = MaTTSP, @GiaNhap = GiaNhap, @SoLuong = SoLuong
    FROM inserted;

    SET @ThanhTien = @GiaNhap * @SoLuong;

    UPDATE ctpn
    SET GiaNhap = @GiaNhap, SoLuong = @SoLuong, ThanhTien = @ThanhTien
    WHERE MaPN = @MaPN AND MaTTSP = @MaTTSP;
END;
GO

CREATE TRIGGER Alter_Update_CTPN
ON ctpn
AFTER UPDATE
AS
BEGIN
    DECLARE @MaPN int, @MaTTSP int, @SoLuongMoi int, @SoLuongCu int, @ThanhTien int = 0;

    SELECT @MaPN = i.MaPN, @MaTTSP = i.MaTTSP, @SoLuongMoi = i.SoLuong
    FROM inserted i;

    SELECT @SoLuongCu = SoLuong
    FROM deleted;

    -- Cập nhật lại số lượng sản phẩm
    UPDATE thongtinsp
    SET SoLuong = SoLuong - @SoLuongCu + @SoLuongMoi
    WHERE MaTTSP = @MaTTSP;

    -- Tính tổng tiền trong phiếu nhập
    SELECT @ThanhTien = SUM(ThanhTien)
    FROM ctpn
    WHERE MaPN = @MaPN;

    UPDATE phieunhap
    SET TongTienTT = @ThanhTien
    WHERE MaPN = @MaPN;
END;
GO

CREATE TRIGGER Alter_Delete_CTPN
ON ctpn
AFTER DELETE
AS
BEGIN
    DECLARE @MaPN int, @MaTTSP int, @SoLuong int, @ThanhTien int = 0;

    SELECT @MaPN = MaPN, @MaTTSP = MaTTSP, @SoLuong = SoLuong
    FROM deleted;

    -- Cập nhật lại số lượng sản phẩm
    UPDATE thongtinsp
    SET SoLuong = SoLuong - @SoLuong
    WHERE MaTTSP = @MaTTSP;

    -- Tính tổng tiền trong phiếu nhập
    SELECT @ThanhTien = SUM(ThanhTien)
    FROM ctpn
    WHERE MaPN = @MaPN;

    -- Nếu không còn sản phẩm nào trong phiếu nhập
    IF @ThanhTien IS NULL
        SET @ThanhTien = 0;

    UPDATE phieunhap
    SET TongTienTT = @ThanhTien
    WHERE MaPN = @MaPN;
END;
GO

-- Trigger cho bảng hoadon
CREATE TRIGGER Before_Update_HD
ON hoadon
AFTER UPDATE
AS
BEGIN
    DECLARE @MaHD int, @MaKM int, @MaKM_Cu int, @NgayLapHD date, @NgayLapHD_Cu date;
    DECLARE @TongTien int, @TienGiam int = 0;
    DECLARE @SoPTKM tinyint, @TuNgay date, @DenNgay date, @TTienToiThieu int;
    DECLARE @SoTienNhan int, @SoTienNhan_Cu int, @TongTienTT int;

    SELECT @MaHD = i.MaHD, @MaKM = i.MaKM, @NgayLapHD = i.NgayLapHD, @SoTienNhan = i.SoTienNhan, @TongTienTT = i.TongTienTT
    FROM inserted i;

    SELECT @MaKM_Cu = MaKM, @NgayLapHD_Cu = NgayLapHD, @SoTienNhan_Cu = SoTienNhan
    FROM deleted;

    -- Nếu MaKM hoặc NgayLapHD thay đổi
    IF (@MaKM != @MaKM_Cu OR @NgayLapHD != @NgayLapHD_Cu)
    BEGIN
        -- Tính tổng tiền chưa áp dụng khuyến mãi
        SELECT @TongTien = SUM(ThanhTien)
        FROM cthd
        WHERE MaHD = @MaHD;

        UPDATE hoadon
        SET TongTienTT = @TongTien
        WHERE MaHD = @MaHD;

        -- Lấy thông tin khuyến mãi
        SELECT @SoPTKM = SoPTKM, @TuNgay = TuNgay, @DenNgay = DenNgay, @TTienToiThieu = TTienToiThieu
        FROM khuyenmai
        WHERE MaKM = @MaKM;

        IF (@NgayLapHD >= @TuNgay AND @NgayLapHD <= @DenNgay AND @TongTien >= @TTienToiThieu)
        BEGIN
            SET @TienGiam = @TongTien * (@SoPTKM / 100.0);
            UPDATE hoadon
            SET TongTienTT = @TongTien - @TienGiam, SoTienTra = SoTienNhan - (@TongTien - @TienGiam)
            WHERE MaHD = @MaHD;
        END
    END

    -- Nếu SoTienNhan thay đổi
    IF (@SoTienNhan != @SoTienNhan_Cu)
    BEGIN
        UPDATE hoadon
        SET SoTienTra = @SoTienNhan - @TongTienTT
        WHERE MaHD = @MaHD;
    END

    -- Cập nhật tình trạng thanh toán
    IF (@SoTienNhan >= @TongTienTT)
    BEGIN
        UPDATE hoadon
        SET TinhTrangTT = 1
        WHERE MaHD = @MaHD;
    END
    ELSE
    BEGIN
        UPDATE hoadon
        SET TinhTrangTT = 0
        WHERE MaHD = @MaHD;
    END
END;
GO

