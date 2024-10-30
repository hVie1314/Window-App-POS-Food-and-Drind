/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE CaLamViec (
            CaLamViecID SERIAL PRIMARY KEY,  
            NhanVienID INT,  
            GioBatDau TIME NOT NULL,  
            GioKetThuc TIME NOT NULL,  
            NgayLamViec DATE 
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE CaLamViec;
    `);
};