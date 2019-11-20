using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover
{
    public static class AccountValidate
    {
        public static bool IsMail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch
            {
                MessageBox.Show("Nhập sai định dạng email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool IsPassword(string psw)
        {
            var hasWord = new Regex(@"[a-zA-Z]+");
            var hasDigit = new Regex(@"[0-9]+");
            var hasSpecialChar = new Regex(@"[^\w\d]+");
            if (psw.Length >= 8)
            {
                if (hasWord.IsMatch(psw) && hasDigit.IsMatch(psw) && hasSpecialChar.IsMatch(psw))
                    return true;
                else
                {
                    MessageBox.Show("Nhập sai định dạng mật khẩu!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu tối thiểu có 8 ký tự!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
