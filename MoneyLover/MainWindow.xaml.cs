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
        private string _maKH;

        public MainWindow(string maKH)
        {
            _maKH = maKH;
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
            try
            {
                _context = new MLContext();
                LoadDanhSach();
            }
            catch
            {
                if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Close();
                }
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new AddForm(_maKH);
            addForm.ShowDialog();
            LoadDanhSach();
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            /* TODO: Form sửa sổ chưa tất toán
             *  - Gửi thêm
             *  - Rút một phần
             *  - Tất toán
             */

            LoadDanhSach();
        }

        private void BtnXem_Click(object sender, RoutedEventArgs e)
        {
            string maSo = dgSoDaTatToan.SelectedValue.ToString();
            DetailForm detailForm = new DetailForm(maSo);
            detailForm.ShowDialog();
        }

        private void LoadDanhSach()
        {
            var lstSoDangMo = _context.SoTietKiems.Where(x => x.KhachHang.MaKH == _maKH && x.TinhTrang == "Chưa tất toán").ToList();
            var lstSoDaTatToan = _context.SoTietKiems.Where(x => x.KhachHang.MaKH == _maKH && x.TinhTrang == "Đã tất toán").ToList();
            dgSoDangMo.ItemsSource = null;
            dgSoDangMo.ItemsSource = lstSoDangMo;
            dgSoDangMo.SelectedValuePath = "MaSTK";
            dgSoDaTatToan.ItemsSource = null;
            dgSoDaTatToan.ItemsSource = lstSoDaTatToan;
            dgSoDaTatToan.SelectedValuePath = "MaSTK";
        }
    }
}
