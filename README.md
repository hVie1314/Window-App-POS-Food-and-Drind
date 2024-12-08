# OrderFood

Đồ án môn lập trình Window

Sinh viên thực hiện:

20120092 - Trần Huy Hoàng

22120369 - Phan Văn Tiến

22120429 - Hoàng Quốc Việt

## Hướng dẫn tạo database

Sử dụng Docker chạy database

```bash
docker run -d --name db -e POSTGRES_PASSWORD=1234 -p 5432:5432 postgres
```
**_NOTE:_** Chuyển hướng tới tệp database của đồ án (Nếu chưa, dùng lệnh cd của terminal).
Nếu dùng docker thì chú ý service của postgres có đang chạy trên máy hay không. Nếu có thì cần ánh xạ qua port khác để tránh xung đột. Vd: -p 5431:5432. Hoặc tắt service postgres trên máy đi (khuyến khích dùng cách này để đồng nhất port).

Cấu hình lại file .env

## Sử dụng

Tạo database có tên pos, cài các thư viện cần thiết (npm, knex, dotenv, pg)

Nếu hiển thị lỗi không có quyền sử dụng các câu lệnh knex có thể sử dụng câu lệnh này cho session đang sử dụng

```python
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

Chạy migration
```python
knex migrate:latest
```
**_NOTE:_** Nếu đúng vào check database sẽ hiện ra 11 bảng. (Dùng extension Database Client JDBC để xem nếu xài docker. Còn nếu chạy trực tiếp postgre trên máy thì xem bằng pgAdmin).
```python
knex seed:run 
```

![image](https://github.com/user-attachments/assets/9190e979-a6a6-45a2-a3e1-d653274d9b18)

## Cấu hình DAO

```python
setx DB_HOST "localhost"
setx DB_PORT "5432"
setx DB_NAME "pos"
setx DB_USER "postgres"
setx DB_PASS "1234"
```

Các câu lệnh để tạo biến môi trường cho máy

Vào VS cài thêm extention như hình để có thể tương tác với postgrest
![image](https://github.com/user-attachments/assets/191d6591-4bc0-4574-92a4-f9148385a6e9)


Please make sure to update tests as appropriate.

