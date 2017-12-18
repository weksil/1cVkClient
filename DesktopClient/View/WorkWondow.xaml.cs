using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        //private AlbumPhoto goodsAlbum;
        private Products products;
        private GroupCustomers customers;
        private OrdersCollection orders;
        private OrderModel curCreatedOrder;

        public WorkWondow(Session session)
        {
            InitializeComponent();
            curentSession = session;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cgSelectPhoto.ItemsSource = (new AlbumPhoto(curentSession.GetAllItems<Album>(Request.comGetPhotos), curentSession.token)).Photos;
            cgSelectPhoto.SelectedIndex = 0;
            Update_Goods();
            Update_Customers();
            Update_Orders();
        }

        private void ClickOnProduct(object sender, MouseButtonEventArgs e)
        {
            grGoodsInfo.DataContext = (sender as Border).DataContext;
        }
        private void ClickOnCustomer(object sender, MouseButtonEventArgs e)
        {
            grCustomerInfo.DataContext = (sender as Border).DataContext;
        }
        private void ClickOnOrder(object sender, MouseButtonEventArgs e)
        {
            grOrderInfo.DataContext = (sender as Border).DataContext;
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
                Update_Goods();
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
        private void Update_Goods(object sender = null, RoutedEventArgs e = null)
        {
            products = new Products(curentSession.GetAllItems<GoodsCollection>(Request.comGetGoods), curentSession.token);
            icGoods.ItemsSource = products.Goods;
            grGoodsInfo.DataContext = null;
        }
        private void Update_Customers(object sender = null, RoutedEventArgs e = null)
        {
            customers = curentSession.GetAllItems<GroupCustomers>(Request.comGetCustomers);
            icCustomers.ItemsSource = customers.customers;
            grCustomerInfo.DataContext = null;
        }
        private void Update_Orders(object sender = null, RoutedEventArgs e = null)
        {
            orders = curentSession.GetAllItems<OrdersCollection>(Request.comGetOrders);
            icOrders.ItemsSource = orders.orders;
            grOrderInfo.DataContext = null;
        }

        private void OrderTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Product tmp;
            //foreach (var item in curCreatedOrder.goods)
            //{
            //    tmp = products.Goods.Where(x => x.id == item.id).FirstOrDefault()?.CurrentProduct;
            //    if (tmp is null)
            //    {
            //        continue;
            //    }
            //    item.title = tmp.title;
            //    item.price = tmp.price;
            //}
            //try
            //{
            //    dgCreateOrder.Dispatcher.BeginInvoke(new Action(() => dgCreateOrder.Items.Refresh()), System.Windows.Threading.DispatcherPriority.Background);
            //}
            //catch (Exception){}
            
            txtCreateOrderTotal.Text = (curCreatedOrder.total).ToString();
        }

        private void AllOrders_View(object sender, RoutedEventArgs e)
        {
            grOrderInfo.Visibility = Visibility.Visible;
            grOrderCreate.Visibility = Visibility.Hidden;
        }
        private void CreateOrder_View(object sender, RoutedEventArgs e)
        {
            grOrderInfo.Visibility = Visibility.Hidden;
            curCreatedOrder = new OrderModel();
            curCreatedOrder.goods.Add(new Product());
            grOrderCreate.DataContext = curCreatedOrder;
            grOrderCreate.Visibility = Visibility.Visible;

        }
        private void RemoveProduct(object sender, RoutedEventArgs e)
        {
            if (grGoodsInfo.DataContext is null) return;
            curentSession.RemoveGoods((grGoodsInfo.DataContext as GoodsModel).id);
            Update_Goods();
        }
        private void AddPositionInOrder(object sender, RoutedEventArgs e)
        {
            curCreatedOrder.goods.Add(new Product());
            dgCreateOrder.Items.Refresh();
        }
    }
}
