using BusinessLayer.Base.Services;

namespace BusinessLayer.Base.Command
{
    public class NavigateCommand : BaseCommand
    {

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }

        private readonly NavigationService _navigationService;
    }
}