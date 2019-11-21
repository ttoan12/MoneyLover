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
            var password = "123";

            bool result = AccountHelper.DangNhap(email, password, out string msg);

            var expected = false;

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

    [TestClass]
    public class InputHelper
    {
        [TestMethod]
        public void SoTienGui()
        {
            // Min: 1000000
            string tienGui = "1500000";
            bool result = MoneyLover.InputHelper.SoTienGui(tienGui, out double soTienGui, out string msg);
            bool expected = true;

            Assert.AreEqual(expected, result, "Sai rồi TT_TT");
        }

        [TestMethod]
        public void NgayGui()
        {
            DateTime ngayGui = DateTime.Now;
            bool result = MoneyLover.InputHelper.NgayGui(ngayGui, out string msg);
            bool expected = true;

            Assert.AreEqual(expected, result, "Sai rồi TT_TT");
        }

        [TestMethod]
        public void LaiSuat()
        {
            string laiSuat = "0.7";
            bool result = MoneyLover.InputHelper.LaiSuat(laiSuat, out double soTienGui, out string msg);
            bool expected = true;

            Assert.AreEqual(expected, result, "Sai rồi TT_TT");
        }

        [TestMethod]
        public void IsDenHan()
        {
            DateTime ngayDenHan = DateTime.Now;
            var result = MoneyLover.InputHelper.IsDenHan(ngayDenHan);

            var expected = true;

            Assert.AreEqual(expected, result, "Sai rồi");
        }
    }
}
