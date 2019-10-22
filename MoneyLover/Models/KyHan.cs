using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    class KyHan
    {
        [Key]
        public string MaKyHan { get; set; }
        public int ThoiHan { get; set; }
        public double LaiSuatNam { get; set; }

        public NganHang NganHang { get; set; }
    }
}
