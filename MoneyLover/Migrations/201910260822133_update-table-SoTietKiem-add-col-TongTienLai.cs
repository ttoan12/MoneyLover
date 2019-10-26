namespace MoneyLover.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableSoTietKiemaddcolTongTienLai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SoTietKiems", "TongTienLai", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SoTietKiems", "TongTienLai");
        }
    }
}
