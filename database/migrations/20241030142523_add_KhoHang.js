/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE KhoHang (
            KhoID SERIAL PRIMARY KEY,  
            TenNguyenLieu VARCHAR(100) NOT NULL,  
            SoLuongTon INT NOT NULL,  
            DonViTinh VARCHAR(20),  
            NgayNhapKho DATE NOT NULL,  
            NgayHetHan DATE  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE KhoHang;
    `);
};