namespace QuanLyBanGiay.Models
{
    public class GioHang
    {
        public List<MatHang> matHangs { get; set; } = new List<MatHang>(); 
        public void ThemMatHang(SanPham sp, int soLuong)
        {
            MatHang? hang = matHangs.Where(m => m.sanpham.ID == sp.ID).FirstOrDefault();
            if (hang == null)
            {
                matHangs.Add(new MatHang()
                {
                    sanpham = sp,
                    SoLuong= soLuong,
                });
            }
            else
            {
                hang.SoLuong += soLuong;
            }
        }

        public void TinhMatHang(SanPham sp)
        {
            MatHang? hang = matHangs.Where(m => m.sanpham.ID ==sp.ID).FirstOrDefault();
            if (hang != null)
            {
                matHangs.Sum(m => m.sanpham.GiaBan * m.SoLuong);
            }
        }

        public void XoaMatHang(SanPham sp) => matHangs.RemoveAll(m => m.sanpham.ID == sp.ID);
        
        public double TinhTongTien() => matHangs.Sum(m => m.sanpham.GiaBan * m.SoLuong);
        public void XoaGioHang() => matHangs.Clear();
        //public int MaSP { get; set; }
        //public string TenSP { get; set; }
        //public string KichCoSP { get; set; }
        //public string MauSP { get; set; }
        //public string AnhSP { get; set; }
        //public int SoLuongSP { get; set; }
        //public double DonGiaSP { get; set; }
        //public double ThanhTien
        //{
        //    get
        //    {
        //        return SoLuongSP * DonGiaSP;
        //    }
        //}

        
    }

    public class MatHang
    {
        public int MatHangID { get; set; }
        public SanPham sanpham { get; set; } = new();
        public int SoLuong { get; set; }
    }
}
