/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(`
    ALTER TABLE chitiethoadon
    ADD CONSTRAINT fk_hoadon_id
    FOREIGN KEY (hoadonid) REFERENCES hoadon (hoadonid)
    ON DELETE CASCADE
    ON UPDATE CASCADE`);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {
    await knex.raw(`
        ALTER TABLE chitiethoadon
DROP CONSTRAINT fk_hoadon_id;
    `);
};
