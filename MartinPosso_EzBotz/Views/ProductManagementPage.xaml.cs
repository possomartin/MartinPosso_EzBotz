using MartinPosso_EzBotz.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MartinPosso_EzBotz.Views
{
    public sealed partial class ProductManagementPage : Page, INotifyPropertyChanged
    {
        private byte[] buffer;
        private BitmapImage RetrieveImage = new BitmapImage();
        private string path;
        public ProductManagementPage()
        {
            InitializeComponent();
            comboList.ItemsSource = Categories.GetCategories((App.Current as App).ConnectionString);
            SuppliersCombo.ItemsSource = Suppliers.GetSuppliers((App.Current as App).ConnectionString);
            UpdateList();
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

        private void AddProduct(object sender, Windows.UI.Xaml.RoutedEventArgs e) //insertar a la base de datos
        {
            try
            {
                if (Stock.Text.Equals("") || Name.Text.Equals(""))
                {
                    MessageDialog msg = new MessageDialog("Faltan Datos Por Completar");
                    msg.ShowAsync();
                }
                else
                {
                    var category = (Categories)comboList.SelectedItem;
                    var supplier = (Suppliers)SuppliersCombo.SelectedItem;

                    Products.AddData((App.Current as App).ConnectionString, category.Id, Int32.Parse(Stock.Text), Name.Text, Description.Text, supplier.Id, path);
                    UpdateList();

                    EmptyBoxes();

                }
            }
            catch(Exception ex)
            {

            }

        }
        public void UpdateList()
        {
            ProductsList.ItemsSource = Products.GetProducts((App.Current as App).ConnectionString);
        }

        private void EliminarClick(object sender, Windows.UI.Xaml.RoutedEventArgs e) //Eliminar de la base de datos
        {
            var product = (Products)ProductsList.SelectedItem;

            Products.Delete((App.Current as App).ConnectionString, product.Id);
            UpdateList();

            EmptyBoxes();
        }

        private void SelectedItem(object sender, SelectionChangedEventArgs e) //Cuando se selecciona un producto del listView
        {
            var product = (Products)ProductsList.SelectedItem;
            

            if (product != null)
            {
                Id.Text = "" + product.Id;
                Name.Text = "" + product.Name;
                Stock.Text = "" + product.Stock;
                Description.Text = "" + product.Description;
                getImage(product.Image);
            }
          
        }


        private void UpdateClick(object sender, Windows.UI.Xaml.RoutedEventArgs e) //Actualizar informacion a la base de datos
        {
            try
            {
                var category = (Categories)comboList.SelectedItem;
                var supplier = (Suppliers)SuppliersCombo.SelectedItem;

                var product = (Products)ProductsList.SelectedItem;

                Products.UpdateData((App.Current as App).ConnectionString, category.Id, Int32.Parse(Stock.Text), Name.Text, Description.Text, supplier.Id, product.Id, path);
                UpdateList();

                EmptyBoxes();
            }
            catch(Exception ex)
            {

            }

        }

        private async void AddImage(object sender, Windows.UI.Xaml.RoutedEventArgs e) //añadir imagen a la propiedad UWP image
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path);

            if (file != null)
            {
                var imageFile = file;
                using (var imageStream = await imageFile.OpenReadAsync())
                {
                    var image = new BitmapImage();
                    image.DecodePixelWidth = 100;
                    image.DecodePixelWidth = 200;

                    path = folder.Path + "\\" + Name.Text + imageFile.FileType.ToString();

                    

                    if (File.Exists(path))
                    {
                        //If exists deletes the file and replaces it with the new one.
                        File.Delete(path);
                    }
                        //store image into Assets/Photos folder
                    await imageFile.CopyAsync(folder, Name.Text + imageFile.FileType.ToString());

                    MessageDialog msg = new MessageDialog(imageFile.Path);
                    await msg.ShowAsync();

                    // Load the image from the file stream
                    await image.SetSourceAsync(imageStream);
                    productImage.Source = image;
                }

            }
            else
            {
                 Console.WriteLine("Operation cancelled.");
                
            }
        }

        private async void getImage(string sourcePath) //Convertir Bytes de Imagen a BitmapImage
        {
            try
            {
                using (var f = File.Open(sourcePath, FileMode.Open))
                {
                    var image = new BitmapImage();
                    image.DecodePixelWidth = 100;
                    image.DecodePixelWidth = 200;

                    // Load the image from the file stream
                    await image.SetSourceAsync(f.AsRandomAccessStream());
                    productImage.Source = image;
                }
            }
            catch(Exception e)
            {

            }

        }

        private void EmptyBoxes()
        {
            Id.Text = "";
            Name.Text = "";
            Stock.Text = "";
            Description.Text = "";
            productImage.Source = null;
            comboList.SelectedItem = null;
            SuppliersCombo.SelectedItem = null;
        }

        private void Deselect(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            ProductsList.SelectedItem = null;
            EmptyBoxes();
        }
    }
}
