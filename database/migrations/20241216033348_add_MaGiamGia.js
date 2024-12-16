/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
        CREATE TABLE MaGiamGia (
            id SERIAL PRIMARY KEY,  
            MaGiamGia VARCHAR(50) NOT NULL,  
            TriGia INT NOT NULL  
        );
    `);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
        DROP TABLE MaGiamGia;
    `);
};
