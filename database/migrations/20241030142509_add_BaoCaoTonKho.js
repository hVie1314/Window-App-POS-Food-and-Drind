/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE BaoCaoTonKho (
            BaoCaoTonID SERIAL PRIMARY KEY,  
            NgayBaoCao DATE NOT NULL,  
            NguyenLieuID INT NOT NULL,  
            SoLuongConLai INT NOT NULL  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE BaoCaoTonKho;
    `);
};