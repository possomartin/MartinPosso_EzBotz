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
    public sealed partial class CategoriesManagementPage : Page
    {
        private string connection = (App.Current as App).ConnectionString;
        public CategoriesManagementPage()
        {
            this.InitializeComponent();
            updateList();
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var category = (Category)CategoryList.SelectedItem;

            if(category != null)
            {
                Id.Text = "" + category.CategoryID;
                Name.Text = category.CategoryName;
                Description.Text = category.description;
            }
        }

        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            CategoryList.SelectedItem = null;
            Empty();
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            Category.AddData(connection, Name.Text, Description.Text);

            updateList();
            Empty();
            
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var category = (Category)CategoryList.SelectedItem;

            if (category != null)
            {
                Category.UpdateData(connection, Name.Text, Description.Text, category.CategoryID);
                updateList();
                Empty();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            var category = (Category)CategoryList.SelectedItem;

            if (category != null)
            {
                Category.Delete(connection, category.CategoryID);

                updateList();
                Empty();
            }
        }

        private void updateList()
        {
            CategoryList.ItemsSource = Category.GetCategories(connection);
        }

        private void Empty()
        {
            Id.Text = "";
            Name.Text = "";
            Description.Text = "";
        }
    }
}
