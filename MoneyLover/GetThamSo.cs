using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover
{
    class GetThamSo
    {
        MLContext _context;

        public GetThamSo()
        {
            _context = new MLContext();
            _context.ThamSos.Load();
        }

        public double SoTienGuiToiThieu
        {
            get { return _context.ThamSos.Local.First(x => x.TenThamSo == "SoTienGuiMIN").GiaTri; }
        }

        public double LaiSuatMacDinh
        {
            get { return _context.ThamSos.Local.First(x => x.TenThamSo == "LaiSuatMacDinh").GiaTri; }
        }

        public double SoTienGuiThemToiThieu
        {
            get { return _context.ThamSos.Local.First(x => x.TenThamSo == "SoTienGuiThemMIN").GiaTri; }
        }

        public double SoNgayGuiToiThieu
        {
            get { return _context.ThamSos.Local.First(x => x.TenThamSo == "SoNgayGuiMIN").GiaTri; }
        }
    }
}
