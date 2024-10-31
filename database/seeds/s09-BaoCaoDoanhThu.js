/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('baocaodoanhthu').del();

  // Inserts seed entries
  await knex('baocaodoanhthu').insert([
    { ngaybaocao: '2024-10-10', doanhthu: 95000, hoadonid: 1 },
    { ngaybaocao: '2024-10-10', doanhthu: 30000, hoadonid: 2 }
  ]);
};