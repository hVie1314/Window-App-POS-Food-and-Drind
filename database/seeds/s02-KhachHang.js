/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('khachhang').del();

  // Inserts seed entries
  await knex('khachhang').insert([
    { tenkhachhang: 'Đào Thiên Phước', sodienthoai: '0901234567', email: 'dtphuoc@gmail.com', diachi: '123 Đường Linh Trung, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Liễu Như Yên', sodienthoai: '0902345678', email: 'lnyen@gmail.com', diachi: '84 Đường Cách Mạng Tháng Tám, Hồ Chí Minh', loaikhachhang: 'VIP' },
    { tenkhachhang: 'Trần Huy Hoàng', sodienthoai: '0904847236', email: 'huyhoang02mgm@gmail.com', diachi: '84 Phạm Văn Đồng, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Lâm Gia Nghĩa', sodienthoai: '0979285731', email: 'lgnghia@gmail.com', diachi: '44 Đường Tú Xương, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Lê Hoàng Bửu', sodienthoai: '0732847364', email: 'lhbuu@gmail.com', diachi: '274 Đường Hai Bà Trưng, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Nguyễn Bùi Khương Duy', sodienthoai: '0274945732', email: 'nbkduy@gmail.com', diachi: '274 Đường Lê Lợi, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Hoàng Quốc Việt', sodienthoai: '0827484592', email: 'hqviet@gmail.com', diachi: '39 Đường Phan Văn Trị, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Đinh Thanh Tùng', sodienthoai: '0969696969', email: 'dttunglolicon@gmail.com', diachi: '349 Đường Lê Thánh Tôn, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Nguyễn Trường Vinh Sơn', sodienthoai: '0284945827', email: 'sonredflag@gmail.com', diachi: '75 Đường Lê Thánh Tôn, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Nguyễn Trí Thiệu', sodienthoai: '0283739485', email: 'thieuthesimp@gmail.com', diachi: '456 Đường Lê Duẩn, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Nguyễn Nhất Duy', sodienthoai: '0489104548', email: 'ndduy@gmail.com', diachi: '284 Đường Lê Đức Thọ, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Trần Công Minh', sodienthoai: '0258938545', email: 'tcminh@gmail.com', diachi: '1 Đường Hoàng Diệu, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Phạm Quốc Hùng', sodienthoai: '0729388347', email: 'rename02@gmail.com', diachi: '123 Đường Tú Xương, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Nguyễn Thế Tài', sodienthoai: '0782388341', email: 'nttai@gmail.com', diachi: '27 Đường Phan Xích Long, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Hoàng Kim Mạnh Hùng', sodienthoai: '0828371093', email: 'hkmhung@gmail.com', diachi: '19 Hoàng Diệu, Hồ Chí Minh', loaikhachhang: 'Thường xuyên' },
    { tenkhachhang: 'Phan Quan Tiến', sodienthoai: '0739482909', email: 'pqtien@gmail.com', diachi: '29 Đường Lê Lợi, Hồ Chí Minh', loaikhachhang: 'VIP' }
  ]);
};