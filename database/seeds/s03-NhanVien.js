/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('nhanvien').del();

  // Inserts seed entries
  await knex('nhanvien').insert([
    { nhanvienid: 1, tennhanvien: 'Trần Cao Thị Thủy Tiên', chucvu: 'Phục vụ', luong: 5000000, ngayvaolam: '2024-01-15', trangthai: 1 },
    { nhanvienid: 2, tennhanvien: 'Lê Ngọc Trâm', chucvu: 'Thu ngân', luong: 6000000, ngayvaolam: '2024-02-15', trangthai: 1 },
    { nhanvienid: 3, tennhanvien: 'Nguyễn Thị Cẩm Tú', chucvu: 'Phục vụ', luong: 5000000, ngayvaolam: '2024-03-15', trangthai: 1 },
    { nhanvienid: 4, tennhanvien: 'Nguyễn Minh Thư', chucvu: 'Đầu bếp', luong: 6000000, ngayvaolam: '2024-04-01', trangthai: 0 },
    { nhanvienid: 5, tennhanvien: 'Lê Thảo Uyên', chucvu: 'Đầu bếp', luong: 6000000, ngayvaolam: '2024-01-07', trangthai: 1 },
    { nhanvienid: 6, tennhanvien: 'Nguyễn Thạch Thảo', chucvu: 'Phục vụ', luong: 5000000, ngayvaolam: '2024-02-10', trangthai: 1 },
    { nhanvienid: 7, tennhanvien: 'Dương Hồng Tuyết', chucvu: 'Phục vụ', luong: 5000000, ngayvaolam: '2024-03-15', trangthai: 1 },
    { nhanvienid: 8, tennhanvien: 'Nguyễn Tuyết Ngân', chucvu: 'Phục vụ', luong: 5000000, ngayvaolam: '2024-02-19', trangthai: 0 }
  ]);
};
