/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE ChiTietHoaDon (
            ChiTietID SERIAL PRIMARY KEY,  
            HoaDonID INT NOT NULL,  
            MonAnID INT NOT NULL,  
            SoLuong INT NOT NULL,  
            GiaBan INT NOT NULL,  
            ThanhTien INT GENERATED ALWAYS AS (SoLuong * GiaBan) STORED,  
            GhiChu VARCHAR(255)  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE ChiTietHoaDon;
    `);
};