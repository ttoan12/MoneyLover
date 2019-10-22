using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyLover
{
    /// <summary>
    /// Interaction logic for RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        private MLContext _context;

        public RegWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _context = new MLContext();
                _context.KhachHangs.Load();
            }
            catch
            {
                if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Close();
                }
            }

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ShowPassword(object sender, MouseButtonEventArgs e)
        {
            txtPassword_Show.Text = txtPassword.Password;
            txtPassword.Password = "";

            icoHidden.Visibility = Visibility.Hidden;
            icoShowed.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Hidden;
            txtPassword_Show.Visibility = Visibility.Visible;
        }

        private void HidePassword(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Password = txtPassword_Show.Text;
            txtPassword_Show.Text = "";

            icoHidden.Visibility = Visibility.Visible;
            icoShowed.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Visible;
            txtPassword_Show.Visibility = Visibility.Hidden;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            string mail = txtEmail.Text;
            string psw = txtPassword.Password.Length > 0 ? txtPassword.Password : txtPassword_Show.Text;
            if (AccountValidate.IsMail(mail) && AccountValidate.IsPassword(psw))
            {
                var kh = _context.KhachHangs.Local.Where(x => x.Email == mail);
                if (kh.Count() == 0)
                {
                    var listKH = _context.KhachHangs.Local;

                    var lastNumID = 0;
                    if (listKH.Count() > 0)
                    {
                        lastNumID = int.Parse(listKH.OrderByDescending(x => x.MaKH).FirstOrDefault().MaKH.Split('_')[1]);
                    }

                    var makh = "KH_" + (lastNumID + 1).ToString();

                    KhachHang newKH = new KhachHang()
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
                        if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                        {
                            Close();
                        }
                    }

                    MainWindow mainWindow = new MainWindow(makh);
                    mainWindow.Show();
                    Close();
                }
                else MessageBox.Show("Email này đã được sử dụng!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
