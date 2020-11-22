using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class ProductManagementPage : Page, INotifyPropertyChanged
    {

        public ProductManagementPage()
        {
            InitializeComponent();
           comboList.ItemsSource = Categories.GetCategories((App.Current as App).ConnectionString);
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

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var category = (Categories)comboList.SelectedItem;

            Products.AddData((App.Current as App).ConnectionString, category.Id, Int32.Parse(Stock.Text), Name.Text, Description.Text, 1);
        }
    }
}
