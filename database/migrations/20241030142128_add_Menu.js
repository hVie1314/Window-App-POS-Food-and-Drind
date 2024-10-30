/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE Menu (
            MonAnID SERIAL PRIMARY KEY,  
            TenMonAn VARCHAR(100) NOT NULL,  
            LoaiMonAn VARCHAR(50),  
            Gia INT,  
            MoTa VARCHAR(255),  
            ImagePath VARCHAR(255),  
            TrangThai BOOLEAN NOT NULL DEFAULT TRUE  
        );
    `);
};

exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE Menu;
    `);
};
