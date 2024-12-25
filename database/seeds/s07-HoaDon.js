/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('hoadon').del();

  // Inserts seed entries
  await knex('hoadon').insert([
    { hoadonid:1, ngaylaphoadon: '2024-10-10', tongtien: 95000, phuongthucthanhtoan: 'Tiền mặt', khachhangid: 1, nhanvienid: 2, giamgia: 5, thuevat: 10, ghichu: 'Khách hàng VIP giảm giá' },
    {hoadonid:2, ngaylaphoadon: '2024-10-10', tongtien: 30000, phuongthucthanhtoan: 'Thẻ', khachhangid: 2, nhanvienid: 1, giamgia: 0, thuevat: 10, ghichu: null }
  ]);
};

