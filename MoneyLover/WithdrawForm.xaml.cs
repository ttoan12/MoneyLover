using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for WithdrawForm.xaml
    /// </summary>
    public partial class WithdrawForm : Window
    {
        MLContext _context;
        SoTietKiem _stk;
        GetThamSo _thamso;
        string _maSo;

        public WithdrawForm(string maSo)
        {
            InitializeComponent();
            _maSo = maSo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _context = new MLContext();
                _stk = _context.SoTietKiems.First(x => x.MaSTK == _maSo);
                _thamso = new GetThamSo();
            }
            catch
            {
                if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Close();
                }
            }
        }

        private void txtTienRut_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnRut_Click(object sender, RoutedEventArgs e)
        {
            if (_stk.KyHan.ThoiHan != 0)
            {
                string ngayDenHan = DateTime.Parse(_stk.NgayMoSo).AddMonths(_stk.KyHan.ThoiHan).ToShortDateString();
                BeeDialog beeDialog = new BeeDialog(_maSo, ngayDenHan, _stk.LaiSuatKhongKyHan.ToString());
                if (beeDialog.ShowDialog() == true)
                {
                    if (double.TryParse(txtTienRut.Text, out double t))
                    {
                        if (t <= _stk.TongTienGoc)
                        {
                            // Trừ tiền trong tk
                            _stk.TongTienGoc -= t;
                            // Tính lãi
                            var soThangDaGui = (DateTime.Now - DateTime.Parse(_stk.NgayMoSo)).Days / 30;
                            _stk.TongTienLai += t * _stk.LaiSuatKhongKyHan * soThangDaGui / 12;
                            _context.SaveChanges();
                            MessageBox.Show("Rút tiền thành công!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Kiểm tra tiền còn lại
                            if (_stk.TongTienGoc <= 0)
                            {
                                MessageBox.Show("Sổ sẽ tự động tất toán do không còn tiền trong tài khoản!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                _stk.TinhTrang = "Đã tất toán";
                                _context.SaveChanges();
                            }

                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số tiền gửi thêm không hợp lệ!", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            else if (MessageBox.Show("Xác nhận rút tiền?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var soNgayDaGui = (DateTime.Now - DateTime.Parse(_stk.NgayMoSo)).Days;
                if (soNgayDaGui > _thamso.SoNgayGuiToiThieu)
                {
                    if (double.TryParse(txtTienRut.Text, out double t))
                    {
                        if (t <= _stk.TongTienGoc)
                        {
                            // Trừ tiền trong tk
                            _stk.TongTienGoc -= t;
                            // Tính lãi
                            var soThangDaGui = soNgayDaGui / 30;
                            _stk.TongTienLai += t * _stk.LaiSuatKhongKyHan * soThangDaGui / 12;
                            _context.SaveChanges();
                            MessageBox.Show("Rút tiền thành công!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Kiểm tra tiền còn lại
                            if (_stk.TongTienGoc <= 0)
                            {
                                MessageBox.Show("Sổ sẽ tự động tất toán do không còn tiền trong tài khoản!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                _stk.TinhTrang = "Đã tất toán";
                                _context.SaveChanges();
                            }

                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số tiền gửi thêm không hợp lệ!", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Sổ tiết kiệm không kỳ hạn cần mở ít nhất " + _thamso.SoNgayGuiToiThieu + " ngày trước khi rút tiền!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
