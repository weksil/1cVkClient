using System;
using System.Collections.Generic;
using System.IO;
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
using CoreApi;

namespace DesktopClient
{
    /// <summary>
    /// Логика взаимодействия для WorkWondow.xaml
    /// </summary>
    public partial class WorkWondow : Window
    {
        private Session curentSession;
        private Album goodsAlbum;
        private Products products;
        public WorkWondow(Session session)
        {
            InitializeComponent();
            curentSession = session;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            goodsAlbum = curentSession.GetAllItems<Album>(Request.comGetPhotos);
            products =  new Products( curentSession.GetAllItems<GoodsCollection>(Request.comGetGoods),curentSession.token);
            icGoods.ItemsSource = products.Goods;
        }

        private void ClickOnProduct(object sender, MouseButtonEventArgs e)
        {
            grInfo.DataContext = (sender as Border).DataContext;
        }
    }
}
