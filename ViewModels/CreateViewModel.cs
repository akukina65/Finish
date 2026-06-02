
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoText12.ViewModels
{
    public partial class CreateViewModel:ViewModelBase
    {
        [ObservableProperty]
        public Product products;

        [ObservableProperty]
        public Bitmap image;

        [ObservableProperty] public List<ProductCategory> categories = db.ProductCategories.ToList();
        [ObservableProperty] public List<ProductName> productNames = db.ProductNames.ToList();
        [ObservableProperty] public List<Supplier> suppliers = db.Suppliers.ToList();
        [ObservableProperty] public List<Maker> makers = db.Makers.ToList();
        public CreateViewModel(Product product)
        {
            Products = product;
            Image = new Bitmap("Assets/picture.png");
            ImageLoad();
            Products.Discount = 0;
        }

        public void ImageLoad()
        {
            var path = Path.Combine("Assets", Products.Image?? "picture.png");
            if (File.Exists(path))

            {
                Image = new Bitmap(path);
            }
            else 
            {

                Image = new Bitmap("Assets/picture.png");
            }
        }

        public async void SelectImage()
        {
            var files = await TopLevel.GetTopLevel(MainWindowViewModel.MainWindow)?.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions { FileTypeFilter = new[] { new FilePickerFileType("Images") { Patterns = new[] { "*.jpg", "*.png" } } } });
            if(files?.Count>0 && files[0]!=null)
            {
                if(Products.Image!="picture.png" && !string.IsNullOrEmpty(Products.Image))
                {
                    var old = Path.Combine("Assets", Products.Image);
                    if(File.Exists(old))
                    {
                        File.Delete(old);
                    }
                }


                using var sream = File.OpenRead(files[0].Path.LocalPath);
                var img = new Bitmap(sream);
                var reliz = img.CreateScaledBitmap(new Avalonia.PixelSize(300, 200));

                var path = Path.Combine("Assets", $"{Guid.NewGuid()}.jpg");
                Directory.CreateDirectory("Assets");
                reliz.Save(path);
                Image = new Bitmap(path);
                Products.Image = Path.GetFileName(path);

            }


        }

        public async void Save()
        {
            // Проверка производителя
            if (Products.IdMakerNavigation == null)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите производителя", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }

            // Проверка категории
            if (Products.IdProductCategoryNavigation == null)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите категорию", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }

            // Проверка наименования
            if (Products.IdProductNameNavigation == null)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите наименование", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }

            // Проверка поставщика
            if (Products.IdSupplierNavigation == null)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите поставщика", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }

            // Проверка цены
            if (Products.Price <= 0)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Цена должна быть больше 0", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }

            // Проверка количества
            if (Products.CountOnStock < 0)
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Количество не может быть отрицательным", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }



            // Проверка описания
            if (string.IsNullOrWhiteSpace(Products.Discription))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Заполните описание", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }



            // Проверка единицы измерения
            if (string.IsNullOrWhiteSpace(Products.Unit))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Заполните единицу измерения", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }

            // Проверка артикула
            if (string.IsNullOrWhiteSpace(Products.Article))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Заполните артикул", ButtonEnum.Ok, Icon.Error).ShowAsync();
                return;
            }
            Products.Id = 0;
            db.Products.Add(Products);
            db.SaveChanges();
            MainWindowViewModel.Instance.PageSwich = new AdminViewModel();
        }
    }
}
