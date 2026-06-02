using Avalonia.Controls;
using DemoText12.ViewModels;
using System.ComponentModel;

namespace DemoText12.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel.MainWindow = this;
        }
    }
}