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
    public sealed partial class PeopleManagementPage : Page
    {
        private string connection = (App.Current as App).ConnectionString;
        public PeopleManagementPage()
        {
            this.InitializeComponent();
            comboUser.ItemsSource = Users.GetUsers(connection);
            updateList();
        }

        private void AddPeople(object sender, RoutedEventArgs e)
        {
            var user = (Users)comboUser.SelectedItem;

            if (user != null)
            {
                People.AddData(connection, user.Name, user.LastName, Address.Text, user.Email, Telefono.Text, user.Id);
                updateList();
                empty();
            }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var person = (People)PeopleList.SelectedItem;
            var user = (Users)comboUser.SelectedItem;

            if (person != null && user != null)
            {
                People.UpdateData(connection, Name.Text, LastName.Text, Address.Text, Email.Text, Telefono.Text, user.Id, person.Id);
                updateList();
                empty();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            var person = (People)PeopleList.SelectedItem;

            if (person != null)
            {
                People.Delete(connection, person.Id);
                updateList();
                empty();
            }
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var person = (People)PeopleList.SelectedItem;

            if(person != null)
            {
                Id.Text = "" + person.Id;
                Name.Text = person.Name;
                LastName.Text = person.LastName;
                Address.Text = person.Address;
                Email.Text = person.Email;
                Telefono.Text = person.Telefono;

                Email.IsReadOnly = false;
                Name.IsReadOnly = false;
                LastName.IsReadOnly = false;
            }
        }
        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            PeopleList.SelectedItem = null;
            empty();
        }

        private void updateList()
        {
            PeopleList.ItemsSource = People.GetPeople(connection);
        }

        private void empty()
        {
            Id.Text = "";
            Name.Text = "";
            LastName.Text = "";
            Address.Text = "";
            Email.Text = "";
            Telefono.Text = "";
            comboUser.SelectedItem = null;

            Email.IsReadOnly = true;
            Name.IsReadOnly = true;
            LastName.IsReadOnly = true;
        }

        private void comboUserSelection(object sender, SelectionChangedEventArgs e)
        {
            var user = (Users)comboUser.SelectedItem;

            if(user != null)
            {
                Name.Text = user.Name;
                LastName.Text = user.LastName;
                Email.Text = user.Email;
            }
        }
    }
}
