using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    class SoTietKiem
    {
        [Key]
        public string MaSTK { get; set; }
        public double TongTienGoc { get; set; }
        public double TongTienLai { get; set; }
        public string NgayMoSo { get; set; }
        public string TinhTrang { get; set; }
        public double LaiSuatKhongKyHan { get; set; }
        public int LoaiTraLai { get; set; }
        public int KhiDenHan { get; set; }

        public KhachHang KhachHang { get; set; }
        public KyHan KyHan { get; set; }
    }
}
