using System.Windows.Input;
using Eternity.ViewModels.Dashboard;

namespace Eternity.ViewModels.Home
{
    internal abstract class HomeChildScreen<TModel> : BaseViewModel<TModel>
    {
        protected HomeChildScreen(HomeViewModel parent)
        {
            Parent = parent;
        }

        protected HomeViewModel Parent;

        public ICommand BackCommand { get; set; }

        public void Back()
        {
            Parent.Navigate<DashboardViewModel>();
        }
    }
}
