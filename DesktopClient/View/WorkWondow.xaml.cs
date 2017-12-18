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
        private AlbumPhoto goodsAlbum;
        private Products products;

        public WorkWondow(Session session)
        {
            InitializeComponent();
            curentSession = session;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            goodsAlbum = new AlbumPhoto( curentSession.GetAllItems<Album>(Request.comGetPhotos), curentSession.token);
            products =  new Products( curentSession.GetAllItems<GoodsCollection>(Request.comGetGoods),curentSession.token);
            icGoods.ItemsSource = products.Goods;
            cgSelectPhoto.ItemsSource = goodsAlbum.Photos;
            cgSelectPhoto.SelectedIndex = 0;
        }

        private void ClickOnProduct(object sender, MouseButtonEventArgs e)
        {
            grInfo.DataContext = (sender as Border).DataContext;
        }

        private void CreateItem_SelectPhoto(object sender, RoutedEventArgs e)
        {
            cgPhoto.DataContext = cgSelectPhoto.SelectedItem;
        }

        private void CreateProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                var price = double.Parse(cgPrice.Text.Replace(".", ","));
                var idPhoto = (cgSelectPhoto.SelectedItem as PhotoUri).Id;
                curentSession.CreateGoods(cgTitle.Text, price, idPhoto);
                cgError.Text = "Создано!";
            }
            catch (Exception)
            {
                cgError.Text = "Неверные данные";
            }
        }

        private void EditInfo(object sender, KeyEventArgs e)
        {
            cgError.Text = "";
        }

        private void All_Goods_View(object sender, RoutedEventArgs e)
        {
            GoodsCreateGrid.Visibility = Visibility.Hidden;
            GoodsInfoGrid.Visibility = Visibility.Visible;
        }
        private void Create_Goods_View(object sender, RoutedEventArgs e)
        {
            GoodsCreateGrid.Visibility = Visibility.Visible;
            GoodsInfoGrid.Visibility = Visibility.Hidden;
        }
        private void Update_Goods(object sender, RoutedEventArgs e)
        {
            products = new Products(curentSession.GetAllItems<GoodsCollection>(Request.comGetGoods), curentSession.token);
            icGoods.ItemsSource = products.Goods;
        }
    }
}
