/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE KhachHang (
            KhachHangID SERIAL PRIMARY KEY,  
            TenKhachHang VARCHAR(100) NOT NULL,  
            SoDienThoai VARCHAR(15),  
            Email VARCHAR(100),  
            DiaChi VARCHAR(255),  
            LoaiKhachHang VARCHAR(50)  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE KhachHang;
    `);
};