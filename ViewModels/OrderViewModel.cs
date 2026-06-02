using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoText12.ViewModels
{
   
    public partial class OrderViewModel:ViewModelBase
    {
        [ObservableProperty] List<Order> orders;
        [ObservableProperty] public Order rowSelect;

        public OrderViewModel()
        {
            Orders = db.Orders.Include(x => x.IdAdressPointNavigation).Include(x => x.IdOrderStatusNavigation).ToList();
        }

        public void CreateOrder()
        {
            Order order = new Order();
            MainWindowViewModel.Instance.PageSwich = new CreateOrderViewModel(order);
        }
        partial void OnRowSelectChanged(Order value)
        {
            MainWindowViewModel.Instance.PageSwich = new EditOrderViewModel(value);
        }
        public async void Removerder(Order order)
        {
            var old = MessageBoxManager.GetMessageBoxStandard("Уведомление", "Вы уверены то хотите удалить", ButtonEnum.YesNo, Icon.Question);
            var result = await old.ShowAsync();
            if (result == ButtonResult.Yes)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                Orders = db.Orders.Include(x => x.IdAdressPointNavigation).Include(x => x.IdOrderStatusNavigation).ToList();
            }
        }
    }
}
