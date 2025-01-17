/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE HoaDon (
            HoaDonID SERIAL PRIMARY KEY,  
            NgayLapHoaDon DATE NOT NULL,  
            TongTien INT NOT NULL,  
            PhuongThucThanhToan VARCHAR(50),  
            KhachHangID INT,  
            NhanVienID INT,  
            GiamGia INT DEFAULT 0,  
            ThueVAT DECIMAL(5, 2) DEFAULT 10,  
            GhiChu VARCHAR(255)  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE HoaDon;
    `);
};