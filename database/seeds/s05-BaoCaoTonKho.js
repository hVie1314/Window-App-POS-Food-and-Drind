/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('baocaotonkho').del();

  // Inserts seed entries
  await knex('baocaotonkho').insert([
    { ngaybaocao: '2024-10-10', nguyenlieuid: 1, soluongconlai: 15 },
    { ngaybaocao: '2024-10-10', nguyenlieuid: 2, soluongconlai: 40 },
    { ngaybaocao: '2024-10-10', nguyenlieuid: 3, soluongconlai: 95 }
  ]);
};

