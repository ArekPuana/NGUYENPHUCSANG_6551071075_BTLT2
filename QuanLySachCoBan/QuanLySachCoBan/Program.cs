using System;

class Sach
{
    private string _maSach;
    private string _tenSach;
    private string _tacGia;
    private int _namXuatBan;
    private double _giaBan;

    public Sach(string maSach, string tenSach, string tacGia, int namXuatBan, double giaBan)
    {
        this._maSach = maSach;
        this._tenSach = tenSach;
        this._tacGia = tacGia;
        this._namXuatBan = namXuatBan;
        this._giaBan = giaBan;
    }

    public Sach()
    {
        _maSach = "5000";
        _tenSach = "Chua co ten";
        _tacGia = "Chua co tac gia";
        _namXuatBan = DateTime.Now.Year;
        _giaBan = 0.0;
    }

    public string MaSach //chi doc
    {
        get { return _maSach; }
    }

    public string TenSach //doc va ghi, khong duoc rong
    {
        get { return _tenSach; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Ten sach khong duoc de trong!");
            }

            _tenSach = value;
        }
    }

    public int namXuatBan //doc va ghi, khong duoc nho hon 1900
    {
        get { return _namXuatBan; }
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
            {
                throw new ArgumentException("Nam xuat ban khong duoc nho hon 1900!");
            }
            _namXuatBan = value;
        }
    }

    public double GiaBan //chi doc
    {
        get { return _giaBan; }
    }

    public void HienThiThongTin()
    {
        Console.WriteLine("Ma sach: " + _maSach);
        Console.WriteLine("Ten sach: " + _tenSach);
        Console.WriteLine("Tac gia: " + _tacGia);
        Console.WriteLine("Nam xuat ban: " + _namXuatBan);
        Console.WriteLine("Gia ban: " + _giaBan);
    }

    public override string ToString()
    {
        return $"Ma sach: {_maSach}, Ten sach: {_tenSach}, Tac gia: {_tacGia}, Nam xuat ban: {_namXuatBan}, Gia ban: {_giaBan}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("MSSV: 6551071075");
        Console.WriteLine();

        // Tạo đối tượng bằng constructor có tham số
        Sach sach1 = new Sach("5001", "Lap trinh C#", "Nguyen Van A", 2020, 150000);

        //Tạo đối tượng bằng constructor không tham số rồi gắn property
        Sach sach2 = new Sach();
        sach2.TenSach = "Lap trinh Java";
        sach2.namXuatBan = 2021;

        //Tạo đối tượng bằng object initializer
        Sach sach3 = new Sach("5003", "Lap trinh Python", "Nguyen Van B", 2022, 200000);
        sach3.TenSach = "Lap trinh Python";
        
        Console.WriteLine("Thong tin sach 1:");
        Console.WriteLine();

        sach1.HienThiThongTin();
        sach2.HienThiThongTin();
        sach3.HienThiThongTin();
        Console.WriteLine();

        Console.WriteLine("Kiem tra du lieu nhap vao:");
        try
        {
            sach1.namXuatBan = 1800; // Sai du lieu
        }

        catch (ArgumentException ex)
        {
            Console.WriteLine("Loi: " + ex.Message);
        }
        Console.WriteLine();

        Console.WriteLine("Nhap phim bat ky de thoat chuong trinh...");
        Console.ReadKey();
    }
}
