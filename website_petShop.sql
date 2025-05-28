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
-- Thêm dữ liệu chi tiết hóa đơn cho các hóa đơn từ MaHD 20 đến 115
-- Tháng 1/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(20, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(20, 1, 2, 80000),               -- Cát vệ sinh cho mèo
(21, 7, 2, 450000),              -- Thức ăn cho chó
(22, 13, 2, 275000),             -- Thuốc tẩy giun cho mèo
(23, 4, 2, 400000),              -- Đồ chơi cho chó
(23, 1, 3, 120000);              -- Cát vệ sinh cho mèo

-- Tháng 2/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(24, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(24, 2, 2, 110000),              -- Thức ăn cho mèo phổ thông
(25, 7, 1, 250000),              -- Thức ăn cho chó
(25, 13, 2, 170000),             -- Thuốc tẩy giun cho mèo
(26, 3, 1, 135000),              -- Thức ăn cho mèo cao cấp
(26, 1, 4, 175000),              -- Cát vệ sinh cho mèo
(27, 4, 2, 400000),              -- Đồ chơi cho chó
(27, 2, 2, 90000);               -- Thức ăn cho mèo phổ thông

-- Tháng 3/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(28, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(28, 13, 1, 90000),              -- Thuốc tẩy giun cho mèo
(29, 7, 1, 250000),              -- Thức ăn cho chó
(29, 4, 1, 180000),              -- Đồ chơi cho chó
(30, 3, 1, 135000),              -- Thức ăn cho mèo cao cấp
(30, 1, 4, 160000),              -- Cát vệ sinh cho mèo
(31, 4, 2, 400000),              -- Đồ chơi cho chó
(31, 2, 2, 110000);              -- Thức ăn cho mèo phổ thông

-- Tháng 4/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(32, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(32, 1, 2, 100000),              -- Cát vệ sinh cho mèo
(33, 7, 1, 250000),              -- Thức ăn cho chó
(33, 4, 1, 190000),              -- Đồ chơi cho chó
(34, 3, 1, 135000),              -- Thức ăn cho mèo cao cấp
(34, 13, 2, 185000),             -- Thuốc tẩy giun cho mèo
(35, 4, 2, 400000),              -- Đồ chơi cho chó
(35, 2, 1, 80000);               -- Thức ăn cho mèo phổ thông

-- Tiếp tục cho các tháng còn lại của năm 2024
-- Tháng 5/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(36, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(36, 1, 3, 120000),              -- Cát vệ sinh cho mèo
(37, 7, 1, 250000),              -- Thức ăn cho chó
(37, 4, 1, 210000),              -- Đồ chơi cho chó
(38, 3, 1, 135000),              -- Thức ăn cho mèo cao cấp
(38, 13, 2, 205000),             -- Thuốc tẩy giun cho mèo
(39, 4, 2, 400000),              -- Đồ chơi cho chó
(39, 2, 2, 130000);              -- Thức ăn cho mèo phổ thông

-- Tháng 6/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(40, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(40, 1, 3, 140000),              -- Cát vệ sinh cho mèo
(41, 7, 1, 250000),              -- Thức ăn cho chó
(41, 4, 1, 220000),              -- Đồ chơi cho chó
(42, 3, 1, 135000),              -- Thức ăn cho mèo cao cấp
(42, 13, 2, 195000),             -- Thuốc tẩy giun cho mèo
(43, 4, 2, 400000),              -- Đồ chơi cho chó
(43, 2, 3, 140000);              -- Thức ăn cho mèo phổ thông

-- Tháng 7/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(44, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(44, 1, 3, 130000),              -- Cát vệ sinh cho mèo
(45, 7, 1, 250000),              -- Thức ăn cho chó
(45, 4, 1, 230000),              -- Đồ chơi cho chó
(46, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(46, 2, 2, 80000),               -- Thức ăn cho mèo phổ thông
(47, 4, 2, 400000),              -- Đồ chơi cho chó
(47, 1, 3, 150000);              -- Cát vệ sinh cho mèo

-- Tháng 8/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(48, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(48, 1, 3, 150000),              -- Cát vệ sinh cho mèo
(49, 7, 1, 250000),              -- Thức ăn cho chó
(49, 4, 1, 240000),              -- Đồ chơi cho chó
(50, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(50, 13, 1, 90000),              -- Thuốc tẩy giun cho mèo
(51, 4, 2, 400000),              -- Đồ chơi cho chó
(51, 2, 4, 160000);              -- Thức ăn cho mèo phổ thông

-- Tháng 9/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(52, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(52, 1, 4, 160000),              -- Cát vệ sinh cho mèo
(53, 7, 2, 500000),              -- Thức ăn cho chó
(54, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(54, 1, 2, 100000),              -- Cát vệ sinh cho mèo
(55, 4, 2, 400000),              -- Đồ chơi cho chó
(55, 13, 2, 170000);             -- Thuốc tẩy giun cho mèo

-- Tháng 10/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(56, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(56, 13, 2, 170000),             -- Thuốc tẩy giun cho mèo
(57, 7, 2, 500000),              -- Thức ăn cho chó
(57, 1, 1, 10000),               -- Cát vệ sinh cho mèo
(58, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(58, 2, 2, 110000),              -- Thức ăn cho mèo phổ thông
(59, 4, 2, 400000),              -- Đồ chơi cho chó
(59, 13, 2, 180000);             -- Thuốc tẩy giun cho mèo

-- Tháng 11/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(60, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(60, 13, 2, 180000),             -- Thuốc tẩy giun cho mèo
(61, 7, 2, 500000),              -- Thức ăn cho chó
(61, 1, 1, 20000),               -- Cát vệ sinh cho mèo
(62, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(62, 2, 3, 120000),              -- Thức ăn cho mèo phổ thông
(63, 4, 2, 400000),              -- Đồ chơi cho chó
(63, 13, 2, 190000);             -- Thuốc tẩy giun cho mèo

-- Tháng 12/2024
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(64, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(64, 13, 2, 190000),             -- Thuốc tẩy giun cho mèo
(65, 7, 2, 500000),              -- Thức ăn cho chó
(65, 1, 1, 30000),               -- Cát vệ sinh cho mèo
(66, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(66, 2, 3, 130000),              -- Thức ăn cho mèo phổ thông
(67, 4, 3, 600000);              -- Đồ chơi cho chó

-- Dữ liệu năm 2025
-- Tháng 1/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(68, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(68, 13, 2, 200000),             -- Thuốc tẩy giun cho mèo
(69, 7, 2, 500000),              -- Thức ăn cho chó
(69, 1, 1, 40000),               -- Cát vệ sinh cho mèo
(70, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(70, 2, 3, 140000),              -- Thức ăn cho mèo phổ thông
(71, 4, 3, 610000);              -- Đồ chơi cho chó

-- Tháng 2/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(72, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(72, 13, 2, 210000),             -- Thuốc tẩy giun cho mèo
(73, 7, 2, 500000),              -- Thức ăn cho chó
(73, 1, 1, 50000),               -- Cát vệ sinh cho mèo
(74, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(74, 2, 3, 150000),              -- Thức ăn cho mèo phổ thông
(75, 4, 3, 620000);              -- Đồ chơi cho chó

-- Tháng 3/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(76, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(76, 13, 2, 220000),             -- Thuốc tẩy giun cho mèo
(77, 7, 2, 500000),              -- Thức ăn cho chó
(77, 1, 1, 60000),               -- Cát vệ sinh cho mèo
(78, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(78, 2, 4, 160000),              -- Thức ăn cho mèo phổ thông
(79, 4, 3, 630000);              -- Đồ chơi cho chó

-- Tháng 4/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(80, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(80, 13, 2, 230000),             -- Thuốc tẩy giun cho mèo
(81, 7, 2, 500000),              -- Thức ăn cho chó
(81, 1, 2, 70000),               -- Cát vệ sinh cho mèo
(82, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(82, 2, 4, 170000),              -- Thức ăn cho mèo phổ thông
(83, 4, 3, 640000);              -- Đồ chơi cho chó

-- Tháng 5/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(84, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(84, 13, 2, 240000),             -- Thuốc tẩy giun cho mèo
(85, 7, 2, 500000),              -- Thức ăn cho chó
(85, 1, 2, 80000),               -- Cát vệ sinh cho mèo
(86, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(86, 2, 4, 180000),              -- Thức ăn cho mèo phổ thông
(87, 4, 3, 650000);              -- Đồ chơi cho chó

-- Tháng 6/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(88, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(88, 13, 2, 250000),             -- Thuốc tẩy giun cho mèo
(89, 7, 2, 500000),              -- Thức ăn cho chó
(89, 1, 2, 90000),               -- Cát vệ sinh cho mèo
(90, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(90, 2, 4, 190000),              -- Thức ăn cho mèo phổ thông
(91, 4, 3, 660000);              -- Đồ chơi cho chó

-- Tháng 7/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(92, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(92, 13, 2, 260000),             -- Thuốc tẩy giun cho mèo
(93, 7, 2, 500000),              -- Thức ăn cho chó
(93, 1, 2, 100000),              -- Cát vệ sinh cho mèo
(94, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(94, 2, 5, 200000),              -- Thức ăn cho mèo phổ thông
(95, 4, 3, 670000);              -- Đồ chơi cho chó

-- Tháng 8/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(96, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(96, 13, 2, 270000),             -- Thuốc tẩy giun cho mèo
(97, 7, 2, 500000),              -- Thức ăn cho chó
(97, 1, 3, 110000),              -- Cát vệ sinh cho mèo
(98, 3, 2, 270000),              -- Thức ăn cho mèo cao cấp
(98, 2, 5, 210000),              -- Thức ăn cho mèo phổ thông
(99, 4, 3, 680000);              -- Đồ chơi cho chó

-- Tháng 9/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(100, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(100, 13, 2, 280000),            -- Thuốc tẩy giun cho mèo
(101, 7, 2, 500000),             -- Thức ăn cho chó
(101, 1, 3, 120000),             -- Cát vệ sinh cho mèo
(102, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(102, 2, 5, 220000),             -- Thức ăn cho mèo phổ thông
(103, 4, 3, 690000);             -- Đồ chơi cho chó

-- Tháng 10/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(104, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(104, 13, 2, 290000),            -- Thuốc tẩy giun cho mèo
(105, 7, 2, 500000),             -- Thức ăn cho chó
(105, 1, 3, 130000),             -- Cát vệ sinh cho mèo
(106, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(106, 2, 5, 230000),             -- Thức ăn cho mèo phổ thông
(107, 4, 3, 700000);             -- Đồ chơi cho chó

-- Tháng 11/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(108, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(108, 13, 3, 300000),            -- Thuốc tẩy giun cho mèo
(109, 7, 2, 500000),             -- Thức ăn cho chó
(109, 1, 3, 140000),             -- Cát vệ sinh cho mèo
(110, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(110, 2, 6, 240000),             -- Thức ăn cho mèo phổ thông
(111, 4, 3, 710000);             -- Đồ chơi cho chó

-- Tháng 12/2025
INSERT INTO cthd (MaHD, MaTTSP, SoLuong, ThanhTien) VALUES
(112, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(112, 13, 3, 310000),            -- Thuốc tẩy giun cho mèo
(113, 7, 2, 500000),             -- Thức ăn cho chó
(113, 1, 3, 150000),             -- Cát vệ sinh cho mèo
(114, 3, 2, 270000),             -- Thức ăn cho mèo cao cấp
(114, 2, 6, 250000),             -- Thức ăn cho mèo phổ thông
(115, 4, 3, 720000);             -- Đồ chơi cho chó

-- Cấu trúc bảng cho bảng ctpn
CREATE TABLE ctpn (
    MaPN int NOT NULL,
    MaTTSP int NOT NULL,
    GiaNhap int NOT NULL,
    SoLuong int NOT NULL,
    ThanhTien int NULL,
    PRIMARY KEY (MaPN, MaTTSP)
);

-- Đang đổ dữ liệu cho bảng ctpn cho các phiếu nhập năm 2025

-- Tháng 1/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(6, 1, 50000, 40, 2000000),       -- Cát vệ sinh cho mèo
(6, 5, 600000, 10, 6000000),      -- Nhà cho mèo
(6, 9, 250000, 10, 2500000),      -- Đồ chơi cho mèo
(7, 3, 100000, 35, 3500000),      -- Thức ăn cho mèo cao cấp
(7, 11, 800000, 5, 4000000),      -- Balo vận chuyển thú cưng
(7, 13, 125000, 10, 1250000),     -- Thuốc tẩy giun cho mèo
(8, 2, 80000, 25, 2000000),       -- Thức ăn cho mèo
(8, 8, 150000, 20, 3000000),      -- Đồ chơi cho chó cao cấp
(8, 12, 30000, 10, 300000);       -- Thuốc nhỏ mắt

-- Tháng 2/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(9, 4, 150000, 30, 4500000),      -- Đồ chơi cho chó
(9, 6, 1700000, 2, 3400000),      -- Lồng chim cao cấp
(9, 13, 130000, 10, 1300000),     -- Thuốc tẩy giun cho mèo
(10, 1, 55000, 50, 2750000),      -- Cát vệ sinh cho mèo
(10, 7, 210000, 20, 4200000),     -- Thức ăn cho chó
(10, 11, 650000, 1, 650000),      -- Balo vận chuyển thú cưng
(11, 3, 100000, 25, 2500000),     -- Thức ăn cho mèo cao cấp
(11, 9, 230000, 10, 2300000);     -- Đồ chơi cho mèo

-- Tháng 3/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(12, 5, 620000, 10, 6200000),     -- Nhà cho mèo
(12, 10, 1020000, 5, 5100000),    -- Lồng mèo cao cấp
(13, 2, 85000, 30, 2550000),      -- Thức ăn cho mèo
(13, 8, 155000, 25, 3875000),     -- Đồ chơi cho chó cao cấp
(13, 12, 35000, 15, 525000),      -- Thuốc nhỏ mắt
(14, 1, 52000, 60, 3120000),      -- Cát vệ sinh cho mèo
(14, 4, 160000, 20, 3200000),     -- Đồ chơi cho chó
(14, 13, 129000, 20, 2580000);    -- Thuốc tẩy giun cho mèo

-- Tháng 4/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(15, 3, 105000, 40, 4200000),     -- Thức ăn cho mèo cao cấp
(15, 6, 1750000, 3, 5250000),     -- Lồng chim cao cấp
(15, 11, 750000, 4, 3000000),     -- Balo vận chuyển thú cưng
(16, 7, 195000, 30, 5850000),     -- Thức ăn cho chó
(16, 9, 240000, 8, 1920000),      -- Đồ chơi cho mèo
(17, 2, 82000, 20, 1640000),      -- Thức ăn cho mèo
(17, 5, 630000, 5, 3150000),      -- Nhà cho mèo
(17, 12, 32000, 25, 800000);      -- Thuốc nhỏ mắt

-- Tháng 5/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(18, 1, 53000, 45, 2385000),      -- Cát vệ sinh cho mèo
(18, 8, 160000, 30, 4800000),     -- Đồ chơi cho chó cao cấp
(18, 10, 1050000, 2, 2100000),    -- Lồng mèo cao cấp
(18, 13, 135000, 15, 2025000),    -- Thuốc tẩy giun cho mèo
(19, 3, 110000, 35, 3850000),     -- Thức ăn cho mèo cao cấp
(19, 4, 155000, 28, 4340000),     -- Đồ chơi cho chó
(20, 6, 1720000, 2, 3440000),     -- Lồng chim cao cấp
(20, 11, 720000, 4, 2880000);     -- Balo vận chuyển thú cưng

-- Tháng 6/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(21, 2, 84000, 40, 3360000),      -- Thức ăn cho mèo
(21, 5, 640000, 8, 5120000),      -- Nhà cho mèo
(21, 9, 242000, 10, 2420000),     -- Đồ chơi cho mèo
(22, 7, 205000, 25, 5125000),     -- Thức ăn cho chó
(22, 12, 34000, 20, 680000),      -- Thuốc nhỏ mắt
(22, 13, 132000, 12, 1584000),    -- Thuốc tẩy giun cho mèo
(23, 1, 54000, 50, 2700000),      -- Cát vệ sinh cho mèo
(23, 8, 155000, 20, 3100000);     -- Đồ chơi cho chó cao cấp

-- Tháng 7/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(24, 3, 112000, 40, 4480000),     -- Thức ăn cho mèo cao cấp
(24, 10, 1080000, 4, 4320000),    -- Lồng mèo cao cấp
(24, 11, 750000, 4, 3000000),     -- Balo vận chuyển thú cưng
(25, 4, 162000, 25, 4050000),     -- Đồ chơi cho chó
(25, 6, 1730000, 2, 3460000),     -- Lồng chim cao cấp
(25, 13, 136000, 8, 1088000),     -- Thuốc tẩy giun cho mèo
(26, 1, 55000, 40, 2200000),      -- Cát vệ sinh cho mèo
(26, 5, 650000, 6, 3900000);      -- Nhà cho mèo

-- Tháng 8/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(27, 2, 86000, 35, 3010000),      -- Thức ăn cho mèo
(27, 7, 210000, 30, 6300000),     -- Thức ăn cho chó
(27, 9, 245000, 5, 1225000),      -- Đồ chơi cho mèo
(28, 8, 158000, 25, 3950000),     -- Đồ chơi cho chó cao cấp
(28, 12, 36000, 30, 1080000),     -- Thuốc nhỏ mắt
(28, 13, 138000, 20, 2760000),    -- Thuốc tẩy giun cho mèo
(29, 3, 115000, 30, 3450000),     -- Thức ăn cho mèo cao cấp
(29, 11, 760000, 2, 1520000),     -- Balo vận chuyển thú cưng
(29, 4, 165000, 3, 495000);       -- Đồ chơi cho chó

-- Tháng 9/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(30, 1, 56000, 55, 3080000),      -- Cát vệ sinh cho mèo
(30, 6, 1750000, 3, 5250000),     -- Lồng chim cao cấp
(30, 10, 1100000, 3, 3300000),    -- Lồng mèo cao cấp
(31, 5, 660000, 7, 4620000),      -- Nhà cho mèo
(31, 9, 248000, 15, 3720000),     -- Đồ chơi cho mèo
(32, 2, 88000, 25, 2200000),      -- Thức ăn cho mèo
(32, 7, 215000, 20, 4300000),     -- Thức ăn cho chó
(32, 12, 38000, 10, 380000);      -- Thuốc nhỏ mắt

-- Tháng 10/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(33, 3, 118000, 35, 4130000),     -- Thức ăn cho mèo cao cấp
(33, 8, 160000, 30, 4800000),     -- Đồ chơi cho chó cao cấp
(33, 13, 140000, 15, 2100000),    -- Thuốc tẩy giun cho mèo
(34, 4, 168000, 25, 4200000),     -- Đồ chơi cho chó
(34, 11, 770000, 6, 4620000),     -- Balo vận chuyển thú cưng
(35, 1, 57000, 40, 2280000),      -- Cát vệ sinh cho mèo
(35, 6, 1760000, 2, 3520000),     -- Lồng chim cao cấp
(35, 9, 250000, 4, 1000000);      -- Đồ chơi cho mèo

-- Tháng 11/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(36, 2, 90000, 30, 2700000),      -- Thức ăn cho mèo
(36, 5, 670000, 8, 5360000),      -- Nhà cho mèo
(36, 10, 1120000, 2, 2240000),    -- Lồng mèo cao cấp
(37, 7, 220000, 25, 5500000),     -- Thức ăn cho chó
(37, 12, 40000, 25, 1000000),     -- Thuốc nhỏ mắt
(37, 13, 142000, 10, 1420000),    -- Thuốc tẩy giun cho mèo
(38, 3, 120000, 30, 3600000),     -- Thức ăn cho mèo cao cấp
(38, 8, 162000, 15, 2430000);     -- Đồ chơi cho chó cao cấp

-- Tháng 12/2025
INSERT INTO ctpn (MaPN, MaTTSP, GiaNhap, SoLuong, ThanhTien) VALUES
(39, 1, 58000, 60, 3480000),      -- Cát vệ sinh cho mèo
(39, 4, 170000, 30, 5100000),     -- Đồ chơi cho chó
(39, 11, 780000, 5, 3900000),     -- Balo vận chuyển thú cưng
(40, 6, 1780000, 3, 5340000),     -- Lồng chim cao cấp
(40, 9, 252000, 15, 3780000),     -- Đồ chơi cho mèo
(41, 2, 92000, 35, 3220000),      -- Thức ăn cho mèo
(41, 5, 680000, 5, 3400000),      -- Nhà cho mèo
(41, 12, 42000, 15, 630000);      -- Thuốc nhỏ mắt



-- Cấu trúc bảng cho bảng giaohang
CREATE TABLE giaohang (
    MaHD int NOT NULL,
    MaNV int NULL,
    TinhTrangGH int NOT NULL
);

-- Đang đổ dữ liệu cho bảng giaohang
-- Thêm dữ liệu giao hàng cho các hóa đơn từ MaHD 20 đến 115
-- Năm 2024
INSERT INTO giaohang (MaHD, MaNV, TinhTrangGH) VALUES
-- Tháng 1/2024
(20, 2, 1),  -- Đã giao thành công
(21, 5, 1),  -- Đã giao thành công
(22, 6, 1),  -- Đã giao thành công
(23, 2, 1),  -- Đã giao thành công

-- Tháng 2/2024
(24, 5, 1),  -- Đã giao thành công
(25, 6, 1),  -- Đã giao thành công
(26, 2, 1),  -- Đã giao thành công
(27, 5, 1),  -- Đã giao thành công

-- Tháng 3/2024
(28, 6, 1),  -- Đã giao thành công
(29, 2, 1),  -- Đã giao thành công
(30, 5, 1),  -- Đã giao thành công
(31, 6, 1),  -- Đã giao thành công

-- Tháng 4/2024
(32, 2, 1),  -- Đã giao thành công
(33, 5, 1),  -- Đã giao thành công
(34, 6, 1),  -- Đã giao thành công
(35, 2, 1),  -- Đã giao thành công

-- Tháng 5/2024
(36, 5, 1),  -- Đã giao thành công
(37, 6, 1),  -- Đã giao thành công
(38, 2, 1),  -- Đã giao thành công
(39, 5, 1),  -- Đã giao thành công

-- Tháng 6/2024
(40, 6, 1),  -- Đã giao thành công
(41, 2, 1),  -- Đã giao thành công
(42, 5, 1),  -- Đã giao thành công
(43, 6, 1),  -- Đã giao thành công

-- Tháng 7/2024
(44, 2, 1),  -- Đã giao thành công
(45, 5, 1),  -- Đã giao thành công
(46, 6, 1),  -- Đã giao thành công
(47, 2, 1),  -- Đã giao thành công

-- Tháng 8/2024
(48, 5, 1),  -- Đã giao thành công
(49, 6, 1),  -- Đã giao thành công
(50, 2, 1),  -- Đã giao thành công
(51, 5, 1),  -- Đã giao thành công

-- Tháng 9/2024
(52, 6, 1),  -- Đã giao thành công
(53, 2, 1),  -- Đã giao thành công
(54, 5, 1),  -- Đã giao thành công
(55, 6, 1),  -- Đã giao thành công

-- Tháng 10/2024
(56, 2, 1),  -- Đã giao thành công
(57, 5, 1),  -- Đã giao thành công
(58, 6, 1),  -- Đã giao thành công
(59, 2, 1),  -- Đã giao thành công

-- Tháng 11/2024
(60, 5, 1),  -- Đã giao thành công
(61, 6, 1),  -- Đã giao thành công
(62, 2, 1),  -- Đã giao thành công
(63, 5, 1),  -- Đã giao thành công

-- Tháng 12/2024
(64, 6, 1),  -- Đã giao thành công
(65, 2, 1),  -- Đã giao thành công
(66, 5, 1),  -- Đã giao thành công
(67, 6, 1),  -- Đã giao thành công

-- Năm 2025
-- Tháng 1/2025
(68, 2, 1),  -- Đã giao thành công
(69, 5, 1),  -- Đã giao thành công
(70, 6, 1),  -- Đã giao thành công
(71, 2, 1),  -- Đã giao thành công

-- Tháng 2/2025
(72, 5, 1),  -- Đã giao thành công
(73, 6, 1),  -- Đã giao thành công
(74, 2, 1),  -- Đã giao thành công
(75, 5, 1),  -- Đã giao thành công

-- Tháng 3/2025
(76, 6, 1),  -- Đã giao thành công
(77, 2, 1),  -- Đã giao thành công
(78, 5, 1),  -- Đã giao thành công
(79, 6, 1),  -- Đã giao thành công

-- Tháng 4/2025
(80, 2, 1),  -- Đã giao thành công
(81, 5, 1),  -- Đã giao thành công
(82, 6, 1),  -- Đã giao thành công
(83, 2, 1),  -- Đã giao thành công

-- Tháng 5/2025
(84, 5, 1),  -- Đã giao thành công
(85, 6, 1),  -- Đã giao thành công
(86, 2, 1),  -- Đã giao thành công
(87, 5, 1),  -- Đã giao thành công

-- Tháng 6/2025
(88, 6, 1),  -- Đã giao thành công
(89, 2, 1),  -- Đã giao thành công
(90, 5, 1),  -- Đã giao thành công
(91, 6, 1),  -- Đã giao thành công

-- Tháng 7/2025
(92, 2, 1),  -- Đã giao thành công
(93, 5, 1),  -- Đã giao thành công
(94, 6, 1),  -- Đã giao thành công
(95, 2, 1),  -- Đã giao thành công

-- Tháng 8/2025
(96, 5, 1),  -- Đã giao thành công
(97, 6, 1),  -- Đã giao thành công
(98, 2, 1),  -- Đã giao thành công
(99, 5, 1),  -- Đã giao thành công

-- Tháng 9/2025
(100, 6, 1),  -- Đã giao thành công
(101, 2, 1),  -- Đã giao thành công
(102, 5, 1),  -- Đã giao thành công
(103, 6, 1),  -- Đã giao thành công

-- Tháng 10/2025
(104, 2, 1),  -- Đã giao thành công
(105, 5, 1),  -- Đã giao thành công
(106, 6, 1),  -- Đã giao thành công
(107, 2, 1),  -- Đã giao thành công

-- Tháng 11/2025
(108, 5, 1),  -- Đã giao thành công
(109, 6, 1),  -- Đã giao thành công
(110, 2, 1),  -- Đã giao thành công
(111, 5, 1),  -- Đã giao thành công

-- Tháng 12/2025
(112, 6, 1),  -- Đã giao thành công
(113, 2, 1),  -- Đã giao thành công
(114, 5, 1),  -- Đã giao thành công
(115, 6, 1);  -- Đã giao thành công



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
-- Đang đổ dữ liệu mẫu cho bảng hoadon
SET IDENTITY_INSERT hoadon ON;

-- Dữ liệu năm 2024
-- Tháng 1/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(20, 1, NULL, N'Q7, HCM', 350000, '2024-01-05', 1, 1, 350000, 0),
(21, 2, 1, N'Q2, HCM', 450000, '2024-01-12', 1, 1, 450000, 0),
(22, 3, NULL, N'Thủ Đức, HCM', 275000, '2024-01-18', 1, 1, 275000, 0),
(23, 4, 6, N'Bình Thạnh, HCM', 520000, '2024-01-25', 1, 1, 520000, 0);

-- Tháng 2/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(24, 2, NULL, N'Q1, HCM', 380000, '2024-02-03', 1, 1, 380000, 0),
(25, 3, 1, N'Gò Vấp, HCM', 420000, '2024-02-10', 1, 1, 420000, 0),
(26, 1, NULL, N'Phú Nhuận, HCM', 310000, '2024-02-17', 1, 1, 310000, 0),
(27, 4, NULL, N'Q9, HCM', 490000, '2024-02-24', 1, 1, 490000, 0);

-- Tháng 3/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(28, 3, 6, N'Tân Bình, HCM', 360000, '2024-03-05', 1, 1, 360000, 0),
(29, 4, NULL, N'Q3, HCM', 430000, '2024-03-12', 1, 1, 430000, 0),
(30, 2, 1, N'Q7, HCM', 295000, '2024-03-19', 1, 1, 295000, 0),
(31, 1, NULL, N'Bình Tân, HCM', 510000, '2024-03-26', 1, 1, 510000, 0);

-- Tháng 4/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(32, 4, NULL, N'Q8, HCM', 370000, '2024-04-04', 1, 1, 370000, 0),
(33, 1, 6, N'Tân Phú, HCM', 440000, '2024-04-11', 1, 1, 440000, 0),
(34, 3, NULL, N'Q5, HCM', 320000, '2024-04-18', 1, 1, 320000, 0),
(35, 2, 1, N'Q12, HCM', 480000, '2024-04-25', 1, 1, 480000, 0);

-- Tháng 5/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(36, 1, NULL, N'Thủ Đức, HCM', 390000, '2024-05-02', 1, 1, 390000, 0),
(37, 2, NULL, N'Q1, HCM', 460000, '2024-05-09', 1, 1, 460000, 0),
(38, 4, 6, N'Bình Thạnh, HCM', 340000, '2024-05-16', 1, 1, 340000, 0),
(39, 3, 1, N'Q7, HCM', 530000, '2024-05-23', 1, 1, 530000, 0);

-- Tháng 6/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(40, 2, 6, N'Phú Nhuận, HCM', 410000, '2024-06-01', 1, 1, 410000, 0),
(41, 3, NULL, N'Q3, HCM', 470000, '2024-06-08', 1, 1, 470000, 0),
(42, 1, 1, N'Gò Vấp, HCM', 330000, '2024-06-15', 1, 1, 330000, 0),
(43, 4, NULL, N'Q9, HCM', 540000, '2024-06-22', 1, 1, 540000, 0);

-- Tháng 7/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(44, 3, NULL, N'Tân Bình, HCM', 400000, '2024-07-03', 1, 1, 400000, 0),
(45, 4, 1, N'Q5, HCM', 480000, '2024-07-10', 1, 1, 480000, 0),
(46, 2, NULL, N'Bình Tân, HCM', 350000, '2024-07-17', 1, 1, 350000, 0),
(47, 1, 6, N'Q12, HCM', 550000, '2024-07-24', 1, 1, 550000, 0);

-- Tháng 8/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(48, 4, 1, N'Q8, HCM', 420000, '2024-08-01', 1, 1, 420000, 0),
(49, 1, NULL, N'Tân Phú, HCM', 490000, '2024-08-08', 1, 1, 490000, 0),
(50, 3, 6, N'Q2, HCM', 360000, '2024-08-15', 1, 1, 360000, 0),
(51, 2, NULL, N'Thủ Đức, HCM', 560000, '2024-08-22', 1, 1, 560000, 0);

-- Tháng 9/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(52, 1, 6, N'Q1, HCM', 430000, '2024-09-02', 1, 1, 430000, 0),
(53, 2, NULL, N'Bình Thạnh, HCM', 500000, '2024-09-09', 1, 1, 500000, 0),
(54, 4, 1, N'Phú Nhuận, HCM', 370000, '2024-09-16', 1, 1, 370000, 0),
(55, 3, NULL, N'Q7, HCM', 570000, '2024-09-23', 1, 1, 570000, 0);

-- Tháng 10/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(56, 2, NULL, N'Q3, HCM', 440000, '2024-10-01', 1, 1, 440000, 0),
(57, 3, 1, N'Gò Vấp, HCM', 510000, '2024-10-08', 1, 1, 510000, 0),
(58, 1, NULL, N'Q9, HCM', 380000, '2024-10-15', 1, 1, 380000, 0),
(59, 4, 6, N'Tân Bình, HCM', 580000, '2024-10-22', 1, 1, 580000, 0);

-- Tháng 11/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(60, 3, 6, N'Q5, HCM', 450000, '2024-11-03', 1, 1, 450000, 0),
(61, 4, NULL, N'Bình Tân, HCM', 520000, '2024-11-10', 1, 1, 520000, 0),
(62, 2, 1, N'Q12, HCM', 390000, '2024-11-17', 1, 1, 390000, 0),
(63, 1, NULL, N'Q8, HCM', 590000, '2024-11-24', 1, 1, 590000, 0);

-- Tháng 12/2024
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(64, 4, 1, N'Tân Phú, HCM', 460000, '2024-12-01', 1, 1, 460000, 0),
(65, 1, NULL, N'Q2, HCM', 530000, '2024-12-08', 1, 1, 530000, 0),
(66, 3, 6, N'Thủ Đức, HCM', 400000, '2024-12-15', 1, 1, 400000, 0),
(67, 2, NULL, N'Q1, HCM', 600000, '2024-12-22', 1, 1, 600000, 0);

-- Dữ liệu năm 2025
-- Tháng 1/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(68, 1, NULL, N'Bình Thạnh, HCM', 470000, '2025-01-05', 1, 1, 470000, 0),
(69, 2, 1, N'Phú Nhuận, HCM', 540000, '2025-01-12', 1, 1, 540000, 0),
(70, 3, NULL, N'Q7, HCM', 410000, '2025-01-19', 1, 1, 410000, 0),
(71, 4, 6, N'Q3, HCM', 610000, '2025-01-26', 1, 1, 610000, 0);

-- Tháng 2/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(72, 2, NULL, N'Gò Vấp, HCM', 480000, '2025-02-02', 1, 1, 480000, 0),
(73, 3, 1, N'Q9, HCM', 550000, '2025-02-09', 1, 1, 550000, 0),
(74, 1, NULL, N'Tân Bình, HCM', 420000, '2025-02-16', 1, 1, 420000, 0),
(75, 4, NULL, N'Q5, HCM', 620000, '2025-02-23', 1, 1, 620000, 0);

-- Tháng 3/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(76, 3, 6, N'Bình Tân, HCM', 490000, '2025-03-02', 1, 1, 490000, 0),
(77, 4, NULL, N'Q12, HCM', 560000, '2025-03-09', 1, 1, 560000, 0),
(78, 2, 1, N'Q8, HCM', 430000, '2025-03-16', 1, 1, 430000, 0),
(79, 1, NULL, N'Tân Phú, HCM', 630000, '2025-03-23', 1, 1, 630000, 0);

-- Tháng 4/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(80, 4, NULL, N'Q2, HCM', 500000, '2025-04-06', 1, 1, 500000, 0),
(81, 1, 6, N'Thủ Đức, HCM', 570000, '2025-04-13', 1, 1, 570000, 0),
(82, 3, NULL, N'Q1, HCM', 440000, '2025-04-20', 1, 1, 440000, 0),
(83, 2, 1, N'Bình Thạnh, HCM', 640000, '2025-04-27', 1, 1, 640000, 0);

-- Tháng 5/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(84, 1, NULL, N'Phú Nhuận, HCM', 510000, '2025-05-04', 1, 1, 510000, 0),
(85, 2, NULL, N'Q7, HCM', 580000, '2025-05-11', 1, 1, 580000, 0),
(86, 4, 6, N'Q3, HCM', 450000, '2025-05-18', 1, 1, 450000, 0),
(87, 3, 1, N'Gò Vấp, HCM', 650000, '2025-05-25', 1, 1, 650000, 0);

-- Tháng 6/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(88, 2, 6, N'Q9, HCM', 520000, '2025-06-01', 1, 1, 520000, 0),
(89, 3, NULL, N'Tân Bình, HCM', 590000, '2025-06-08', 1, 1, 590000, 0),
(90, 1, 1, N'Q5, HCM', 460000, '2025-06-15', 1, 1, 460000, 0),
(91, 4, NULL, N'Bình Tân, HCM', 660000, '2025-06-22', 1, 1, 660000, 0);

-- Tháng 7/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(92, 3, NULL, N'Q12, HCM', 530000, '2025-07-06', 1, 1, 530000, 0),
(93, 4, 1, N'Q8, HCM', 600000, '2025-07-13', 1, 1, 600000, 0),
(94, 2, NULL, N'Tân Phú, HCM', 470000, '2025-07-20', 1, 1, 470000, 0),
(95, 1, 6, N'Q2, HCM', 670000, '2025-07-27', 1, 1, 670000, 0);

-- Tháng 8/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(96, 4, 1, N'Thủ Đức, HCM', 540000, '2025-08-03', 1, 1, 540000, 0),
(97, 1, NULL, N'Q1, HCM', 610000, '2025-08-10', 1, 1, 610000, 0),
(98, 3, 6, N'Bình Thạnh, HCM', 480000, '2025-08-17', 1, 1, 480000, 0),
(99, 2, NULL, N'Phú Nhuận, HCM', 680000, '2025-08-24', 1, 1, 680000, 0);

-- Tháng 9/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(100, 1, 6, N'Q7, HCM', 550000, '2025-09-07', 1, 1, 550000, 0),
(101, 2, NULL, N'Q3, HCM', 620000, '2025-09-14', 1, 1, 620000, 0),
(102, 4, 1, N'Gò Vấp, HCM', 490000, '2025-09-21', 1, 1, 490000, 0),
(103, 3, NULL, N'Q9, HCM', 690000, '2025-09-28', 1, 1, 690000, 0);

-- Tháng 10/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(104, 2, NULL, N'Tân Bình, HCM', 560000, '2025-10-05', 1, 1, 560000, 0),
(105, 3, 1, N'Q5, HCM', 630000, '2025-10-12', 1, 1, 630000, 0),
(106, 1, NULL, N'Bình Tân, HCM', 500000, '2025-10-19', 1, 1, 500000, 0),
(107, 4, 6, N'Q12, HCM', 700000, '2025-10-26', 1, 1, 700000, 0);

-- Tháng 11/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(108, 3, 6, N'Q8, HCM', 570000, '2025-11-02', 1, 1, 570000, 0),
(109, 4, NULL, N'Tân Phú, HCM', 640000, '2025-11-09', 1, 1, 640000, 0),
(110, 2, 1, N'Q2, HCM', 510000, '2025-11-16', 1, 1, 510000, 0),
(111, 1, NULL, N'Thủ Đức, HCM', 710000, '2025-11-23', 1, 1, 710000, 0);

-- Tháng 12/2025
INSERT INTO hoadon (MaHD, MaKH, MaKM, DiaChiGH, TongTienTT, NgayLapHD, TinhTrangHD, TinhTrangTT, SoTienNhan, SoTienTra) VALUES
(112, 4, 1, N'Q1, HCM', 580000, '2025-12-07', 1, 1, 580000, 0),
(113, 1, NULL, N'Bình Thạnh, HCM', 650000, '2025-12-14', 1, 1, 650000, 0),
(114, 3, 6, N'Phú Nhuận, HCM', 520000, '2025-12-21', 1, 1, 520000, 0),
(115, 2, NULL, N'Q7, HCM', 720000, '2025-12-28', 1, 1, 720000, 0);

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
(1, N'Thức ăn cho thú cưng'),
(2, N'Đồ chơi cho thú cưng'),
(3, N'Phụ kiện thú cưng'),
(4, N'Đồ chơi thú cưng'),
(5, N'Dụng cụ vệ sinh thú cưng'),
(6, N'Thuốc và thực phẩm chức năng')
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

-- Đang đổ dữ liệu cho bảng phieunhap - thêm phiếu nhập cho 12 tháng năm 2025
SET IDENTITY_INSERT phieunhap ON;

-- Tháng 1/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(6, 10500000, '2025-01-05', 1, 1, 7),
(7, 8750000, '2025-01-18', 1, 3, 8),
(8, 5300000, '2025-01-27', 0, 2, 6);

-- Tháng 2/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(9, 9200000, '2025-02-03', 1, 4, 7),
(10, 7600000, '2025-02-15', 1, 5, 8),
(11, 4800000, '2025-02-24', 0, 1, 6);

-- Tháng 3/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(12, 11300000, '2025-03-07', 1, 2, 7),
(13, 6500000, '2025-03-19', 1, 3, 8),
(14, 8900000, '2025-03-28', 0, 4, 6);

-- Tháng 4/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(15, 12400000, '2025-04-05', 1, 5, 7),
(16, 7800000, '2025-04-16', 1, 1, 8),
(17, 5600000, '2025-04-25', 0, 2, 6);

-- Tháng 5/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(18, 9700000, '2025-05-08', 1, 3, 7),
(19, 8200000, '2025-05-17', 1, 4, 8),
(20, 6300000, '2025-05-29', 0, 5, 6);

-- Tháng 6/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(21, 10900000, '2025-06-04', 1, 1, 7),
(22, 7400000, '2025-06-14', 1, 2, 8),
(23, 5800000, '2025-06-26', 0, 3, 6);

-- Tháng 7/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(24, 11800000, '2025-07-07', 1, 4, 7),
(25, 8600000, '2025-07-18', 1, 5, 8),
(26, 6100000, '2025-07-29', 0, 1, 6);

-- Tháng 8/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(27, 10200000, '2025-08-05', 1, 2, 7),
(28, 7900000, '2025-08-16', 1, 3, 8),
(29, 5500000, '2025-08-27', 0, 4, 6);

-- Tháng 9/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(30, 12100000, '2025-09-03', 1, 5, 7),
(31, 8300000, '2025-09-14', 1, 1, 8),
(32, 6700000, '2025-09-25', 0, 2, 6);

-- Tháng 10/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(33, 11500000, '2025-10-06', 1, 3, 7),
(34, 8800000, '2025-10-17', 1, 4, 8),
(35, 6400000, '2025-10-28', 0, 5, 6);

-- Tháng 11/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(36, 10700000, '2025-11-04', 1, 1, 7),
(37, 7500000, '2025-11-15', 1, 2, 8),
(38, 5900000, '2025-11-26', 0, 3, 6);

-- Tháng 12/2025
INSERT INTO phieunhap (MaPN, TongTienTT, NgayLapPN, TinhTrangTT, MaNCC, MaNV) VALUES
(39, 12300000, '2025-12-05', 1, 4, 7),
(40, 9100000, '2025-12-16', 1, 5, 8),
(41, 6800000, '2025-12-27', 0, 1, 6);

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
(1, N'Thức ăn khô cho mèo con', N'https://paddy.vn/cdn/shop/files/thuc-an-cho-meo-con-royal-canin-kitten-36.jpg?v=1737351672', 1, 1, N'Thức ăn cho thú cưng', N'Royal Canin', 198000, 0, 67, N'Nâu', 4.5, N'Mọi lứa tuổi', N'Đức', N'49x36x10 cm', N'Thịt bò tươi, gạo, vitamin tổng hợp', N'Thức ăn cao cấp cho mèo con', N'Định lượng theo cân nặng'),
(2, N'Snack cho mèo con', N'https://paddy.vn/cdn/shop/products/sup-thuong-smartheart-creamy-cho-cho-moi-lua-tuoi-paddy-11.jpg?v=1719994855', 1, 2, N'Thức ăn cho thú cưng', N'SmartHeat', 66000, 55000, 59, N'N', 4.8, N'Trên 3 tháng', N'Hàn Quốc', N'45x40x35 cm', N'Ngũ cốc nguyên hạt, thịt cá hồi, chất xơ', N'Thức ăn đặc biệt cho mèo con', N'Cho ăn 2 lần/ngày'),
(3, N'Thức ăn ướt cho thú cưng', N'https://paddy.vn/cdn/shop/files/1_c32845ed-cc0b-4ff9-b7c2-b05506da99a9.png?v=1707207549', 1, 3, N'Thức ăn cho thú cưng', N'Nutrisource', 482000, 0, 35, N'N', 2.4, N'Dưới 1 năm', N'Hàn Quốc', N'60x10x35 cm', N'Protein từ gà, chất béo, khoáng chất', N'Thức ăn hàng ngày cho thú cưng', N'Cho ăn 2 lần/ngày'),
(4, N'Thức ăn hạt mềm cho mèo con', N'https://paddy.vn/cdn/shop/files/hat-kucinta-cho-meo-moi-lua-tuoi_2_420x.png?v=1719220042', 1, 4, N'Thức ăn cho thú cưng', N'Iskhan', 318000, 0, 30, N'N', 4.1, N'Dưới 1 năm', N'Anh', N'58x32x10 cm', N'Thịt gà, rau củ, dầu cá', N'Thức ăn hàng ngày cho mèo con', N'Định lượng theo cân nặng'),
(5, N'Thức ăn hạt mềm cho mèo con', N'https://paddy.vn/cdn/shop/files/Dr.Clauder_sh_ng_420x.png?v=1730862424', 1, 5, N'Thức ăn cho thú cưng', N'Max Power Pet Food', 271000, 182000, 81, N'N', 3.8, N'Trên 6 tháng', N'Đức', N'25x37x9 cm', N'Ngũ cốc nguyên hạt, thịt bò, chất xơ', N'Bổ sung protein cho mèo con', N'Bảo quản nơi khô ráo'),
(6, N'Xương đồ chơi cho chó', N'https://paddy.vn/cdn/shop/products/xuong-go-djau-tron-doggyman-giam-ngua-rang-and-huan-luyen-cho-paddy-1_420x.jpg?v=1702354655', 2, 1, N'Đồ chơi cho thú cưng', N'Royal Canin', 104000, 12000, 100, N'N', 4.2, N'Trưởng thành', N'Nhật Bản', N'60x14x10 cm', N'Nhựa an toàn, cao su tự nhiên', N'Đồ chơi giải trí cho mèo con', N'Tránh để mèo con nuốt phải'),
(7, N'Xương đồ chơi cho chó', N'https://paddy.vn/cdn/shop/products/ta-go-doggyman-giam-ngua-rang-and-huan-luyen-cho-paddy-3_420x.jpg?v=1702363318', 2, 2, N'Đồ chơi cho thú cưng', N'SmartHeat', 343000, 0, 17, N'N', 2.8, N'Mọi lứa tuổi', N'Việt Nam', N'26x19x14 cm', N'Cao su không độc hại', N'Huấn luyện mèo con cơ bản', N'Thay thế khi bị hư hỏng'),
(8, N'Xương đồ chơi cho chó', N'https://paddy.vn/cdn/shop/products/xuong-cao-su-tpet-sieu-dai-cho-cho-nhai-gam-sach-rang-paddy-1_420x.jpg?v=1690718207', 2, 3, N'Đồ chơi cho thú cưng', N'Nutrisource', 250000, 202000, 83, N'N', 0.6, N'2-12 tháng', N'Nhật Bản', N'39x13x23 cm', N'Nhựa ABS cao cấp', N'Giảm stress cho thú cưng', N'Tránh để thú cưng nuốt phải'),
(9, N'Chuột đồ chơi cho mèo', N'https://paddy.vn/cdn/shop/products/7_197b0fc1-6997-4bf2-9c9a-4a1aba7c76e3_420x.jpg?v=1692338737', 2, 4, N'Đồ chơi cho thú cưng', N'Iskhan', 93000, 65000, 22, N'N', 1.6, N'2-12 tháng', N'Anh', N'39x21x7 cm', N'Nhựa an toàn, cao su tự nhiên', N'Giúp thú cưng vận động', N'Không để thú cưng tự chơi một mình'),
(10, N'Dây đồ chơi cho chó con', N'https://paddy.vn/cdn/shop/files/do-choi-cho-pups-afp-dog-toy-3-1690275877379_420x.jpg?v=1690721175', 2, 5, N'Đồ chơi cho thú cưng', N'Max Power Pet Food', 89000, 19000, 27, N'N', 0.4, N'Mọi lứa tuổi', N'Pháp', N'21x32x18 cm', N'Vải bông, catnip', N'Giảm stress cho chó con', N'Tránh để chó con nuốt phải'),
(11, N'Nón cho thú cưng', N'https://paddy.vn/cdn/shop/files/non-len-tet-cho-cho-meo-de-thuong_2_420x.png?v=1706171592', 3, 1, N'Phụ kiện thú cưng', N'Royal Canin', 462000, 0, 46, N'N', 0.9, N'Mọi lứa tuổi', N'Mỹ', N'13x32x23 cm', N'Nylon chống nước', N'Giữ ấm cho thú cưng', N'Vệ sinh thường xuyên'),
(12, N'Dây dắt mèo', N'https://paddy.vn/cdn/shop/files/day-dat-yem-cho-meo-balo-hop-vuong_420x.jpg?v=1745319504', 3, 2, N'Phụ kiện thú cưng', N'SmartHeat', 467000, 0, 79, N'N', 0.3, N'Trên 3 tháng', N'Nhật Bản', N'60x32x23 cm', N'Cotton mềm mại', N'Bảo vệ an toàn cho mèo', N'Vệ sinh thường xuyên'),
(13, N'Dây dắt chó con', N'https://paddy.vn/cdn/shop/products/set-day-dat-kem-vong-yem-vai-in-hoa-tiet-cho-cho-meo-paddy-1_420x.jpg?v=1700039162', 3, 3, N'Phụ kiện thú cưng', N'Nutrisource', 319000, 306000, 20, N'N', 1.0, N'Trên 3 tháng', N'Mỹ', N'27x12x35 cm', N'Vải polyester bền', N'Giữ ấm cho chó con', N'Kiểm tra độ an toàn thường xuyên'),
(14, N'Dây dắt mèo', N'https://paddy.vn/cdn/shop/files/day-dat-yem-cho-meo-hinh-thu-bong-1cm-x-120cm2_420x.png?v=1694500865', 3, 4, N'Phụ kiện thú cưng', N'Iskhan', 467000, 449000, 84, N'N', 3.8, N'Mọi lứa tuổi', N'Mỹ', N'53x31x8 cm', N'Vải polyester bền', N'Nhận diện mèo', N'Không đeo quá chặt'),
(15, N'Dây dắt thú cưng', N'https://paddy.vn/cdn/shop/products/day-dat-cho-meo-yem-thoang-khi_420x.jpg?v=1712904875', 3, 5, N'Phụ kiện thú cưng', N'Max Power Pet Food', 384000, 0, 22, N'N', 1.5, N'Trưởng thành', N'Việt Nam', N'52x33x37 cm', N'Da tổng hợp, khóa kim loại', N'Nhận diện thú cưng', N'Kiểm tra độ an toàn thường xuyên'),
(16, N'Xẻng hót phân chó con', N'https://bbpetshop.vn/themes/images/1538907419_40378_xenghotphanmeo4.jpg', 4, 1, N'Đồ chơi thú cưng', N'Royal Canin', 174000, 0, 96, N'N', 1.5, N'Dưới 1 năm', N'Việt Nam', N'21x37x11 cm', N'Silica gel, chất khử mùi', N'Tiện lợi khi dọn vệ sinh cho chó con', N'Loại bỏ chất thải thường xuyên'),
(17, N'Cát vệ sinh chó con', N'https://paddy.vn/cdn/shop/products/cat-ve-sinh-djau-nanh-kit-cat-soya-clump-cho-meo-7l-paddy-2_420x.jpg?v=1702635408', 4, 2, N'Đồ chơi thú cưng', N'SmartHeat', 162000, 135000, 85, N'N', 0.9, N'Trên 3 tháng', N'Việt Nam', N'35x34x30 cm', N'Bentonite, than hoạt tính', N'Khử mùi hiệu quả', N'Vệ sinh hàng ngày'),
(18, N'Túi đựng phân mèo', N'https://product.hstatic.net/200000350119/product/f640a6b0d3a7d2b863c58842a2e939a8.jpg_720x720q80_53df13daaa904a7cb370d2a131344ceb_master.jpg', 4, 3, N'Đồ chơi thú cưng', N'Nutrisource', 178000, 0, 34, N'N', 3.8, N'Trên 3 tháng', N'Việt Nam', N'12x35x12 cm', N'Bentonite, than hoạt tính', N'Dễ dàng vệ sinh cho mèo', N'Loại bỏ chất thải thường xuyên'),
(19, N'Tấm lót vệ sinh mèo con', N'https://pethouse.com.vn/wp-content/uploads/2023/01/ezgif-3-2203a41889.jpg', 4, 4, N'Đồ chơi thú cưng', N'Iskhan', 118000, 81000, 37, N'N', 4.0, N'Trên 6 tháng', N'Nhật Bản', N'16x38x33 cm', N'Giấy tái chế, chất khử trùng', N'Giữ vệ sinh cho mèo con', N'Thay hoàn toàn 1-2 lần/tuần'),
(20, N'Cát vệ sinh mèo', N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSZhC2tmigDDsMrS8iq8JE7OZ5mIQ1hOy8alA&s', 4, 5, N'Đồ chơi thú cưng', N'Max Power Pet Food', 172000, 0, 62, N'N', 3.2, N'2-12 tháng', N'Mỹ', N'51x15x13 cm', N'Đất sét tự nhiên, hương thơm', N'Tiện lợi khi dọn vệ sinh cho mèo', N'Thay hoàn toàn 1-2 lần/tuần'),
(21, N'Lược chải rụng lông mèo con', N'https://fagopet.vn/uploads/images/630487e79487f617f236da4e/luoc-chai-long-meo__3_.webp', 5, 1, N'Dụng cụ vệ sinh thú cưng', N'Royal Canin', 287000, 281000, 65, N'N', 3.9, N'Dưới 1 năm', N'Thái Lan', N'55x36x19 cm', N'Silicon y tế, cồn sát trùng', N'Làm sạch lông mèo con', N'Sử dụng đúng kỹ thuật'),
(22, N'Dung dịch vệ sinh tai mèo', N'https://fagopet.vn/storage/m5/ft/m5ftx9lp476u9bjs427xjecffgmu.webp', 5, 2, N'Dụng cụ vệ sinh thú cưng', N'SmartHeat', 459000, 0, 89, N'N', 1.2, N'Mọi lứa tuổi', N'Nhật Bản', N'28x17x21 cm', N'Lông tự nhiên, tay cầm gỗ', N'Loại bỏ lông rụng', N'Khen thưởng mèo sau khi hoàn thành'),
(23, N'Lược chải rụng lông thú cưng', N'https://down-vn.img.susercontent.com/file/0cc4fb917ca54eb528ad7faa8d33fa81', 5, 3, N'Dụng cụ vệ sinh thú cưng', N'Nutrisource', 54000, 0, 90, N'N', 2.6, N'Dưới 1 năm', N'Trung Quốc', N'42x36x9 cm', N'Silicon y tế, cồn sát trùng', N'Vệ sinh tai sạch sẽ', N'Cẩn thận khi sử dụng'),
(24, N'Dung dịch vệ sinh tai mèo', N'https://fagopet.vn/uploads/images/6306d27b9487f617f236dac5/dung-dich-ve-sinh-tai-cho-cho__2_.webp', 5, 4, N'Dụng cụ vệ sinh thú cưng', N'Iskhan', 401000, 314000, 15, N'N', 2.6, N'2-12 tháng', N'Nhật Bản', N'40x11x30 cm', N'Lông tự nhiên, tay cầm gỗ', N'Chăm sóc vệ sinh mèo', N'Khen thưởng mèo sau khi hoàn thành'),
(25, N'Bàn chải lông mèo', N'https://petto.vn/wp-content/uploads/2020/12/CCT015_4.jpg', 5, 5, N'Dụng cụ vệ sinh thú cưng', N'Max Power Pet Food', 208000, 196000, 56, N'N', 3.3, N'Trưởng thành', N'Thái Lan', N'58x40x27 cm', N'Kim loại cao cấp, cao su', N'Chăm sóc vệ sinh mèo', N'Khen thưởng mèo sau khi hoàn thành'),
(26, N'Sữa non cho mèo con', N'https://bizweb.dktcdn.net/thumb/grande/100/448/728/products/2ca815d0-0841-4e1d-8fa9-90267b0cb510.jpg?v=1647341097977', 6, 1, N'Thuốc và thực phẩm chức năng', N'Royal Canin', 438000, 341000, 88, N'N', 2.9, N'Mọi lứa tuổi', N'Anh', N'23x24x32 cm', N'Tảo biển, men vi sinh, kẽm', N'Bổ sung vitamin cho mèo con', N'Cho uống trực tiếp'),
(27, N'Vitamin tổng hợp mèo con', N'https://cdn.shopify.com/s/files/1/0624/1746/9697/files/4_9d090b17-d955-42f0-b22b-1ef0f8f2b8ac_600x600.jpg?v=1684386052', 6, 2, N'Thuốc và thực phẩm chức năng', N'SmartHeat', 224000, 0, 81, N'N', 0.4, N'Trên 3 tháng', N'Trung Quốc', N'14x12x25 cm', N'Tảo biển, men vi sinh, kẽm', N'Bổ sung vitamin cho mèo con', N'Sử dụng đều đặn hàng ngày'),
(28, N'Vitamin tổng hợp chó con', N'https://www.petmart.vn/wp-content/uploads/2016/01/thuoc-bo-sung-vitamin-tong-hop-cho-cho-vegebrand-fruit-vitamin.jpg', 6, 3, N'Thuốc và thực phẩm chức năng', N'Nutrisource', 241000, 0, 56, N'N', 0.8, N'2-12 tháng', N'Việt Nam', N'11x11x34 cm', N'Sữa non, protein, khoáng chất', N'Tăng cường hệ miễn dịch chó con', N'Liều lượng theo cân nặng'),
(29, N'Dầu cá omega-3 chó con', N'https://product.hstatic.net/200000264739/product/vien_dau_ca_1_d55bf58eea3e48c88162a4a8fc8632bf_master.jpg', 6, 4, N'Thuốc và thực phẩm chức năng', N'Iskhan', 177000, 0, 12, N'N', 3.4, N'Dưới 1 năm', N'Anh', N'49x22x40 cm', N'Glucosamine, chondroitin, MSM', N'Bổ sung dinh dưỡng cho chó con', N'Dùng theo chỉ định'),
(30, N'Vitamin tổng hợp chó con', N'https://bizweb.dktcdn.net/100/298/319/products/vietducpetsho-prosense-multivitamin-for-all-life.jpg?v=1572543878547', 6, 5, N'Thuốc và thực phẩm chức năng', N'Max Power Pet Food', 496000, 0, 40, N'N', 4.8, N'Mọi lứa tuổi', N'Anh', N'13x38x24 cm', N'Dầu cá, omega-3, omega-6', N'Bổ sung dinh dưỡng cho chó con', N'Cho uống trực tiếp');
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
(2, N'SmartHeat'),
(3, N'Nutrisource'),
(4, N'Iskhan'),
(5, N'Max Power Pet Food')
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
(1, N'https://i.pinimg.com/736x/bb/f0/40/bbf04028587621b681377b48acc25542.jpg', N'5 Bí quyết chăm sóc chó con mới sinh để bé khỏe mạnh và phát triển tốt', N'# 5 Bí quyết chăm sóc chó con mới sinh để bé khỏe mạnh và phát triển tốt

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
(2, N'https://i.pinimg.com/736x/0f/12/7f/0f127fb8cb75d762e29a1ff508d76442.jpg', N'Top 10 thức ăn cho mèo tốt nhất 2023 - Lựa chọn dinh dưỡng hàng đầu cho boss nhà bạn', N'# Top 10 thức ăn cho mèo tốt nhất 2023 - Lựa chọn dinh dưỡng hàng đầu cho boss nhà bạn

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
(3, N'https://i.pinimg.com/736x/0d/d3/d5/0dd3d50d779d86ee69b3884361bd6132.jpg', N'Hướng dẫn chi tiết cách tắm cho chó mèo đúng cách tại nhà - An toàn và hiệu quả', N'# Hướng dẫn chi tiết cách tắm cho chó mèo đúng cách tại nhà - An toàn và hiệu quả

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



Select * From tintuc