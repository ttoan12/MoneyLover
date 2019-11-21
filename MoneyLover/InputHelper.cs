using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover
{
    public static class InputHelper
    {
        public static bool SoTienGui(string input, out double tienGui, out string msg)
        {
            var _thamso = new GetThamSo();
            if (double.TryParse(input, out tienGui))
            {
                if (tienGui < _thamso.SoTienGuiToiThieu)
                {
                    msg = "Số tiền gửi tối thiểu là " + _thamso.SoTienGuiToiThieu;
                    return false;
                }
                else
                {
                    msg = "";
                    return true;
                }
            }
            else
            {
                msg = "Hãy nhập số tiền cần gửi!";
                return false;
            }
        }

        public static bool SoTienGuiThem(string input, out double tienGui, out string msg)
        {
            var _thamso = new GetThamSo();
            if (double.TryParse(input, out tienGui))
            {
                if (tienGui < _thamso.SoTienGuiThemToiThieu)
                {
                    msg = "Số tiền gửi thêm tối thiểu là " + _thamso.SoTienGuiThemToiThieu;
                    return false;
                }
                else
                {
                    msg = "";
                    return true;
                }
            }
            else
            {
                msg = "Hãy nhập số tiền cần gửi!";
                return false;
            }
        }

        public static bool NgayGui(DateTime input, out string msg)
        {
            if (input.Month < DateTime.Now.Month || input.Day < DateTime.Now.Day)
            {
                msg = "Ngày gửi không được sớm hơn thời gian hiện tại!";
                return false;
            }
            else
            {
                msg = "";
                return true;
            }
        }

        public static bool LaiSuat(string input, out double laiSuat, out string msg)
        {
            if (!double.TryParse(input.Replace("%", ""), out laiSuat))
            {
                msg = "Hãy nhập lãi suất!";
                return false;
            }
            else
            {
                msg = "";
                return true;
            }
        }

        public static bool IsDenHan(DateTime ngayDenHan)
        {
            return (DateTime.Now - ngayDenHan).Days >= 0;
        }
    }
}
