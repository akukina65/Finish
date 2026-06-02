using CommunityToolkit.Mvvm.ComponentModel;
using DemoText12.Models;

namespace DemoText12.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public static KukinaContext db = new KukinaContext();

       
        public void LoadPre()
        {
            MainWindowViewModel.Instance.roureFlag = false;
            MainWindowViewModel.Instance.PageSwich = MainWindowViewModel.Instance.prePage.Pop();
        }

        public void LoaNext()
        {
            MainWindowViewModel.Instance.roureFlag = true;
            MainWindowViewModel.Instance.PageSwich = MainWindowViewModel.Instance.nextPage.Pop();
        }
    }
}
