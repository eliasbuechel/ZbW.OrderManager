using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Dashboard.ViewModels
{
    public class DashboardViewModel : BaseViewModel, IMainNavigationViewModel
    {
        public static DashboardViewModel LoadViewModel()
        {
            return new DashboardViewModel();
        }
        private DashboardViewModel()
        {
        }
    }
}
