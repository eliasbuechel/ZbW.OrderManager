using BusinessLayer.Base.ViewModels;

namespace BusinessLayer.Dashboard.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public static DashboardViewModel LoadViewModel()
        {
            return new DashboardViewModel();
        }
    }
}
