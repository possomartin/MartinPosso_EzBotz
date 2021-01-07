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
    public sealed partial class UserManagementPage : Page
    {
        private string connection = (App.Current as App).ConnectionString;
        public UserManagementPage()
        {
            this.InitializeComponent();
            updateList();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            User.AddData(connection, Name.Text, Email.Text, Password.Text);
            updateList();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var user = (User)UserList.SelectedItem;

            if (user != null)
            {
                User.UpdateData(connection, Name.Text, Email.Text, Password.Text, user.UserID);
                updateList();
                Empty();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            var user = (User)UserList.SelectedItem;

            if(user != null)
            {
                User.Delete(connection, user.UserID);
                updateList();
                Empty();
            }
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var user = (User)UserList.SelectedItem;

            if(user != null)
            {
                Id.Text = "" + user.UserID;
                Name.Text = user.UserName;
                Email.Text = user.Email;
                Password.Text = user.Password;
            }
        }

        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            UserList.SelectedItem = null;

            Empty();
        }

        private void updateList()
        {
            UserList.ItemsSource = User.GetUsers(connection);
        }

        private void Empty()
        {
            Id.Text = "";
            Name.Text = "";
            Email.Text = "";
            Password.Text = "";
        }


    }
}
