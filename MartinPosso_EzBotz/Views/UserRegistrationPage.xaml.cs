using MartinPosso_EzBotz.Core.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class UserRegistrationPage : Page, INotifyPropertyChanged
    {
        public UserRegistrationPage()
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

        private async void SignUpBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Users.AddData((App.Current as App).ConnectionString, NameTextbox.Text, LastNameTextbox.Text, EmailTextbox.Text, PasswordTextbox.Password.ToString());
            MessageDialog msg = new MessageDialog("Se ha registrado existosamente", "Registro exitoso!");
            await msg.ShowAsync();
            this.Frame.Navigate(typeof(UserLoginPage));
        }
    }
}
