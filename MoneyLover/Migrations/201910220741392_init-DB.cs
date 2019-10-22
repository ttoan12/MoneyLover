namespace MoneyLover.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class initDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        MaKH = c.String(nullable: false, maxLength: 128),
                        TenKH = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.MaKH);

            CreateTable(
                "dbo.KyHans",
                c => new
                    {
                        MaKyHan = c.String(nullable: false, maxLength: 128),
                        ThoiHan = c.Int(nullable: false),
                        LaiSuatNam = c.Double(nullable: false),
                        NganHang_MaNganHang = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaKyHan)
                .ForeignKey("dbo.NganHangs", t => t.NganHang_MaNganHang)
                .Index(t => t.NganHang_MaNganHang);

            CreateTable(
                "dbo.NganHangs",
                c => new
                    {
                        MaNganHang = c.String(nullable: false, maxLength: 128),
                        TenNganHang = c.String(),
                    })
                .PrimaryKey(t => t.MaNganHang);

            CreateTable(
                "dbo.SoTietKiems",
                c => new
                    {
                        MaSTK = c.String(nullable: false, maxLength: 128),
                        TongTienGoc = c.Double(nullable: false),
                        NgayMoSo = c.String(),
                        TinhTrang = c.String(),
                        LaiSuatKhongKyHan = c.Double(nullable: false),
                        KhachHang_MaKH = c.String(maxLength: 128),
                        KyHan_MaKyHan = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MaSTK)
                .ForeignKey("dbo.KhachHangs", t => t.KhachHang_MaKH)
                .ForeignKey("dbo.KyHans", t => t.KyHan_MaKyHan)
                .Index(t => t.KhachHang_MaKH)
                .Index(t => t.KyHan_MaKyHan);

            CreateTable(
                "dbo.ThamSoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenThamSo = c.String(),
                        GiaTri = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SoTietKiems", "KyHan_MaKyHan", "dbo.KyHans");
            DropForeignKey("dbo.SoTietKiems", "KhachHang_MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.KyHans", "NganHang_MaNganHang", "dbo.NganHangs");
            DropIndex("dbo.SoTietKiems", new[] { "KyHan_MaKyHan" });
            DropIndex("dbo.SoTietKiems", new[] { "KhachHang_MaKH" });
            DropIndex("dbo.KyHans", new[] { "NganHang_MaNganHang" });
            DropTable("dbo.ThamSoes");
            DropTable("dbo.SoTietKiems");
            DropTable("dbo.NganHangs");
            DropTable("dbo.KyHans");
            DropTable("dbo.KhachHangs");
        }
    }
}
