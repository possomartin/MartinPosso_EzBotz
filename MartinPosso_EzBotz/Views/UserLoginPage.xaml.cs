using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class UserLoginPage : Page, INotifyPropertyChanged
    {
        private string con = (App.Current as App).ConnectionString;
        private User userLogin { get; set; }
        public UserLoginPage()
        {
            InitializeComponent();
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

        private async void SignInBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            if (UserExist(EmailTextbox.Text, PasswordTextbox.Password.ToString()))
            {
                this.Frame.Navigate(typeof(MainPage), EmailTextbox.Text);
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Los datos ingresados no existen, Intente nuevamente!", "Error");
                await dialog.ShowAsync();
                this.Frame.Navigate(typeof(UserLoginPage));
            }

        }

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserRegistrationPage));
        }


        private Boolean UserExist(string email, string password)
        {
            var Users = User.GetUsers(con);

            bool exists = false;

            foreach(User user in Users)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password))
                    exists = true;
            }

            return exists;
        }
    }
}
