using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyLover;
using MoneyLover.Models;

namespace MLTest
{
    [TestClass]
    public class AccountValidateTest
    {
        [TestMethod]
        public void IsMail()
        {
            var emailaddress = "abc@gmail.com";

            var result = AccountValidate.IsMail(emailaddress);

            var expected = true;

            Assert.AreEqual(expected, result, "Sai rồi");
        }

        [TestMethod]
        public void IsPassword()
        {
            var psw = "@bc123456";

            var result = AccountValidate.IsPassword(psw);

            var expected = true;

            Assert.AreEqual(expected, result, "Sai rồi");
        }
    }

    [TestClass]
    public class LoginWindow
    {
        [TestMethod]
        public void BtnLogin_Click()
        {
            var email = "abc@gmail.com";
            var password = "@bc123456";

            bool result = AccountHelper.DangNhap(email, password);

            var expected = true;

            Assert.AreEqual(expected, result, "Sai rồi");
        }
    }

    [TestClass]
    public class RegWindow
    {
        [TestMethod]
        public void BtnReg_Click()
        {
            var email = "abc@gmail.com";
            var password = "@bc123456";

            var result = AccountHelper.DangKy(email, password, out KhachHang kh, out string msg);

            var expected = false;

            Assert.AreEqual(expected, result, "Sai rồi");
        }
    }
    }
}
