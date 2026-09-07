using System;

namespace TinhLuongNhanVien
{
    class NhanVien
    {
        private string _maNV;
        private string _hoTen;
        private decimal _luongCoBan;
        private int _soNgayLam;
        private int _soNgayNghiPhep;

        //constructor khong tham so
        public NhanVien()
        {
            _maNV = "NV000";
            _hoTen = "Chưa có tên";
            _luongCoBan = 5_000_000;
            _soNgayLam = 26;
            _soNgayNghiPhep = 0;
        }

        // constructor chỉ nhận mã NV + họ tên
        public NhanVien(string maNV, string hoTen)
        {
            _maNV = maNV;
            _hoTen = hoTen;
            _luongCoBan = 5_000_000;
            _soNgayLam = 26;
            _soNgayNghiPhep = 0;
        }

        // constructor đầy đủ tham số
        public NhanVien(
            string maNV,
            string hoTen,
            decimal luongCoBan,
            int soNgayLam,
            int soNgayNghiPhep)
        {
            _maNV = maNV;
            _hoTen = hoTen;
            LuongCoBan = luongCoBan;
            SoNgayLam = soNgayLam;
            _soNgayNghiPhep = soNgayNghiPhep;
        }

        //constructor với optional parameters
        public NhanVien(
            string maNV,
            string hoTen,
            decimal luong = 5_000_000,
            int soNgayLam = 26)
        {
            _maNV = maNV;
            _hoTen = hoTen;
            LuongCoBan = luong;
            SoNgayLam = soNgayLam;
            _soNgayNghiPhep = 0;
        }

        //=======================================property
        public string HoTen
        {
            get
            {
                return _hoTen;
            }
            set
            {
                _hoTen = value;
            }
        }

        public decimal LuongCoBan
        {
            get
            {
                return _luongCoBan;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        "Lương cơ bản không được nhỏ hơn 0.");
                }

                _luongCoBan = value;
            }
        }

        public int SoNgayLam
        {
            get
            {
                return _soNgayLam;
            }
            set
            {
                if (value < 0 || value > 31)
                {
                    throw new ArgumentException(
                        "Số ngày làm phải từ 0 đến 31.");
                }

                _soNgayLam = value;
            }
        }

        public decimal LuongThucNhan
        {
            get
            {
                // Khấu trừ BHXH = 8% lương cơ bản
                decimal khauTruBHXH = _luongCoBan * 0.08m;

                // Lương theo số ngày làm
                decimal luongTheoNgay =
                    _luongCoBan / 26 * _soNgayLam;

                return luongTheoNgay - khauTruBHXH;
            }
        }

        // Không có tham số
        public decimal TinhThuong()
        {
            return 0;
        }


        // Có hệ số
        public decimal TinhThuong(decimal heSo)
        {
            return _luongCoBan * heSo;
        }


        // Có hệ số + phúc lợi
        public decimal TinhThuong(decimal heSo, bool coPhucLoi)
        {
            decimal thuong = _luongCoBan * heSo;

            if (coPhucLoi)
            {
                thuong += 500_000;
            }

            return thuong;
        }


        public void HienThiThongTin()
        {
            Console.WriteLine($"Mã NV: {_maNV}");
            Console.WriteLine($"Họ tên: {HoTen}");
            Console.WriteLine($"Lương cơ bản: {LuongCoBan:N0} đ");
            Console.WriteLine($"Số ngày làm: {SoNgayLam}");
            Console.WriteLine($"Lương thực nhận: {LuongThucNhan:N0} đ");
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
            Console.WriteLine();

            // NHÂN VIÊN 1
            // Constructor không tham số

            NhanVien nv1 = new NhanVien();


            // NHÂN VIÊN 2
            // Constructor chỉ nhận mã NV + họ tên

            NhanVien nv2 =
                new NhanVien("NV002", "Bình");


            // NHÂN VIÊN 3
            // Constructor đầy đủ tham số

            NhanVien nv3 =
                new NhanVien(
                    "NV003",
                    "Cường",
                    10_000_000,
                    25,
                    1);


            // SỬ DỤNG NAMED ARGUMENTS
            // VỚI OPTIONAL PARAMETERS

            NhanVien nv4 =
                new NhanVien(
                    maNV: "NV004",
                    hoTen: "An",
                    soNgayLam: 20);


            Console.WriteLine("----- NHÂN VIÊN 1 -----");
            nv1.HienThiThongTin();

            Console.WriteLine();
            Console.WriteLine("----- NHÂN VIÊN 2 -----");
            nv2.HienThiThongTin();

            Console.WriteLine();
            Console.WriteLine("----- NHÂN VIÊN 3 -----");
            nv3.HienThiThongTin();

            Console.WriteLine();
            Console.WriteLine("----- NHÂN VIÊN 4 -----");
            nv4.HienThiThongTin();

            Console.WriteLine();
            Console.WriteLine("SO SÁNH TIỀN THƯỞNG");

            decimal thuong1 = nv3.TinhThuong();
            decimal thuong2 = nv3.TinhThuong(0.1m);
            decimal thuong3 = nv3.TinhThuong(0.1m, true);


            Console.WriteLine($"TinhThuong(): {thuong1:N0} đ");
            Console.WriteLine($"TinhThuong(0.1m): {thuong2:N0} đ");
            Console.WriteLine($"TinhThuong(0.1m, true): {thuong3:N0} đ");

            // SO SÁNH
            Console.WriteLine();
            Console.WriteLine("KẾT QUẢ SO SÁNH");

            if (thuong1 > thuong2 && thuong1 > thuong3)
            {
                Console.WriteLine("TinhThuong() có tiền thưởng cao nhất.");
            }
            else if (thuong2 > thuong3)
            {
                Console.WriteLine("TinhThuong(heSo) có tiền thưởng cao nhất.");
            }
            else
            {
                Console.WriteLine("TinhThuong(heSo, coPhucLoi) có tiền thưởng cao nhất.");
            }
            Console.ReadKey();
        }
    }
}