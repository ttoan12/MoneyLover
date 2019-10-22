namespace MoneyLover.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableKhachHang : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KhachHangs", "TenKH");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KhachHangs", "TenKH", c => c.String());
        }
    }
}
