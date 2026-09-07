using System;
using System.Collections.Generic;

namespace QuanLySanPhamCuaHang
{
  class SanPham
    {
        private string _maSP;
        private string _tenSP;
        private decimal _gia;
        private int _soLuongTon;

        public string MaSP
        {
            get { return _maSP; }
            set { _maSP = value; }
        }

        public string TenSP
        {
            get { return _tenSP; }
            set { _tenSP = value; }
        }

        public decimal Gia
        {
            get { return _gia; }
            set { _gia = value; }
        }

        public int SoLuongTon
        {
            get { return _soLuongTon; }
            set { _soLuongTon = value; }
        }

        // Constructor đầy đủ
        public SanPham(
            string maSP,
            string tenSP,
            decimal gia,
            int soLuongTon)
        {
            _maSP = maSP;
            _tenSP = tenSP;
            _gia = gia;
            _soLuongTon = soLuongTon;
        }

        // Tính giá bán
        public virtual decimal TinhGiaBan()
        {
            return _gia;
        }

        // Mô tả sản phẩm
        public virtual string MoTa()
        {
            return $"Mã SP: {_maSP}, Tên SP: {_tenSP}, Giá: {_gia:N0} đ";
        }
    }

    //SAN PHAM THUC PHAM
    class SanPhamThucPham : SanPham
    {
        private DateTime _ngayHetHan;
        private int _nhietDoBAoquan;

        // Constructor gọi base()
        public SanPhamThucPham(
            string maSP,
            string tenSP,
            decimal gia,
            int soLuongTon,
            DateTime ngayHetHan,
            int nhietDoBAoquan)
            : base(maSP, tenSP, gia, soLuongTon)
        {
            _ngayHetHan = ngayHetHan;
            _nhietDoBAoquan = nhietDoBAoquan;
        }

        // Tính giá bán
        public override decimal TinhGiaBan()
        {
            // Số ngày còn lại đến ngày hết hạn
            int soNgayConLai =
                (_ngayHetHan.Date - DateTime.Now.Date).Days;

            // Nếu còn 3 ngày hoặc ít hơn thì giảm 30%
            if (soNgayConLai <= 3 && soNgayConLai >= 0)
            {
                return Gia * 0.7m;
            }

            return Gia;
        }

        public override string MoTa()
        {
            return $"[Thực phẩm] {MaSP} - {TenSP} | " +
                   $"HSD: {_ngayHetHan:dd/MM/yyyy} | " +
                   $"Bảo quản: {_nhietDoBAoquan}°C";
        }
    }

    // SAN PHAM DIEN TU
    class SanPhamDienTu : SanPham
    {
        private int _baoHanhThang;
        private string _hangSanXuat;

        // Constructor gọi base()
        public SanPhamDienTu(
            string maSP,
            string tenSP,
            decimal gia,
            int soLuongTon,
            int baoHanhThang,
            string hangSanXuat)
            : base(maSP, tenSP, gia, soLuongTon)
        {
            _baoHanhThang = baoHanhThang;
            _hangSanXuat = hangSanXuat;
        }

        // Tính giá bán
        public override decimal TinhGiaBan()
        {
            // Nếu bảo hành > 12 tháng
            // thì cộng thêm 10%
            if (_baoHanhThang > 12)
            {
                return Gia * 1.1m;
            }

            return Gia;
        }

        // Mô tả
        public override string MoTa()
        {
            return $"[Điện tử] {MaSP} - {TenSP} | " +
                   $"Hãng: {_hangSanXuat} | " +
                   $"Bảo hành: {_baoHanhThang} tháng";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding =
                System.Text.Encoding.UTF8;

            // MSSV
            Console.WriteLine("MSSV: 6551071075");
            Console.WriteLine("       QUẢN LÝ SẢN PHẨM CỬA HÀNG");
            Console.WriteLine();


            // TẠO LIST<SanPham>
            List<SanPham> danhSachSanPham =
                new List<SanPham>
                {
                    // Sản phẩm thường
                    new SanPham(
                        "SP001",
                        "Bút bi",
                        5000,
                        100),

                    // Sản phẩm thực phẩm
                    new SanPhamThucPham(
                        "TP001",
                        "Sữa tươi",
                        30000,
                        50,
                        DateTime.Now.AddDays(2),
                        4),

                    // Sản phẩm thực phẩm
                    new SanPhamThucPham(
                        "TP002",
                        "Bánh mì",
                        20000,
                        30,
                        DateTime.Now.AddDays(20),
                        25),

                    // Sản phẩm điện tử
                    new SanPhamDienTu(
                        "DT001",
                        "Tai nghe",
                        500000,
                        20,
                        24,
                        "Sony"),

                    // Sản phẩm điện tử
                    new SanPhamDienTu(
                        "DT002",
                        "Chuột máy tính",
                        300000,
                        15,
                        12,
                        "Logitech")
                };


            // DUYỆT DANH SÁCH

            decimal tongGiaTriKho = 0;

            foreach (SanPham sanPham in danhSachSanPham)
            {
                // Đa hình:
                // C# tự gọi TinhGiaBan() của class
                // tương ứng tại runtime
                decimal giaBan =
                    sanPham.TinhGiaBan();

                Console.WriteLine(sanPham.MoTa());
                Console.WriteLine(
                    $"Giá gốc: {sanPham.Gia:N0} đ");
                Console.WriteLine(
                    $"Giá bán: {giaBan:N0} đ");
                Console.WriteLine(
                    $"Số lượng tồn: {sanPham.SoLuongTon}");

                Console.WriteLine(
                    $"Giá trị tồn kho: " +
                    $"{giaBan * sanPham.SoLuongTon:N0} đ");

                Console.WriteLine(
                    new string('-', 50));

                // Tổng giá trị kho hàng
                tongGiaTriKho +=
                    sanPham.Gia * sanPham.SoLuongTon;
            }


            // TỔNG GIÁ TRỊ KHO HÀNG
            Console.WriteLine();
            Console.WriteLine("        TỔNG GIÁ TRỊ KHO HÀNG");

            Console.WriteLine(
                $"Tổng giá trị kho: " +
                $"{tongGiaTriKho:N0} đ");


            Console.ReadKey();
        }
    }
}