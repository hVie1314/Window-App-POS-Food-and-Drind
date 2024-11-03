/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('chitiethoadon').del();

  // Inserts seed entries
  await knex('chitiethoadon').insert([
    { hoadonid: 1, monanid: 1, soluong: 1, giaban: 45000, ghichu: 'Không hành' },
    { hoadonid: 1, monanid: 2, soluong: 1, giaban: 50000, ghichu: null },
    { hoadonid: 2, monanid: 3, soluong: 1, giaban: 30000, ghichu: 'Ít đá' }
  ]);
};