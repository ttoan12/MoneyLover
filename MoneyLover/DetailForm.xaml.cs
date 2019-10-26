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
    /// Interaction logic for DetailForm.xaml
    /// </summary>
    public partial class DetailForm : Window
    {
        MLContext _context;
        SoTietKiem _stk;
        string _maSo;

        public DetailForm(string maSo)
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

                txtMaSo.Text += _maSo;
                txtNgayMo.Text = _stk.NgayMoSo;
                txtTongTienGui.Text = _stk.TongTienGoc.ToString();
                txtKyHan.Text = _stk.KyHan.ThoiHan == 0 ? "Không kỳ hạn" : _stk.KyHan.ThoiHan.ToString() + " tháng";
                txtLaiSuat.Text = _stk.KyHan.LaiSuatNam.ToString() + "%";
                txtTongTienLai.Text = _stk.TongTienLai.ToString();
                txtSoDu.Text = (_stk.TongTienGoc + _stk.TongTienLai).ToString();
            }
            catch
            {
                if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
