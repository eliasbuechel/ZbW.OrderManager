using BusinessLayer.ArticleGroups.Models;
using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Articles.Models;
using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Models;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.ViewModels;
using BusinessLayer.Customers.Models;
using BusinessLayer.Customers.ViewModels;
using BusinessLayer.Dashboard.ViewModels;
using DataLayer.ArticleGroups.Services.ArticleGroupCreators;
using DataLayer.ArticleGroups.Services.ArticleGroupDeletors;
using DataLayer.ArticleGroups.Services.ArticleGroupProviders;
using DataLayer.ArticleGroups.Services.ArticleGroupUpdator;
using DataLayer.Articles.Services.ArticleCreator;
using DataLayer.Articles.Services.ArticleDeletors;
using DataLayer.Articles.Services.ArticleEditors;
using DataLayer.Articles.Services.ArticleProviders;
using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.Services.CustomerCreators;
using DataLayer.Customers.Services.CustomerDeletors;
using DataLayer.Customers.Services.CustomerEditors;
using DataLayer.Customers.Services.CustomerProviders;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace PresentationLayer
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Server=.;Database=OrderManagerTestV2;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;";
        private readonly Manager _manager;
        private readonly ManagerStore _managerStore;
        private readonly ManagerDbContextFactory _orderDbContextFactory;

        private readonly NavigationStore _navigationStore;
        private readonly NavigationService _dashboardViewModelNavigationService;
        private readonly NavigationService _customerListingViewModelNavigationService;
        private readonly NavigationService _createCustomerViewModelNavigationService;
        private readonly NavigationService _articleGroupListingViewModelNavigationService;
        private readonly NavigationService _createArticleGroupViewModelNavigationService;
        private readonly NavigationService _articleListingViewModelNavigationService;
        private readonly NavigationService _createArticleViewModelNavigationService;

        public App()
        {
            _orderDbContextFactory = new ManagerDbContextFactory(CONNECTION_STRING);

            ICustomerProvider customerProvider = new DatabaseCustomerProvider(_orderDbContextFactory);
            ICustomerCreator customerCreator = new DatabaseCustomerCreator(_orderDbContextFactory);
            ICustomerDeletor cusotmerDeletor = new DatabaseCustomerDeletor(_orderDbContextFactory);
            ICustomerEditor customerEditor = new DatabaseCustomerEditor(_orderDbContextFactory);

            IArticleGroupProvider articleGroupProvider = new DatabaseArticleGroupProviders(_orderDbContextFactory);
            IArticleGroupCreator articleGroupCreator = new DatabaseArticleGroupCreator(_orderDbContextFactory);
            IArticleGroupDeletor articleGroupDeletor = new DatabaseArticleGroupDeletor(_orderDbContextFactory);
            IArticleGroupUpdator articleGroupUpdator = new DatabaseArticleGroupUpdator(_orderDbContextFactory);

            IArticleProvider articleProvider = new DatabaseArticleProvider(_orderDbContextFactory);
            IArticleCreator articleCreator = new DatabaseArticleCreator(_orderDbContextFactory);
            IArticleDeletor articleDeletor = new DatabaseArticleDeletor(_orderDbContextFactory);
            IArticleEditor articleEditor = new DatabaseArticleEditor(_orderDbContextFactory);

            CustomerList customerList = new CustomerList(customerProvider, customerCreator, cusotmerDeletor, customerEditor);
            ArticleGroupList articleGroupList = new ArticleGroupList(articleGroupProvider, articleGroupCreator, articleGroupDeletor, articleGroupUpdator);
            ArticleList articleList = new ArticleList(articleProvider, articleCreator, articleDeletor, articleEditor);

            _manager = new Manager(customerList, articleGroupList, articleList);
            _managerStore = new ManagerStore(_manager);
            _navigationStore = new NavigationStore();

            _dashboardViewModelNavigationService = new NavigationService(_navigationStore, CreateDashboardViewModel);
            _customerListingViewModelNavigationService = new NavigationService(_navigationStore, CreateCustomerListingViewModel);
            _createCustomerViewModelNavigationService = new NavigationService(_navigationStore, CreateCreateCustomerViewModel);
            _articleGroupListingViewModelNavigationService = new NavigationService(_navigationStore, CreateArticleGroupListingViewModel);
            _createArticleGroupViewModelNavigationService = new NavigationService(_navigationStore, CreateCreateArticleGroupViewModel);
            _articleListingViewModelNavigationService = new NavigationService(_navigationStore, CreateArticleListingViewModel);
            _createArticleViewModelNavigationService = new NavigationService(_navigationStore, CreateCreateArticleViewModel);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using ManagerDbContext dbContext = _orderDbContextFactory.CreateDbContext();
            dbContext.Database.Migrate();

            _navigationStore.CurrentViewModel = CreateDashboardViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore,_dashboardViewModelNavigationService, _customerListingViewModelNavigationService, _articleGroupListingViewModelNavigationService, _articleListingViewModelNavigationService)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private DashboardViewModel CreateDashboardViewModel()
        {
            return DashboardViewModel.LoadViewModel();
        }
        private CustomerListingViewModel CreateCustomerListingViewModel()
        {
            return CustomerListingViewModel.LoadViewModel(_managerStore, _navigationStore, _createCustomerViewModelNavigationService, _customerListingViewModelNavigationService);
        }
        private CreateCustomerViewModel CreateCreateCustomerViewModel()
        {
            return new CreateCustomerViewModel(_managerStore, _customerListingViewModelNavigationService);
        }
        private ArticleGroupListingViewModel CreateArticleGroupListingViewModel()
        {
            return ArticleGroupListingViewModel.LoadViewModel(_managerStore, _navigationStore, _articleGroupListingViewModelNavigationService, _createArticleGroupViewModelNavigationService);
        }
        private CreateArticleGroupViewModel CreateCreateArticleGroupViewModel()
        {
            return CreateArticleGroupViewModel.LoadViewModel(_managerStore, _articleGroupListingViewModelNavigationService);
        }
        private ArticleListingViewModel CreateArticleListingViewModel()
        {
            return ArticleListingViewModel.LoadViewModel(_managerStore, _navigationStore, _articleListingViewModelNavigationService, _createArticleViewModelNavigationService);
        }
        private CreateArticleViewModel CreateCreateArticleViewModel()
        {
            return CreateArticleViewModel.LoadViewModel(_managerStore, _articleListingViewModelNavigationService);
        }
    }
}
