using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DemoText12.ViewModels
{
    public partial class CreateOrderViewModel : ViewModelBase
    {
        [ObservableProperty] public Order orders;

        [ObservableProperty] public List<OrderStatus> orderStatus = db.OrderStatuses.ToList();
        [ObservableProperty] public List<AdressPoint> adressPoints = db.AdressPoints.ToList();
        public CreateOrderViewModel(Order order)
        {
           Orders = order;  
        }

       

        public DateTimeOffset DateOrd
        {
            get => new DateTimeOffset(Orders.OrderDate, TimeOnly.MinValue, TimeSpan.Zero);
            set => Orders.OrderDate = new DateOnly(value.Year, value.Month, value.Day);
        }

        public DateTimeOffset DatePol
        {
            get => new DateTimeOffset(Orders.DeliveryDate, TimeOnly.MinValue, TimeSpan.Zero);
            set => Orders.DeliveryDate = new DateOnly(value.Year, value.Month, value.Day);
        }

        public void Save()
        {
            Orders.Id = 0;
            db.Orders.Add(Orders);
            db.SaveChanges();

            MainWindowViewModel.Instance.PageSwich = new OrderViewModel();
        }
    }
}