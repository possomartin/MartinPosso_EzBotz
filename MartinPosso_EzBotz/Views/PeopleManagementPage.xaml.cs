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
            comboUser.ItemsSource = User.GetUsers(connection);
            updateList();
        }

        private void AddPeople(object sender, RoutedEventArgs e)
        {
            var user = (User)comboUser.SelectedItem;

            if (user != null)
            {
                Person.AddData(connection, Name.Text, LastName.Text, Address.Text, Telefono.Text, user.UserID);
                updateList();
                empty();
            }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            var person = (Person)PeopleList.SelectedItem;
            var user = (User)comboUser.SelectedItem;

            if (person != null && user != null)
            {
                Person.UpdateData(connection, Name.Text, LastName.Text, Address.Text, Telefono.Text, user.UserID, person.PersonID);
                updateList();
                empty();
            }
        }

        private void EliminarClick(object sender, RoutedEventArgs e)
        {
            var person = (Person)PeopleList.SelectedItem;

            if (person != null)
            {
                Person.Delete(connection, person.PersonID);
                updateList();
                empty();
            }
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            var person = (Person)PeopleList.SelectedItem;

            if(person != null)
            {
                Id.Text = "" + person.PersonID;
                Name.Text = person.PersonName;
                LastName.Text = person.LastName;
                Address.Text = person.Address;
                Telefono.Text = person.PhoneNumber;

                Email.IsReadOnly = false;
            }
        }
        private void Deselect(object sender, DoubleTappedRoutedEventArgs e)
        {
            PeopleList.SelectedItem = null;
            empty();
        }

        private void updateList()
        {
            PeopleList.ItemsSource = Person.GetPeople(connection);
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
        }

        private void comboUserSelection(object sender, SelectionChangedEventArgs e)
        {
            var user = (User)comboUser.SelectedItem;

            if(user != null)
            {
                Email.Text = user.Email;
            }
        }
    }
}
