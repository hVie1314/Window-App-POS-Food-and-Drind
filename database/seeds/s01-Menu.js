/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('menu').del();

  // Inserts seed entries
  await knex('menu').insert([
    { monanid:1,tenmonan: 'Phở Bò', loaimonan: 'Món chính', gia: 45000, mota: 'Phở bò tái chín', imagepath: 'ms-appx:///Assets/Image/pho_bo.jpg', trangthai: 1 },
    { monanid:2,tenmonan: 'Bún Chả', loaimonan: 'Món chính', gia: 50000, mota: 'Bún chả', imagepath: 'ms-appx:///Assets/Image/bun_cha.jpg', trangthai: 1 },
    { monanid:3,tenmonan: 'Trà Sữa', loaimonan: 'Đồ uống', gia: 30000, mota: 'Trà sữa trân châu', imagepath: 'ms-appx:///Assets/Image/tra_sua.jpg', trangthai: 1 },
    { tenmonan: 'Phở Gân', loaimonan: 'Món chính', gia: 45000, mota: 'Phở gân', imagepath: 'ms-appx:///Assets/Image/pho_gan.jpg', trangthai: 1 },
    { tenmonan: 'Bún Thịt Nướng', loaimonan: 'Món chính', gia: 50000, mota: 'Bún thịt nướng', imagepath: 'ms-appx:///Assets/Image/bun_thit_nuong.jpg', trangthai: 1 },
    { tenmonan: 'Trà Tắc', loaimonan: 'Đồ uống', gia: 10000, mota: 'Trà tắc', imagepath: 'ms-appx:///Assets/Image/tra_tac.jpg', trangthai: 1 },
    { tenmonan: 'Phở Gà', loaimonan: 'Món chính', gia: 40000, mota: 'Phở gà', imagepath: 'ms-appx:///Assets/Image/pho_ga.jpg', trangthai: 1 },
    { tenmonan: 'Bún Riêu', loaimonan: 'Món chính', gia: 40000, mota: 'Bún riêu', imagepath: 'ms-appx:///Assets/Image/bun_rieu.jpg', trangthai: 1 },
    { tenmonan: 'Phở Chay', loaimonan: 'Món chính', gia: 30000, mota: 'Phở chay', imagepath: 'ms-appx:///Assets/Image/pho_chay.jpg', trangthai: 1 },
    { tenmonan: 'Bún Mộc', loaimonan: 'Món chính', gia: 50000, mota: 'Bún mộc', imagepath: 'ms-appx:///Assets/Image/bun_moc.jpg', trangthai: 1 },
    { tenmonan: 'Nước ép', loaimonan: 'Đồ uống', gia: 20000, mota: 'Nước ép', imagepath: 'ms-appx:///Assets/Image/nuoc_ep.jpg', trangthai: 1 },
    { tenmonan: 'Phở xào', loaimonan: 'Món chính', gia: 40000, mota: 'Phở xào', imagepath: 'ms-appx:///Assets/Image/pho_xao.jpg', trangthai: 1 },
    { tenmonan: 'Bún Ốc', loaimonan: 'Món chính', gia: 50000, mota: 'Bún ốc', imagepath: 'ms-appx:///Assets/Image/bun_oc.jpg', trangthai: 1 },
    { tenmonan: 'Cà Phê', loaimonan: 'Đồ uống', gia: 15000, mota: 'Cà phê', imagepath: 'ms-appx:///Assets/Image/ca_phe.jpg', trangthai: 1 },
    { tenmonan: 'Gỏi cuốn', loaimonan: 'Khai vị', gia: 30000, mota: 'Gỏi cuốn', imagepath: 'ms-appx:///Assets/Image/goi_cuon.jpg', trangthai: 1 },
    { tenmonan: 'Trái cây tươi', loaimonan: 'Tráng miệng', gia: 15000, mota: 'Trái cây tươi', imagepath: 'ms-appx:///Assets/Image/trai_cay_tuoi.jpg', trangthai: 1 },
    { tenmonan: 'Trái cây dầm', loaimonan: 'Tráng miệng', gia: 30000, mota: 'Trái cây dầm', imagepath: 'ms-appx:///Assets/Image/trai_cay_dam.jpg', trangthai: 1 },
    { tenmonan: 'Kem ốc quế', loaimonan: 'Tráng miệng', gia: 25000, mota: 'Kem ốc quế', imagepath: 'ms-appx:///Assets/Image/kem_oc_que.jpg', trangthai: 1 },
    { tenmonan: 'Sinh tố bơ', loaimonan: 'Đồ uống', gia: 20000, mota: 'Sinh tố bơ', imagepath: 'ms-appx:///Assets/Image/sinh_to_bo.jpg', trangthai: 1 },
    { tenmonan: 'Nước mía', loaimonan: 'Đồ uống', gia: 15000, mota: 'Nước mía', imagepath: 'ms-appx:///Assets/Image/nuoc_mia.jpg', trangthai: 1 },
    { tenmonan: 'Trà sữa', loaimonan: 'Đồ uống', gia: 30000, mota: 'Trà sữa', imagepath: 'ms-appx:///Assets/Image/tra_sua.jpg', trangthai: 1 },
    { tenmonan: 'Bia', loaimonan: 'Đồ uống có cồn', gia: 20000, mota: 'Bia', imagepath: 'ms-appx:///Assets/Image/bia.jpg', trangthai: 1 },
  ]);
};
