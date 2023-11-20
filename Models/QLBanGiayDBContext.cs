using Microsoft.EntityFrameworkCore;

namespace QuanLyBanGiay.Models
{
    public class QLBanGiayDBContext : DbContext
    {
        public QLBanGiayDBContext()
        {
        }

        public QLBanGiayDBContext(DbContextOptions<QLBanGiayDBContext> options) : base(options) { }

        public DbSet<TKAdmin> Admins { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DonHang> HoaDons { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ListKichThuoc> KichThuocs { get; set; }
        public DbSet<TaiKhoanKH> TaiKhoanKHs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Khai báo trong trường hợp tên Entity khác với tên bảng trong database 
            modelBuilder.Entity<TKAdmin>().ToTable("tbAdmin");
            modelBuilder.Entity<KhachHang>().ToTable("tbKhachHang");
            modelBuilder.Entity<SanPham>().ToTable("tbSanPham");
            modelBuilder.Entity<DonHang>().ToTable("tbHoaDon");
            modelBuilder.Entity<ChiTietDonHang>().ToTable("tbChiTietThanhToan");
            modelBuilder.Entity<ListKichThuoc>().ToTable("tbListKichThuoc"); 
            modelBuilder.Entity<TaiKhoanKH>().ToTable("tbAccount");


            //Khai báo trong trường hợp tên thuộc tính khác với tên cột trong bảng database 
            //(nếu đã khai báo Attribute [Column("MaSinhVien")] trong Model SinhVien thì không cần khai báo ở đây)

        }
    }
}
