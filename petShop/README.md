Xin chào bạn đọc, đây là đồ án SOA - Kiến trúc phần mềm hướng dịch vụ của nhóm 2 SE001 - K48 - CTD - UEH. 
Đồng tác giả của dự án: 
1. Thới Trần Ngọc Thạch https://github.com/ngocthach041104 
2. Nguyễn Ngọc Tú Trinh https://github.com/Trinhnnt
3. Nguyễn Phụng Châu https://github.com/j1mmyhvstle
Nội dung của tài liệu này sẽ giúp bạn nắm được các vấn đề sau:
Thông tin cơ bản về website SOASHOP

#Nội dung này được dùng cho website SOASHOP phiên bản thử nghiệm, và có thể được cập nhật để phù hợp với các phiên bản tiếp theo. Hãy liên hệ với nhóm phát triển khi bạn gặp khó khăn và không giải quyết được với tài liệu này thông qua github hoặc email liên hệ sau:
1. THẠCH THỚI TRẦN NGỌC - thachthoi.31221025597@st.ueh.edu.vn
2. TRINH NGUYỄN NGỌC TÚ - trinhnguyen.31221024321@st.ueh.edu.vn
3. CHÂU NGUYỄN PHỤNG - chaunguyen.31221024746@st.ueh.edu.vn

SOASHOP là một website được xây dựng nhằm phục vụ cho môn học Kiến trúc phần mềm hướng dịch vụ. Mục tiêu xây dựng là để thực hành việc triển khai - áp dụng một kiến trúc cụ thể trong lập trình lên một sản phẩm cụ thể.
#Thông tin kỹ thuật:
SOASHOP được xây dựng dựa vào mô hình MVC (Model - View - Controller) và là một phần mềm chạy trên Internet.
   
   + Kiến trúc tổng thể: SOASHOP được xây dựng dựa trên mô hình MVC (Model-View-Controller), tạo nên một hệ thống bán đồ điện tử với sự phân tách rõ ràng giữa các thành phần, giúp dễ dàng bảo trì và mở rộng.
   
   + Tầng Model: Quản lý toàn bộ business logic và tương tác với cơ sở dữ liệu, bao gồm các entity chính như KhachHang, CTHD (Chi tiết hóa đơn), và SanPhamContext, đảm bảo tính toàn vẹn dữ liệu và logic nghiệp vụ.
   
   + Tầng Controller: Đóng vai trò điều phối, xử lý các request từ người dùng và tương tác với Model để lấy/cập nhật dữ liệu, sau đó trả về View phù hợp. Các controller chính bao gồm SanPhamController, CartController và AccountController.
   
   + Tầng View: Hiển thị giao diện người dùng, được tổ chức theo cấu trúc thư mục rõ ràng với layout chung và các view riêng cho từng chức năng như quản lý sản phẩm, giỏ hàng, và quản lý đơn hàng.
   
   + Ưu điểm nổi bật: 
      - Tuân thủ nguyên lý Separation of Concerns
      - Cấu trúc project rõ ràng và dễ quản lý
      - Tích hợp đầy đủ các chức năng cơ bản của một hệ thống thương mại điện tử
      - Có tài liệu chi tiết bao gồm sơ đồ UseCase, Activity Diagram và Sequence Diagram
   
   + Khả năng mở rộng: Kiến trúc cho phép dễ dàng thêm mới các tính năng và module mà không ảnh hưởng đến các thành phần hiện có, đồng thời hỗ trợ việc tích hợp các công nghệ mới trong tương lai.

SOASHOP có cơ sở dữ liệu được xây dựng có thể phục vụ tốt công tác thử nghiệm và cải tiến. 
Một số chức năng chính mà nhóm đã thực hiện được 
# Phía người dùng:
1. Đăng Nhập
2. Đăng xuất
3. Đăng ký
4. Trang chủ
5. Sản phẩm: Chức năng này bao gồm một số chức năng khác như xem chi tiết sản phẩm, bình luận, lọc sản phẩm theo điều kiện, tìm kiếm sản phẩm
6. Giỏ hàng
7. Đặt hàng
8. Chỉnh sửa thông tin cá nhân
9. Đọc tin tức
# Người quản trị
1. Đăng nhập
2. Đăng xuất
3. Thống kê bao gồm
   + Thống kê danh sách khách hàng có chi tiêu nhiều nhất
   + Thống kê danh sách hóa đơn
   + Thông tin liên hệ khách hàng
   + Thống kê doanh thu theo tháng
4. Quản lý tin tức
5. Quản lý nhà cung cấp
6. Quản lý nhập hàng
7. Quản lý khuyến mãi
8. Bình luận
9. Quản lý hóa đơn
10. Quản lý giao hàng
11. Quản lý khách hàng
12. Cài đặt hệ thống
13. Quản lý nhân sự
Hiện tại nhóm vẫn tiếp tục phát triển thêm giao diện trực quan, bắt mắt hơn, một số tính năng chưa được hoàn thiện đầy đủ như tính năng thanh toán vẫn trong quá trình hoàn thiện và phát triển, một số lỗi vẫn còn...
Bắt đầu sử dụng phần mềm

Nội dung của mục này sẽ giúp bạn nắm được các vấn đề sau
Truy cập vào phần mềm,
Sử dụng các chức năng dành cho người dùng thông thường và quản trị viên
Thoát khỏi phần mềm
Truy cập vào website
Hiện tại, phần mềm chỉ có thể chạy được trên tên miền cục bộ của máy khởi chạy. Các bước để truy cập phần mềm như sau:
Bước 1: Tải về và giải nén thư mục 
Bước 2: Tải về và khởi động phần mềm        
Bước 3: Khởi động Apache và MySQL trên Xampp.
![image](https://github.com/user-attachments/assets/af061cb6-8bf4-4db9-9e78-1413cbb5e96a)

Cần đảm bảo được cả Apache và MySQL đều khởi động và MySQL đã được cài đặt trong máy, nếu chưa hãy truy cập link sau để xem hướng dẫn cài đặt 
https://www.youtube.com/watch?v=BxdSUGBs0gM&t=533s

Khi gặp lỗi không truy cập được MySQL, bạn có thể tham khảo link sau: https://www.youtube.com/watch?v=pVG6-FH9zGs&pp=0gcJCfcAhR29_xXO

Bước 4: Chạy tệp DOAN-SW.sln trong thư mục SOAnhom2-master đã giải nén, website sau khi khởi chạy thành công sẽ có giao diện chính như sau:
![image](https://github.com/user-attachments/assets/40e13197-a2af-4067-8329-79f34911e64c)
Một số hình ảnh của hệ thống 

![image](https://github.com/user-attachments/assets/2d34a56d-efd9-48e3-90e0-74540a83e32b)

![image](https://github.com/user-attachments/assets/6c3c53a6-14dd-4f4b-a1c8-f9aedfafdcee)

Quản lý bình luận

![image](https://github.com/user-attachments/assets/95bb7067-f901-4567-8ff4-e22238cee3e4)

Thông tin tài khoản 

![image](https://github.com/user-attachments/assets/3e621a12-917d-4609-993b-523a32745ddc)

Quản lý sản phẩm 

![image](https://github.com/user-attachments/assets/c6409b7d-7193-4493-a711-6db8a5940c2e)

Thống kê

![image](https://github.com/user-attachments/assets/ab316662-6005-4481-a367-3d1f010998c4)

Cảm ơn bạn đã đọc, chúc bạn một ngày vui vẻ. 
