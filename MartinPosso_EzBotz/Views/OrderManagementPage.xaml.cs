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

            comboProducts.ItemsSource = Product.GetProducts(connection);
            comboClient.ItemsSource = Person.GetPeople(connection);
            UpdateList();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            var product = (Product)comboProducts.SelectedItem;
            var Client = (Person)comboClient.SelectedItem;

            //var Price = (float)Product.Price * 0.12 + (float)Product.Price;

            Order.AddData(connection, product.ProductID, Client.PersonID, 21, (int)quantity.Value);

            int stock = product.Stock - (int)quantity.Value;

            Product.UpdateData(connection, product.ProductName, product.Description, stock, product.ProductCode, product.SupplierID, product.CategoryID, product.Image, product.ProductID);
            UpdateList();
            unselect();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if(OrderList.SelectedItem != null)
            {
                var Order = (Order)OrderList.SelectedItem;

                var Product = (Product)comboProducts.SelectedItem;
                var Client = (Person)comboClient.SelectedItem;

                //var Price = (float)Product.Price * 0.12 + (float)Product.Price;

                Core.Models.Order.UpdateData(connection, Product.ProductID, Client.PersonID, 21, (int)quantity.Value, Order.OrderID);

                UpdateList();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {
                var Order = (Order)OrderList.SelectedItem;
                Order.Delete(connection, Order.OrderID);
                unselect();

                UpdateList();
            }
            
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {
                var Order = (Order)OrderList.SelectedItem;
                Id.Text = "" + Order.OrderID;
                Price.Text = "" + Order.Price;
                quantity.Value = Order.Quantity;
            }

        }

        private void unselect()
        {
            Id.Text = "";
            Price.Text = "";
            quantity.Value = 0;

            comboClient.SelectedItem = null;
            comboProducts.SelectedItem = null;
           
        }

        private void UpdateList()
        {
            OrderList.ItemsSource = Order.GetOrders(connection);
        }

        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            OrderList.SelectedItem = null;

            unselect();

        }

        private void comboProductsSelection(object sender, SelectionChangedEventArgs e)
        {
            var product = (Product)comboProducts.SelectedItem;

            if (product != null)
            {
                quantity.Maximum = product.Stock;
            }
        }
    }
}
