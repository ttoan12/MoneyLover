using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
            SlideHandler();
        }



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }



        private void SlideHandler()
        {
            Timer tmr = new Timer();
            tmr.Interval = 5000;
            tmr.Elapsed += (sender, e) => {
                Dispatcher.Invoke(() =>
                {
                    // Check indicator
                    if (slideIntro1.IsEnabled)
                    {
                        slideIntro1.IsEnabled = false;
                        slideIntro2.IsEnabled = true;
                    }
                    else if (slideIntro2.IsEnabled)
                    {
                        slideIntro2.IsEnabled = false;
                        slideIntro3.IsEnabled = true;
                    }
                    else if (slideIntro3.IsEnabled)
                    {
                        slideIntro3.IsEnabled = false;
                        slideIntro4.IsEnabled = true;
                    }
                    else if (slideIntro4.IsEnabled)
                    {
                        slideIntro4.IsEnabled = false;
                        slideIntro5.IsEnabled = true;
                    }
                    else if (slideIntro5.IsEnabled)
                    {
                        slideIntro5.IsEnabled = false;
                        slideIntro1.IsEnabled = true;
                    }
                });
                // Fly Out
                Animation.FlyOut(picIntro, lblIntro);

                // Wait for animation
                Timer tmr2 = new Timer();
                tmr2.Interval = 200;
                tmr2.AutoReset = false;
                tmr2.Elapsed += (Object, ElapsedEventArgs) => {
                    Dispatcher.Invoke(() => { 
                        // Make Changes
                        picIntro.Source = new BitmapImage(new Uri(@"/images/Intro.png", UriKind.Relative));
                        lblIntro_Content.Text = "Lên kế hoạch tài chính thông minh và từng bước tiết kiệm để hiện thực hóa ước mơ.";

                        // Fly In
                        Animation.FlyIn(picIntro, lblIntro);
                    });
                };
                tmr2.Start();

            };
            tmr.Start();
        }

        public static class Animation
        {
            public static void FlyOut(Image picIntro, Label lblIntro)
            {
                // Move to middle
                Application.Current.Dispatcher.Invoke(() => {
                    Thickness picIntroMargin = picIntro.Margin;
                    Thickness lblIntroMargin = lblIntro.Margin;
                    picIntroMargin.Left = 0;
                    lblIntroMargin.Left = 0;
                    picIntroMargin.Right = 0;
                    lblIntroMargin.Right = 0;

                    // Apply edit
                    picIntro.Margin = picIntroMargin;
                    lblIntro.Margin = lblIntroMargin;
                });

                Timer tmr = new Timer();
                tmr.Interval = 10;
                tmr.Elapsed += (sender, e) => {
                    Application.Current.Dispatcher.Invoke(() => {
                        if (picIntro.Margin.Left <= -600 && lblIntro.Margin.Left <= -600)
                        {
                            tmr.Stop();
                        }
                        else
                        {
                            // Edit value
                            Thickness picIntroMargin = picIntro.Margin;
                            Thickness lblIntroMargin = lblIntro.Margin;
                            picIntroMargin.Left -= 60;
                            lblIntroMargin.Left -= 60;

                            // Apply edit
                            picIntro.Margin = picIntroMargin;
                            lblIntro.Margin = lblIntroMargin;
                        }
                    });
                };
                tmr.Start();
            }

            public static void FlyIn(Image picIntro, Label lblIntro)
            {
                // Move to right side
                Application.Current.Dispatcher.Invoke(() => {
                    Thickness picIntroMargin = picIntro.Margin;
                    Thickness lblIntroMargin = lblIntro.Margin;
                    picIntroMargin.Left = 0;
                    lblIntroMargin.Left = 0;
                    picIntroMargin.Right = -600;
                    lblIntroMargin.Right = -600;

                    // Apply edit
                    picIntro.Margin = picIntroMargin;
                    lblIntro.Margin = lblIntroMargin;
                });

                Timer tmr = new Timer();
                tmr.Interval = 10;
                tmr.Elapsed += (sender, e) => {
                    Application.Current.Dispatcher.Invoke(() => {
                        if (picIntro.Margin.Right >= 0 && lblIntro.Margin.Right >= 0)
                        {
                            tmr.Stop();
                        }
                        else
                        {
                            // Edit value
                            Thickness picIntroMargin = picIntro.Margin;
                            Thickness lblIntroMargin = lblIntro.Margin;
                            picIntroMargin.Right += 60;
                            lblIntroMargin.Right += 60;

                            // Apply edit
                            picIntro.Margin = picIntroMargin;
                            lblIntro.Margin = lblIntroMargin;
                        }
                    });
                };
                tmr.Start();
            }
        }

    }
}
