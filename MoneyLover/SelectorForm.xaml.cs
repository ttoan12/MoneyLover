using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for SelectorForm.xaml
    /// </summary>
    public partial class SelectorForm : Window
    {
        string _maSo;
        MLContext _context;
        SoTietKiem _stk;

        public SelectorForm(string maSo)
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
            }
            catch
            {
                if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            EditForm editForm = new EditForm(_maSo);
            editForm.ShowDialog();
            Close();
        }

        private void btnGuiThem_Click(object sender, RoutedEventArgs e)
        {
            AddFundForm addFundForm = new AddFundForm(_maSo);
            addFundForm.ShowDialog();
            Close();
        }

        private void btnRutMotPhan_Click(object sender, RoutedEventArgs e)
        {
            // Form rút một phần
        }

        private void btnTatToan_Click(object sender, RoutedEventArgs e)
        {
            DateTime ngayDenHan = DateTime.Parse(_stk.NgayMoSo).AddMonths(_stk.KyHan.ThoiHan);

            if (_stk.KhiDenHan == 2)
            {
                // Sổ chọn tất toán khi đến hạn
                if ((DateTime.Now - ngayDenHan).Days > 0)
                {
                    // Đã đến hạn
                    if (MessageBox.Show("Sổ đã đến hạn tất toán, bạn có muốn tiếp tục?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        var soDu = TatToan(true);
                        MessageBox.Show("Tất toán thành công, tổng số dư của bạn là: " + soDu + " VND", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
                else
                {
                    // Chưa đến hạn
                    if (MessageBox.Show("Sổ tiết kiệm này chưa đến hạn, nếu tất toán ngay bây giờ sẽ tính lãi theo lãi suất không thời hạn.\nBạn có muốn tiếp tục không?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var soDu = TatToan(false);
                        MessageBox.Show("Tất toán thành công, tổng số dư của bạn là: " + soDu + " VND", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                }
            }
            else
            {
                // Không chọn tất toán khi đến hạn
                MessageBox.Show("Vui lòng thay đổi thông tin sổ thành 'Tất toán khi đến hạn'!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private double TatToan(bool denHan)
        {
            if (denHan)
            {
                _stk.TongTienLai += (_stk.TongTienGoc * _stk.KyHan.LaiSuatNam * _stk.KyHan.ThoiHan / 12);
            }
            else
            {
                int soThangDaGui = (DateTime.Now - DateTime.Parse(_stk.NgayMoSo)).Days / 30;
                _stk.TongTienLai += (_stk.TongTienGoc * _stk.LaiSuatKhongKyHan * soThangDaGui / 12);
            }
            _stk.TinhTrang = "Đã tất toán";
            _context.SaveChanges();
            return _stk.TongTienGoc + _stk.TongTienLai + _stk.TienGuiThem;
        }
    }
}
