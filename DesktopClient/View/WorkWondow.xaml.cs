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
        private StatusCollection statuses;

        public WorkWondow(Session session)
        {
            InitializeComponent();
            curentSession = session;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cgSelectPhoto.ItemsSource = (new AlbumPhoto(curentSession.GetAllItems<Album>(Request.comGetPhotos), curentSession.token)).Photos;
            cgSelectPhoto.SelectedIndex = 0;
            statuses = curentSession.GetAllItems<StatusCollection>(Request.comGetStatuses);
            cmbOrderStatus.ItemsSource = statuses.statuses;
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
            cmbOrderStatus.SelectedIndex = (grOrderInfo.DataContext as Order).status.id - 1;
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
            cbxGoods.ItemsSource = products.Goods;
            grGoodsInfo.DataContext = null;
        }
        private void Update_Customers(object sender = null, RoutedEventArgs e = null)
        {
            customers = curentSession.GetAllItems<GroupCustomers>(Request.comGetCustomers);
            icCustomers.ItemsSource = customers.customers;
            cbxCustomers.ItemsSource = customers.customers;
            grCustomerInfo.DataContext = null;
        }
        private void Update_Orders(object sender = null, RoutedEventArgs e = null)
        {
            orders = curentSession.GetAllItems<OrdersCollection>(Request.comGetOrders);
            icOrders.ItemsSource = orders.orders;
            grOrderInfo.DataContext = null;
            cmbOrderStatus.SelectedIndex = -1;
        }
        private void AllOrders_View(object sender, RoutedEventArgs e)
        {
            grOrderInfo.Visibility = Visibility.Visible;
            grOrderCreate.Visibility = Visibility.Hidden;
        }
        private void CreateOrder_View(object sender, RoutedEventArgs e)
        {
            grOrderInfo.Visibility = Visibility.Hidden;
            txtErrorCreateOrder.Text = "";
            curCreatedOrder = new OrderModel();
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
            try
            {
                var quantity = int.Parse(txtGoodsQuantity.Text);
                var vendor = (cbxGoods.SelectedItem as GoodsModel).vendor_code;
                var product = products.Goods.Where(x => x.vendor_code == vendor).First().CurrentProduct;
                if (product.quantity + quantity < 0) quantity = -product.quantity;
                curCreatedOrder.Add(product, quantity);
                curCreatedOrder.Fire();
                dgCreateOrder.Items.Refresh();
            }
            catch (Exception)
            {
            }
            
        }
        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxCustomers.SelectedIndex == -1) throw new Exception();
                if (curCreatedOrder.goods.Count < 0) throw new Exception();
                var res = curCreatedOrder.CreateJson(cbxCustomers.SelectedItem as Customer);
                curentSession.CreateOrder(res);
                Update_Goods();
                Update_Orders();
                txtErrorCreateOrder.Text = "Успешно";
                dgCreateOrder.Items.Refresh();
            }
            catch (Exception error)
            {
                if (error is StockExeption)
                {
                    txtErrorCreateOrder.Text = "Недостаточно золота";
                }
                else
                    txtErrorCreateOrder.Text = "Ошибка при создании";
            }
        }
        private void UpdateOrder(object sender, RoutedEventArgs e)
        {
            var order = grOrderInfo.DataContext as Order;
            if (order is null) return;
            var status = cmbOrderStatus.SelectedItem as Status;
            try
            {
                curentSession.UpdateOrder(order,status);

            }
            catch (Exception)
            {
                MessageBox.Show("Неправильный статус");
            }
            Update_Orders();
        }
        private void AddStockGoods(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = grGoodsInfo.DataContext as GoodsModel;
                if (product is null) return;
                var add = int.Parse(txtNewStock.Text);
                curentSession.AddStokGoods(product.CurrentProduct, add);
                product.Fire() ;
            }
            catch (Exception)
            {
            }
            Update_Orders();
        }
        //private void Cmb_KeyUp(object sender, KeyEventArgs e)
        //{
        //    var source = sender as ComboBox;
        //    CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(source.ItemsSource);

        //    itemsViewOriginal.Filter = ((o) =>
        //    {
        //        if (String.IsNullOrEmpty(source.Text)) return true;
        //        else
        //        {
        //            if (o.ToString().Contains(source.Text)) return true;
        //            else return false;
        //        }
        //    });

        //    itemsViewOriginal.Refresh();
        //}
    }
}
