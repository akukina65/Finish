using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;
using System.Collections.Generic;

namespace DemoText12.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance { get; set; }
        public static Window MainWindow { get; set; }

        [ObservableProperty] public ViewModelBase pageSwich = new LoginViewModel();
        
        
        [ObservableProperty] public User? currentUser;
        public MainWindowViewModel()
        {
            Instance = this;
        }

        public Stack<ViewModelBase> prePage = new Stack<ViewModelBase>();
        public Stack<ViewModelBase> nextPage = new Stack<ViewModelBase>();

        public bool roureFlag = true;
        [ObservableProperty] public bool isPre;
        [ObservableProperty] public bool isNext;


        partial void OnPageSwichChanged(ViewModelBase? oldValue, ViewModelBase newValue)
        {
           if(roureFlag==true)
            {
                prePage.Push(oldValue);
            }
            else
            {
                nextPage.Push(oldValue);
            }

           roureFlag= true;

            IsNext = nextPage?.Count != 0;
            IsPre = prePage?.Count != 0;
        }
    }
}
