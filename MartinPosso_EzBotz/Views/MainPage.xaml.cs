using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
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
            var list = Products.GetProducts((App.Current as App).ConnectionString);

            if (searchBar.Text == "")
            {
                ProductGallery.ItemsSource = list;
            }
            else
            {
                ObservableCollection<Products> listaProductos = new ObservableCollection<Products>();

                foreach(Products products in list)
                {
                   if(products.Name.ToLower().StartsWith(searchBar.Text.ToLower()))
                   {
                        listaProductos.Add(products);
                   }
                }

                ProductGallery.ItemsSource = listaProductos;

            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void searchButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SearchItems();
        }

        private void Featured(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProductGallery.ItemsSource = Products.OrderBy((App.Current as App).ConnectionString, "ASC");
        }

        private void Now(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProductGallery.ItemsSource = Products.OrderBy((App.Current as App).ConnectionString, "DESC");
        }

        private void BestSellings(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ProductGallery.ItemsSource = Products.OrderBy((App.Current as App).ConnectionString, "ASC");
        }
    }
}
