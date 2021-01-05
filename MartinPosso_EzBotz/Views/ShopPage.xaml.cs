using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class ShopPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Products> colection = new ObservableCollection<Products>();
        public Products producto { get; set; }
        public Users client { get; set; }

        public double price { get; set; }
        public ShopPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                var parameters = (MainPage)e.Parameter;
                if (parameters != null)
                {
                    anadirCarrito(parameters.producto.Id);
                    client = parameters.Users;
                }

            }

        }
        public void anadirCarrito(int Id)
        {
            var listaProductos = Products.GetProducts((App.Current as App).ConnectionString);
            foreach (Products product in listaProductos)
            {
                if (Id == product.Id)
                {
                    producto = product;
                    setPrice(product);
                    colection.Add(product);
                }
            }
            Carrito.ItemsSource = colection;


        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (card.Text != "" && Month.Text != null && Year.Text != null && codigo.Text != "")
            {
                MessageDialog msg = new MessageDialog("Pago exitoso");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Llenar todos los campos para proceder a pagar");
                await msg.ShowAsync();
            }
        }

        private void card_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(count => !char.IsDigit(count));
        }

        private async void Save_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string con = (App.Current as App).ConnectionString;

            if (client != null)
            {
                if (Users.Exists(con, client.Email, client.Password))
                {
                    var people = new People();
                    people.Name = client.Name;
                    people.LastName = client.LastName;
                    people.Address = address.Text;
                    people.Email = client.Email;
                    people.Telefono = phone.Text;
                    people.UserID = client.Id;

                    People.AddData(con, people.Name, people.LastName, people.Address, people.Email, people.Telefono, people.UserID);

                    var persona = new People();

                    foreach (People person in People.GetPeople(con))
                    {
                        if (person.Name.Equals(people.Name))
                            persona = person;
                    }

                    Orders.AddData(con, persona.Id, (decimal)price, client.Id, producto.Id, producto.Price);
                    this.Frame.Navigate(typeof(MainPage));
                }
            }
            else
            {
                MessageDialog msg = new MessageDialog("Para realizar una compra necesita ingresar sesion", "Login!");
                await msg.ShowAsync();
                this.Frame.Navigate(typeof(UserLoginPage));
            }
        }

        private void setPrice(Products p)
        {
            cost.Text = "" + p.Price;
            price = (double)p.Price * 0.12 + (double)p.Price;
            TotalCost.Text = "" + price;
        }

        private void Cancel_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }

}
