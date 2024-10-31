/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('khohang').del();

  // Inserts seed entries
  await knex('khohang').insert([
    { tennguyenlieu: 'Gạo', soluongton: 100, donvitinh: 'kg', ngaynhapkho: '2024-10-01', ngayhethan: '2025-10-01' },
    { tennguyenlieu: 'Bánh Phở', soluongton: 50, donvitinh: 'kg', ngaynhapkho: '2024-09-15', ngayhethan: '2025-09-15' },
    { tennguyenlieu: 'Muối', soluongton: 30, donvitinh: 'kg', ngaynhapkho: '2024-10-05', ngayhethan: '2025-10-05' }
  ]);
};

