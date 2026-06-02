using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoText12.ViewModels
{
    public class ProductWithImage
    {
        public Product Product { get; set; }
        public Bitmap Image { get; set; }
    }
    
    public partial class AdminViewModel:ViewModelBase
    {


        [ObservableProperty] public List<ProductWithImage> productWithImage = new();
        [ObservableProperty] public List<ProductWithImage> allProduct = new();
        [ObservableProperty] public List<Product> products = db.Products.Include(x=>x.IdMakerNavigation).Include(x => x.IdProductCategoryNavigation).Include(x => x.IdProductNameNavigation).Include(x=>x.IdSupplierNavigation).ToList();

        [ObservableProperty] public User? user;

        [ObservableProperty] public string textFind;
        [ObservableProperty] public string selectedDiscount;
        [ObservableProperty] public List<string> discountOptions;
        [ObservableProperty] public ProductWithImage rowSelect;

        [ObservableProperty] public List<string> price;
        [ObservableProperty] public List<string> counOnStock;
        [ObservableProperty] public string selectPrice;
        [ObservableProperty] public string selectCountOnSrock;

        partial void OnTextFindChanged(string value) => AllFilter();

     

        partial void OnRowSelectChanged(ProductWithImage value)
        {
           MainWindowViewModel.Instance.PageSwich = new EditViewModel(value.Product);
        }

        public void SortCount(int count)
        {
            switch(count)
            {
                case 1:
                    ProductWithImage = ProductWithImage.OrderBy(x=>x.Product.CountOnStock).ToList();
                    break;
                case 2:
                    ProductWithImage = ProductWithImage.OrderByDescending(x => x.Product.CountOnStock).ToList();
                    break;
                default:
                    break;
            }
        }
        public void AllFilter()
        {
            ProductWithImage = AllProduct.ToList();
            if (!string.IsNullOrEmpty(TextFind)) ProductWithImage = ProductWithImage.Where(
                x=>x.Product.IdProductNameNavigation.Name.Contains(TextFind)|| x.Product.IdProductCategoryNavigation.Name.Contains(TextFind)).ToList();

            switch (SelectedDiscount)
            {
                case "Скидка 0-10.99%":
                    ProductWithImage = ProductWithImage.Where(x=>x.Product.Discount>=0&&x.Product.Discount<=10.99).ToList();
                    break;
                case "Скидка 11-14.99%":
                    ProductWithImage = ProductWithImage.Where(x => x.Product.Discount >= 11 && x.Product.Discount <= 14.99).ToList();
                    break;
                case "Скидка 15 и более":
                    ProductWithImage = ProductWithImage.Where(x => x.Product.Discount >= 15).ToList();
                    break;
                default:
                    break;
            }

            switch(SelectPrice)
            {
                case "Цена по возрастанию":
                    ProductWithImage = ProductWithImage.OrderBy(x=>x.Product.Price).ToList();
                    break;
                case "Цена по убыванию":
                    ProductWithImage = ProductWithImage.OrderByDescending(x => x.Product.Price).ToList();
                    break;
                default:
                    break;
            }

            switch (SelectCountOnSrock)
            {
                case "Количество на складе по убыванию":
                    ProductWithImage = ProductWithImage.OrderBy(x => x.Product.CountOnStock).ToList();
                    break;
                case "Количество на складе по возрастанию":
                    ProductWithImage = ProductWithImage.OrderByDescending(x => x.Product.CountOnStock).ToList();
                    break;
                default:
                    break;
            }

        }

        public async void RemoScreem(ProductWithImage removeProduct)
        {
            if(db.ProductOrders.Any(x=>x.IdProduct==removeProduct.Product.Id))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка","Вы не можите удалить",ButtonEnum.Ok, Icon.Info).ShowAsync();
                return;

            }

            var old = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Вы не можите удалить", ButtonEnum.YesNo, Icon.Question);
            var result = await old.ShowAsync();
            if (result == ButtonResult.Yes)
            {
                db.Products.Remove(removeProduct.Product);
                db.SaveChanges();
                Products = db.Products.Include(x => x.IdMakerNavigation).Include(x => x.IdProductCategoryNavigation).Include(x => x.IdProductNameNavigation).Include(x => x.IdSupplierNavigation).ToList();
                LoadProduct();
            }
        }
        
        public void CreateScreen()
        {
            Product product = new Product();
            MainWindowViewModel.Instance.PageSwich = new CreateViewModel(product);
        }
        partial void OnSelectCountOnSrockChanged(string value) => AllFilter();





        partial void OnSelectPriceChanged(string value) => AllFilter();





        public string FullName => $"{user?.Surname} {user.Name} {user?.Patronymic}";
        public AdminViewModel()
        {
            user = MainWindowViewModel.Instance.CurrentUser;
            LoadProduct();
            DiscountOptions = new List<string> { "Все товары", "Скидка 0-10.99%", "Скидка 11-14.99%", "Скидка 15 и более" };
            SelectedDiscount = DiscountOptions.First();
            Price = new List<string> { "Все товары", "Цена по возрастанию", "Цена по убыванию" };
            SelectPrice = Price.First();
            CounOnStock = new List<string> { "Все товары","Количество на складе по убыванию", "Количество на складе по возрастанию" };
            SelectCountOnSrock = CounOnStock.First();
        }

        partial void OnSelectedDiscountChanged(string value) => AllFilter();




        public void LoadProduct()
        {
            AllProduct = products.Select(
                pr=> new ProductWithImage
                {
                    Product = pr,
                    Image = LoadImage(pr.Image)
                }).ToList();
            ProductWithImage = AllProduct;
        }

        public void OrderScreen()
        {
            MainWindowViewModel.Instance.PageSwich = new OrderViewModel();
        }

        public Bitmap LoadImage(string imageName)
        {
            var path = Path.Combine("Assets", imageName ?? "picture.png");
            return File.Exists(path) ? new Bitmap(path) : new Bitmap("Assets/picture.png");
        }



    }
}
