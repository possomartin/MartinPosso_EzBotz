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
            SupplierList.ItemsSource = Supplier.GetSuppliers(connection);
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            Supplier.AddData(connection, Name.Text, Address.Text, Email.Text);
            updateList();
            Empty();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var supplier = (Supplier)SupplierList.SelectedItem;

            if(supplier != null)
            {
                Supplier.UpdateData(connection, Name.Text, Address.Text, Email.Text, supplier.SupplierID);
                updateList();
                Empty();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            var supplier = (Supplier)SupplierList.SelectedItem;

            if (supplier != null)
            {
                Supplier.Delete(connection, supplier.SupplierID);
                updateList();
                Empty();
            }
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var supplier = (Supplier)SupplierList.SelectedItem;

            if (supplier != null)
            {
                Id.Text = "" + supplier.SupplierID;
                Name.Text = supplier.SupplierName;
                Address.Text = supplier.Address;
                Email.Text = supplier.Email;
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
            Email.Text = "";
        }
    }
}
