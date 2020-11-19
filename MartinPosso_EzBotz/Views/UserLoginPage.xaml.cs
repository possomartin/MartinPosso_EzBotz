using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class UserLoginPage : Page, INotifyPropertyChanged
    {
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

        private void SignInBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Users.GetUser((App.Current as App).ConnectionString, EmailTextbox.Text, PasswordTextbox.Password.ToString()))
                this.Frame.Navigate(typeof(MainPage));
        }

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserRegistrationPage));
        }
    }
}
