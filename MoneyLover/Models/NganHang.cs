using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    class NganHang
    {
        [Key]
        public string MaNganHang { get; set; }
        public string TenNganHang { get; set; }

    }
}
