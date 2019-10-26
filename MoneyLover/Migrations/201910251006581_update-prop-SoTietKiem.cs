namespace MoneyLover.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updatepropSoTietKiem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SoTietKiems", "LoaiTraLai", c => c.Int(nullable: false));
            AddColumn("dbo.SoTietKiems", "KhiDenHan", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.SoTietKiems", "KhiDenHan");
            DropColumn("dbo.SoTietKiems", "LoaiTraLai");
        }
    }
}
