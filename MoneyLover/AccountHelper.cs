using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover
{
    public static class AccountHelper
    {
        public static bool DangNhap(string mail, string psw, out string msg)
        {
            if (AccountValidate.IsMail(mail) && AccountValidate.IsPassword(psw))
            {
                MLContext _context = new MLContext();
                var kh = _context.KhachHangs.Where(x => x.Email == mail);
                if (kh.Count() > 0)
                {
                    if (psw == Encryptor.Decrypt(kh.FirstOrDefault().Password, kh.FirstOrDefault().MaKH))
                    {
                        msg = "";
                        return true;
                    }
                    else
                    {
                        msg = "Sai mật khẩu!";
                        return false;
                    }
                }
                else
                {
                    msg = "Sai tài khoản!";
                    return false;
                }

            }
            else
            {
                msg = "Sai định dạng email hoặc mật khẩu!";
                return false;
            }
        }

        public static bool DangKy(string mail, string psw, out KhachHang newKH, out string msg)
        {
            newKH = null;
            if (AccountValidate.IsMail(mail) && AccountValidate.IsPassword(psw))
            {
                MLContext _context = new Models.MLContext();
                var kh = _context.KhachHangs.Where(x => x.Email == mail);
                if (kh.Count() == 0)
                {
                    var listKH = _context.KhachHangs;

                    var lastNumID = 0;
                    if (listKH.Count() > 0)
                    {
                        lastNumID = int.Parse(listKH.OrderByDescending(x => x.MaKH).FirstOrDefault().MaKH.Split('_')[1]);
                    }

                    var makh = "KH_" + (lastNumID + 1).ToString();

                    newKH = new KhachHang()
                    {
                        MaKH = makh,
                        Email = mail,
                        Password = Encryptor.Encrypt(psw, makh)
                    };

                    try
                    {
                        _context.KhachHangs.Add(newKH);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        msg = "Máy chủ đang được bảo trì!";
                        return false;
                    }

                    msg = "";
                    return true;
                }
                else
                {
                    msg = "Email này đã được sử dụng!";
                    return false;
                }
            }
            else
            {
                msg = "Sai định dạng Email hoặc mật khẩu!";
                return false;
            }
        }

    }
}
