/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('baocaotonkho').del();

  // Inserts seed entries
  await knex('baocaotonkho').insert([
    { baocaotonid: 1, ngaybaocao: '2024-10-10', nguyenlieuid: 1, soluongconlai: 15 },
    { baocaotonid: 2, ngaybaocao: '2024-10-10', nguyenlieuid: 2, soluongconlai: 40 },
    { baocaotonid: 3, ngaybaocao: '2024-10-10', nguyenlieuid: 3, soluongconlai: 95 }
  ]);
};

