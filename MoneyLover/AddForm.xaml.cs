﻿using MoneyLover.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for AddForm.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        MLContext _context;
        KhachHang _khachHang;
        GetThamSo _thamso;
        string _maKH;

        public AddForm(string maKH)
        {
            InitializeComponent();
            _maKH = maKH;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _context = new MLContext();
                _context.NganHangs.Load();
                _context.KyHans.Load();
                _khachHang = _context.KhachHangs.First(x => x.MaKH == _maKH);
                _thamso = new GetThamSo();
            }
            catch
            {
                if (MessageBox.Show("Máy chủ đang được bảo trì!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    Close();
                }
            }

            txtNganHang.ItemsSource = _context.NganHangs.Local.Select(x => x.TenNganHang).ToArray();
            txtNgayGui.SelectedDate = DateTime.Now;
        }

        private void txtSoTienGui_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtLaiSuat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.%]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtLaiSuatKhongKyHan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.%]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtKyHan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtKyHan.SelectedIndex == 0)
                return;

            var nganHangs = _context.NganHangs.Local.Where(x => x.TenNganHang == txtNganHang.Text);
            if (nganHangs.Count() > 0)
            {
                var kyHans = _context.KyHans.Local.Where(x => x.NganHang.MaNganHang == nganHangs.FirstOrDefault().MaNganHang);
                if (kyHans.Count() > 0)
                {
                    int thoiHan = 0;
                    switch (txtKyHan.SelectedIndex)
                    {
                        case 1:
                            thoiHan = 1;
                            break;
                        case 2:
                            thoiHan = 3;
                            break;
                        case 3:
                            thoiHan = 6;
                            break;
                        case 4:
                            thoiHan = 12;
                            break;
                        default:
                            return;
                    }

                    var kyHans1 = kyHans.Where(x => x.ThoiHan == thoiHan);
                    if (kyHans1.Count() > 0)
                    {
                        txtLaiSuat.Text = kyHans1.FirstOrDefault().LaiSuatNam.ToString() + "%";
                    }
                }
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra input
            if (double.TryParse(txtSoTienGui.Text, out double tongTienGoc))
            {
                if (double.Parse(txtSoTienGui.Text) < _thamso.SoTienGuiToiThieu)
                {
                    MessageBox.Show("Số tiền gửi tối thiểu là " + _thamso.SoTienGuiToiThieu, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy nhập số tiền cần gửi!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtNgayGui.SelectedDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày gửi không được sớm hơn thời gian hiện tại!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (double.TryParse(txtLaiSuat.Text.Replace("%", ""), out double laiSuat))
            {
                MessageBox.Show("Hãy nhập lãi suất!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (double.TryParse(txtLaiSuatKhongKyHan.Text.Replace("%", ""), out double laiSuatKhongKH))
            {
                txtLaiSuatKhongKyHan.Text = _thamso.LaiSuatMacDinh.ToString() + "%";
            }
            if (txtTraLai.SelectedIndex == -1)
            {
                txtTraLai.SelectedIndex = 0;
            }
            if (txtKhiDenHan.SelectedIndex == -1)
            {
                txtKhiDenHan.SelectedIndex = 2;
            }

            // Khởi tạo biến
            string maSo = "STK_";
            NganHang nganHang = null;
            string ngayMoSo = txtNgayGui.SelectedDate.ToString();
            KyHan kyHan = null;
            var traLai = txtTraLai.SelectedIndex;
            var denHan = txtKhiDenHan.SelectedIndex;

            // Tạo mã sổ
            var STKs = _context.SoTietKiems.OrderByDescending(x => x.MaSTK);
            if (STKs.Count() > 0)
            {
                maSo += (int.Parse(STKs.First().MaSTK.Split('_')[1]) + 1).ToString();
            }

            // Kiểm tra ngân hàng
            var nganHangs = _context.NganHangs.Where(x => x.TenNganHang == txtNganHang.Text);
            if (nganHangs.Count() > 0)
            {
                // Nếu có
                nganHang = nganHangs.First();
            }
            else
            {
                // Nếu không có
                nganHang = new NganHang()
                {
                    MaNganHang = txtNganHang.Text,
                    TenNganHang = txtNganHang.Text
                };
                _context.NganHangs.Add(nganHang);
                _context.SaveChanges();
            }

            // Kiểm tra kỳ hạn
            var kyHans = _context.KyHans.Where(x => x.NganHang.MaNganHang == nganHang.MaNganHang);
            if (kyHans.Count() > 0)
            {
                // Nếu có
                kyHan = kyHans.First();

                // Kiểm tra lãi suất
                if (kyHan.LaiSuatNam != laiSuat)
                {
                    kyHan.LaiSuatNam = laiSuat;
                    _context.SaveChanges();
                }
            }
            else
            {
                // Nếu chưa có kỳ hạn
                var khIndex = txtKyHan.SelectedIndex;
                kyHan = new KyHan()
                {
                    NganHang = nganHang,
                    MaKyHan = nganHang.MaNganHang + khIndex.ToString(),
                    ThoiHan = khIndex == 1 ? 1 : khIndex == 2 ? 3 : khIndex == 3 ? 6 : khIndex == 4 ? 12 : 0,
                    LaiSuatNam = khIndex == 0 ? laiSuatKhongKH : laiSuat
                };
                _context.KyHans.Add(kyHan);
                _context.SaveChanges();
            }

            // Tạo STK mới
            SoTietKiem newSTK = new SoTietKiem()
            {
                MaSTK = maSo,
                KyHan = kyHan,
                LaiSuatKhongKyHan = laiSuatKhongKH,
                NgayMoSo = ngayMoSo,
                TinhTrang = "Chưa tất toán",
                TongTienGoc = tongTienGoc,
                TongTienLai = 0,
                LoaiTraLai = traLai,
                KhiDenHan = denHan,
                KhachHang = _khachHang
            };
            _context.SoTietKiems.Add(newSTK);
            _context.SaveChanges();

            if (MessageBox.Show("Đã thêm sổ thành công!", "Info", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                Close();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn huỷ thao tác?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }

    }
}
