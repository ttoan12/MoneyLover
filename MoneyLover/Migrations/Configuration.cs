namespace MoneyLover.Migrations
{
    using MoneyLover.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyLover.Models.MLContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyLover.Models.MLContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.ThamSos.AddOrUpdate(ts => ts.Id,
                new ThamSo()
                {
                    Id = 1,
                    TenThamSo = "SoTienGuiMIN",
                    GiaTri = 1000000
                },
                new ThamSo()
                {
                    Id = 2,
                    TenThamSo = "LaiSuatMacDinh",
                    GiaTri = 0.05
                },
                new ThamSo()
                {
                    Id = 3,
                    TenThamSo = "SoTienGuiThemMIN",
                    GiaTri = 100000
                },
                new ThamSo()
                {
                    Id = 4,
                    TenThamSo = "SoNgayGuiMIN",
                    GiaTri = 15
                });
        }
    }
}
