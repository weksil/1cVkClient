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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoreApi;

namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                var session = new Session(txtLogin.Text, txtPass.Password);
                var work = new WorkWondow(session);
                work.Show();
                this.Close();
            }
            catch (Exception)
            {

                txtMess.Text = "Неверные данные";
            }
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtLogin.Text.Length < 6) txtLogin.Background = Brushes.IndianRed;
            else txtLogin.Background = Brushes.White;
            txtMess.Text = "";
        }
    }
}