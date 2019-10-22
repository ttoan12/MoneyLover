using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyLover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MLContext _context;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát ứng dụng ?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context = new MLContext();

            dgSoDangMo.ItemsSource = _context.SoTietKiems.Where(x => x.TinhTrang == "Chưa tất toán").ToList();
            dgSoDaTatToan.ItemsSource = _context.SoTietKiems.Where(x => x.TinhTrang == "Đã tất toán").ToList();
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnXem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
