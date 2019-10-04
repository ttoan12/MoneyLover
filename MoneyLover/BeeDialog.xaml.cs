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
    /// Interaction logic for BeeDialog.xaml
    /// </summary>
    public partial class BeeDialog : Window
    {
        public BeeDialog(string maSo, string ngayHetHan, string tySuat)
        {
            InitializeComponent();
            txtMaSo.Text = maSo;
            txtNgayHetHan.Text = ngayHetHan;
            txtTySuatLai.Text = tySuat;
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
