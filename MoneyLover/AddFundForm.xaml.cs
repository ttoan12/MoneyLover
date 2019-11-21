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
    /// Interaction logic for AddFundForm.xaml
    /// </summary>
    public partial class AddFundForm : Window
    {
        MLContext _context;
        SoTietKiem _stk;
        GetThamSo _thamso;
        string _maSo;

        public AddFundForm(string maSo)
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

        private void txtTienGuiThem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnGui_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Số tiền gửi thêm sẽ bắt đầu tính lãi sau khi đến hạn.\nXác nhận gửi thêm?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string msg = "";
                if (InputHelper.SoTienGui(txtTienGuiThem.Text, out double t, out msg))
                {
                    _stk.TienGuiThem += t;
                    _context.SaveChanges();
                    MessageBox.Show("Gửi thêm thành công", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(msg, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
