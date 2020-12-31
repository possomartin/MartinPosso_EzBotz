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
    public sealed partial class SupplierManagementPage : Page
    {
        private string connection = (App.Current as App).ConnectionString;
        public SupplierManagementPage()
        {
            this.InitializeComponent();
            updateList();
        }

        private void updateList()
        {
            SupplierList.ItemsSource = Suppliers.GetSuppliers(connection);
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            Suppliers.AddData(connection, Name.Text, Address.Text);
            updateList();
            Empty();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var supplier = (Suppliers)SupplierList.SelectedItem;

            if(supplier != null)
            {
                Suppliers.UpdateData(connection, Name.Text, Address.Text, supplier.Id);
                updateList();
                Empty();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            var supplier = (Suppliers)SupplierList.SelectedItem;

            if (supplier != null)
            {
                Suppliers.Delete(connection, supplier.Id);
                updateList();
                Empty();
            }
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var supplier = (Suppliers)SupplierList.SelectedItem;

            if (supplier != null)
            {
                Id.Text = "" + supplier.Id;
                Name.Text = supplier.Name;
                Address.Text = supplier.Address;
            }
        }
        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            SupplierList.SelectedItem = null;
            Empty();
        }

        private void Empty()
        {
            Id.Text = "";
            Name.Text = "";
            Address.Text = "";
        }
    }
}
