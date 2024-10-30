/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE BaoCaoDoanhThu (
            BaoCaoID SERIAL PRIMARY KEY,  
            NgayBaoCao DATE NOT NULL, 
            DoanhThu INT NOT NULL,  
            HoaDonID INT  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE BaoCaoDoanhThu;
    `);
};
