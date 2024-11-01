/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE NhanVien (
            NhanVienID SERIAL PRIMARY KEY,  
            TenNhanVien VARCHAR(100), 
            ChucVu VARCHAR(50),  
            Luong DECIMAL(10, 2) NOT NULL,  
            NgayVaoLam DATE,  
            TrangThai BOOLEAN NOT NULL DEFAULT TRUE  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE NhanVien;
    `);
};