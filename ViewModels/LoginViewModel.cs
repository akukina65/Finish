using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoText12.ViewModels
{
    public partial class LoginViewModel:ViewModelBase
    {
        [ObservableProperty] public string login;
        [ObservableProperty] public string password;


        public async void Enter()
        {
            MainWindowViewModel.Instance.CurrentUser = db.Users.Include(x=>x.IdRoleNavigation).FirstOrDefault(x=>x.Password==Password&&x.Login==Login);
            if(MainWindowViewModel.Instance.CurrentUser == null )
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка","Введите логин или пароль",ButtonEnum.Ok, Icon.Info).ShowAsync();
                
            }
            else
            {
                switch(MainWindowViewModel.Instance.CurrentUser.IdRole)
                {
                    case 1:
                        MainWindowViewModel.Instance.PageSwich = new AdminViewModel(); break;
                     case 2:
                        MainWindowViewModel.Instance.PageSwich = new MenagerViewModel(); break;
                    case 3:
                        MainWindowViewModel.Instance.PageSwich = new UserViewModel(); break;
                    default:
                        break;
                }
            }
        }

        public void GuestScreen()
        {
            MainWindowViewModel.Instance.PageSwich = new GostViewModel();
        }
    }
}
