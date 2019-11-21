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
        private double soTienGuiToiThieu = 1000000;
        private double laiSuatMacDinh = 0.05;
        private double soTienGuiThemToiThieu = 100000;
        private double soNgayGuiToiThieu = 15;

        public GetThamSo()
        {
            try
            {
                _context = new MLContext();
                soTienGuiToiThieu = _context.ThamSos.First(x => x.TenThamSo == "SoTienGuiMIN").GiaTri;
                laiSuatMacDinh = _context.ThamSos.First(x => x.TenThamSo == "LaiSuatMacDinh").GiaTri;
                soTienGuiThemToiThieu = _context.ThamSos.First(x => x.TenThamSo == "SoTienGuiThemMIN").GiaTri;
                soNgayGuiToiThieu = _context.ThamSos.First(x => x.TenThamSo == "SoNgayGuiMIN").GiaTri;
            }
            catch { };
        }

        public double SoTienGuiToiThieu
        {
            get { return soTienGuiToiThieu; }
        }

        public double LaiSuatMacDinh
        {
            get { return laiSuatMacDinh; }
        }

        public double SoTienGuiThemToiThieu
        {
            get { return soTienGuiThemToiThieu; }
        }

        public double SoNgayGuiToiThieu
        {
            get { return soNgayGuiToiThieu; }
        }
    }
}
