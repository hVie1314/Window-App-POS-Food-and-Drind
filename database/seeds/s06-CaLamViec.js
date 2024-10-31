/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('calamviec').del();

  // Inserts seed entries
  await knex('calamviec').insert([
    { nhanvienid: 1, giobatdau: '08:00:00', gioketthuc: '16:00:00', ngaylamviec: '2024-10-10' },
    { nhanvienid: 2, giobatdau: '16:00:00', gioketthuc: '22:00:00', ngaylamviec: '2024-10-10' }
  ]);
};

