using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoText12.ViewModels
{
    public partial class EditOrderViewModel : ViewModelBase
    {
        [ObservableProperty] public ProductOrder detailOrder = new();

        [ObservableProperty] public Order orders;

       
        [ObservableProperty] public List<OrderStatus> orderStatus = db.OrderStatuses.ToList();
        [ObservableProperty] public List<AdressPoint> adressPoints = db.AdressPoints.ToList();
        

        public EditOrderViewModel(Order order) 
        { 
           Orders = order;
        }


        public DateTimeOffset DateOrd
        { 
            get=> new DateTimeOffset(Orders.OrderDate,TimeOnly.MinValue, TimeSpan.Zero);
            set => Orders.DeliveryDate = new DateOnly(value.Year, value.Month, value.Day);
        }
        public DateTimeOffset DatePol
        {
            get => new DateTimeOffset(Orders.DeliveryDate, TimeOnly.MinValue, TimeSpan.Zero);
            set => Orders.DeliveryDate = new DateOnly(value.Year, value.Month, value.Day);
        }

        public void Save()
        {
            db.SaveChanges();
            MainWindowViewModel.Instance.PageSwich = new OrderViewModel();
        }
    }
}
