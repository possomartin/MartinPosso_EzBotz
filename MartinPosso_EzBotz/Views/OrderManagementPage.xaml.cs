using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MartinPosso_EzBotz.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderManagementPage : Page
    {
        private string connection = (App.Current as App).ConnectionString;
        public OrderManagementPage()
        {
            this.InitializeComponent();

            comboProducts.ItemsSource = Products.GetProducts(connection);
            comboUsers.ItemsSource = Users.GetUsers(connection);
            comboClient.ItemsSource = People.GetPeople(connection);
            UpdateList();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            var Product = (Products)comboProducts.SelectedItem;
            var User = (Users)comboUsers.SelectedItem;
            var Client = (People)comboClient.SelectedItem;

            var Price = (float)Product.Price * 0.12 + (float)Product.Price;

            Orders.AddData(connection, Client.Id, (decimal)Price, User.Id, Product.Id, Product.Price);
            UpdateList();
            unselect();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if(OrderList.SelectedItem != null)
            {
                var Order = (Orders)OrderList.SelectedItem;

                var Product = (Products)comboProducts.SelectedItem;
                var User = (Users)comboUsers.SelectedItem;
                var Client = (People)comboClient.SelectedItem;

                var Price = (float)Product.Price * 0.12 + (float)Product.Price;

                Orders.UpdateData(connection, Client.Id, (decimal)Price, User.Id, Product.Id, Product.Price, Order.Id);

                UpdateList();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {
                var Order = (Orders)OrderList.SelectedItem;
                Orders.Delete(connection, Order.Id);
                unselect();

                UpdateList();
            }
            
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {
                var Order = (Orders)OrderList.SelectedItem;
                Id.Text = "" + Order.Id;
                Price.Text = "" + Order.Price;
                total.Text = "" + Order.Total;
            }

        }

        private void unselect()
        {
            Id.Text = "";
            Price.Text = "";
            total.Text = "";

            comboClient.SelectedItem = null;
            comboProducts.SelectedItem = null;
            comboUsers.SelectedItem = null;

        }

        private void UpdateList()
        {
            OrderList.ItemsSource = Orders.GetOrders(connection);
        }

        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            OrderList.SelectedItem = null;

            unselect();

        }

        private void comboProductsSelection(object sender, SelectionChangedEventArgs e)
        {
            var product = (Products)comboProducts.SelectedItem;

            if (product != null)
            {
                var TotalCost = (float)product.Price * 0.12 + (float)product.Price;

                Price.Text = "" + product.Price;
                total.Text = "" + TotalCost;
            }
        }
    }
}
