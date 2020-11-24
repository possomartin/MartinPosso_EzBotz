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
        public ProductManagementPage()
        {
            InitializeComponent();
           comboList.ItemsSource = Categories.GetCategories((App.Current as App).ConnectionString);
           SuppliersCombo.ItemsSource = Suppliers.GetSuppliers((App.Current as App).ConnectionString);
            updateList();
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

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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

                Products.AddData((App.Current as App).ConnectionString, category.Id, Int32.Parse(Stock.Text), Name.Text, Description.Text, supplier.Id, buffer);
                updateList();

                emptyBoxes();
                
            }

        }
        public void updateList()
        {
            ProductsList.ItemsSource = Products.GetProducts((App.Current as App).ConnectionString);
        }

        private void EliminarClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var product = (Products)ProductsList.SelectedItem;

            Products.delete((App.Current as App).ConnectionString, product.Id);
            updateList();

            emptyBoxes();
        }

        private void selctedItem(object sender, SelectionChangedEventArgs e)
        {
            var product = (Products)ProductsList.SelectedItem;

            if (product != null)
            {
                Id.Text = "" + product.Id;
                Name.Text = "" + product.Name;
                Stock.Text = "" + product.Stock;
                Description.Text = "" + product.Description;
            }
          
        }

        private void UpdateClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var category = (Categories)comboList.SelectedItem;
            var supplier = (Suppliers)SuppliersCombo.SelectedItem;

            var product = (Products)ProductsList.SelectedItem;

            Products.UpdateData((App.Current as App).ConnectionString, category.Id, Int32.Parse(Stock.Text), Name.Text, Description.Text, supplier.Id, product.Id);
            updateList();

            emptyBoxes();

        }

        private async void AddImage(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var imageFile = file;
                using (var imageStream = await imageFile.OpenReadAsync())
                {
                    var image = new BitmapImage();
                    image.DecodePixelWidth = 100;

                    // Load the image from the file stream
                    await image.SetSourceAsync(imageStream);
                    productImage.Source = image;
                }

                using (var inputStream = await file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();
                    buffer = new byte[readStream.Length];
                    await readStream.ReadAsync(buffer, 0, buffer.Length);
                    
                }

            }
            else
            {
                 Console.WriteLine("Operation cancelled.");
                
            }
        }

        private async void ConvertBytesAsync(byte[] bytes)
        {
            BitmapImage bmi = new BitmapImage();
            int width = 50, height = 50;
            byte[] buffer = bytes;

            WriteableBitmap wb = new WriteableBitmap(50, 50);
            using (Stream stream = wb.PixelBuffer.AsStream())
            {
                if (stream.CanWrite)
                {
                    byte[] pixelArray = new byte[(uint)productImage.Height * (uint)productImage.Width * 4];
                    int offset;

                    for (int row = 0; row < (uint)productImage.Height; row++)
                    {
                        for (int col = 0; col < (uint)productImage.Width; col++)
                        {
                            offset = (row * (int)productImage.Width * 4) + (col * 4);
                            pixelArray[offset] = 0x00;      // Red
                            pixelArray[offset + 1] = 0xFF;  // Green
                            pixelArray[offset + 2] = 0x00;  // Blue
                            pixelArray[offset + 3] = 0xFF;  // Alpha
                        }
                    }
                    await stream.WriteAsync(pixelArray, 0, pixelArray.Length);
                    stream.Flush();
                    //bmi = await ByteArrayToImageAsync(buffer);
                    productImage.Source = wb;
                }
            }
        }

        private void emptyBoxes()
        {
            Id.Text = "";
            Name.Text = "";
            Stock.Text = "";
            Description.Text = "";
            productImage.Source = null;
        }
    }
}
