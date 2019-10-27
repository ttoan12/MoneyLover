namespace MoneyLover.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updatetableSoTietKiemaddcolTienGuiThem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SoTietKiems", "TienGuiThem", c => c.Double(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.SoTietKiems", "TienGuiThem");
        }
    }
}
