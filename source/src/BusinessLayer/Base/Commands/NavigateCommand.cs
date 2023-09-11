using BusinessLayer.Base.Services;

namespace BusinessLayer.Base.Commands
{
    public class NavigateCommand : BaseCommand
    {
        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }

        protected readonly INavigationService _navigationService;
    }
}