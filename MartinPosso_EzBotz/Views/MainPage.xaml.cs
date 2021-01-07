using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public User Users { get; set; }
        public Product producto { get; set; }
        public MainPage()
        {
            InitializeComponent();
            SearchItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void SearchItems()
        {
            var list = Product.GetProducts((App.Current as App).ConnectionString);

            if (searchBar.Text == "")
            {
                ProductGallery.ItemsSource = list;
            }
            else
            {
                ObservableCollection<Product> listaProductos = new ObservableCollection<Product>();

                foreach(Product products in list)
                {
                   if(products.ProductName.ToLower().StartsWith(searchBar.Text.ToLower()))
                   {
                        listaProductos.Add(products);
                   }
                }

                ProductGallery.ItemsSource = listaProductos;

            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (e.Parameter != null)
                {
                    var parameters = (string)e.Parameter;
                    if (parameters != "")
                    {
                        foreach (User user in User.GetUsers((App.Current as App).ConnectionString))
                        {
                            if (user.Email.Equals(parameters))
                            {
                                Users = user;
                            }
                        }

                        MessageDialog msg = new MessageDialog(Users.Email + " ID: " + Users.UserID);
                        await msg.ShowAsync();
                    }
                }
                base.OnNavigatedTo(e);
            }
            catch(Exception ex)
            {

            }


        }

        private void searchButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SearchItems();
        }

        private void Featured(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProductGallery.ItemsSource = Product.OrderBy((App.Current as App).ConnectionString, "ASC");
        }

        private void Now(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProductGallery.ItemsSource = Product.OrderBy((App.Current as App).ConnectionString, "DESC");
        }

        private void BestSellings(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProductGallery.ItemsSource = Product.OrderBy((App.Current as App).ConnectionString, "ASC");
        }

        private void addcart(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var product = (Product)ProductGallery.SelectedItem;
            
            if (product != null)
            {
                SendData(product);
            }
        }

        private void SendData(Product product)
        {
            var parameters = new MainPage();
            parameters.producto = product;
            parameters.Users = Users;

            this.Frame.Navigate(typeof(ShopPage), parameters);
        }

    }
}
