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
using BusinessLayer.Orders.Models;
using BusinessLayer.Orders.Services.OrderCreators;
using BusinessLayer.Orders.Services.OrderDeletors;
using BusinessLayer.Orders.Services.OrderProviders;
using BusinessLayer.Orders.ViewModels;
using DataLayer.ArticleGroups.Services.ArticleGroupCreators;
using DataLayer.ArticleGroups.Services.ArticleGroupDeletors;
using DataLayer.ArticleGroups.Services.ArticleGroupProviders;
using DataLayer.ArticleGroups.Services.ArticleGroupUpdator;
using DataLayer.ArticleGroups.Validation;
using DataLayer.Articles.Services.ArticleCreators;
using DataLayer.Articles.Services.ArticleDeletors;
using DataLayer.Articles.Services.ArticleEditors;
using DataLayer.Articles.Services.ArticleProviders;
using DataLayer.Articles.Validation;
using DataLayer.Base.DatabaseContext;
using DataLayer.Customers.Services.CustomerCreators;
using DataLayer.Customers.Services.CustomerDeletors;
using DataLayer.Customers.Services.CustomerEditors;
using DataLayer.Customers.Services.CustomerProviders;
using DataLayer.Customers.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using PresentationLayer.Base.Services;
using System;
using System.Windows;

namespace PresentationLayer
{
    public partial class App : Application
    {
        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
                {
                    services.AddSingleton(new ManagerDbContextFactory(CONNECTION_STRING));

                    services.AddSingleton<ICustomerValidator, CustomerValidator>();
                    services.AddSingleton<ICustomerProvider, DatabaseCustomerProvider>();
                    services.AddSingleton<ICustomerCreator, DatabaseCustomerCreator>();
                    services.AddSingleton<ICustomerDeletor, DatabaseCustomerDeletor>();
                    services.AddSingleton<ICustomerEditor, DatabaseCustomerEditor>();

                    services.AddSingleton<IArticleGroupValidator, ArticleGroupValidator>();
                    services.AddSingleton<IArticleGroupProvider, DatabaseArticleGroupProvider>();
                    services.AddSingleton<IArticleGroupCreator, DatabaseArticleGroupCreator>();
                    services.AddSingleton<IArticleGroupDeletor, DatabaseArticleGroupDeletor>();
                    services.AddSingleton<IArticleGroupUpdator, DatabaseArticleGroupUpdator>();

                    services.AddSingleton<IArticleValidator, ArticleValidator>();
                    services.AddSingleton<IArticleProvider, DatabaseArticleProvider>();
                    services.AddSingleton<IArticleCreator, DatabaseArticleCreator>();
                    services.AddSingleton<IArticleDeletor, DatabaseArticleDeletor>();
                    services.AddSingleton<IArticleEditor, DatabaseArticleEditor>();

                    services.AddSingleton<IOrderProvider, DatabaseOrderProvider>();
                    services.AddSingleton<IOrderCreator, DatabaseOrderCreator>();
                    services.AddSingleton<IOrderDeletor, DatabaseOrderDeletor>();

                    services.AddSingleton<IDialogService, DialogService>();

                    services.AddSingleton<CustomerList>();
                    services.AddSingleton<ArticleGroupList>();
                    services.AddSingleton<ArticleList>();
                    services.AddSingleton<OrderList>();

                    services.AddSingleton<NavigationStore>();

                    services.AddTransient<DashboardViewModel>();
                    services.AddSingleton<Func<DashboardViewModel>>(s => () => s.GetRequiredService<DashboardViewModel>());
                    services.AddSingleton<NavigationService<DashboardViewModel>>();

                    services.AddTransient(s => CreateCustomerListingViewModel(s));
                    services.AddSingleton<Func<CustomerListingViewModel>>(s => () => s.GetRequiredService<CustomerListingViewModel>());
                    services.AddSingleton<NavigationService<CustomerListingViewModel>>();

                    services.AddTransient<CreateCustomerViewModel>();
                    services.AddSingleton<Func<CreateCustomerViewModel>>(s => () => s.GetRequiredService<CreateCustomerViewModel>());
                    services.AddSingleton<NavigationService<CreateCustomerViewModel>>();

                    services.AddTransient(s => CreateArticleGroupListingViewModel(s));
                    services.AddSingleton<Func<ArticleGroupListingViewModel>>(s => () => s.GetRequiredService<ArticleGroupListingViewModel>());
                    services.AddSingleton<NavigationService<ArticleGroupListingViewModel>>();

                    services.AddTransient(s => CreateCreateArticleGroupViewModel(s));
                    services.AddSingleton<Func<CreateArticleGroupViewModel>>(s => () => s.GetRequiredService<CreateArticleGroupViewModel>());
                    services.AddSingleton<NavigationService<CreateArticleGroupViewModel>>();

                    services.AddTransient(s => CreateArticleListingViewModel(s));
                    services.AddSingleton<Func<ArticleListingViewModel>>(s => () => s.GetRequiredService<ArticleListingViewModel>());
                    services.AddSingleton<NavigationService<ArticleListingViewModel>>();

                    services.AddTransient(s => CreateCreateArticleViewModel(s));
                    services.AddSingleton<Func<CreateArticleViewModel>>(s => () => s.GetRequiredService<CreateArticleViewModel>());
                    services.AddSingleton<NavigationService<CreateArticleViewModel>>();

                    services.AddTransient(s => CrateOrderListingViewModel(s));
                    services.AddSingleton<Func<OrderListingViewModel>>(s => () => s.GetRequiredService<OrderListingViewModel>());
                    services.AddSingleton<NavigationService<OrderListingViewModel>>();

                    services.AddTransient(s => CreateCreateOrderViewModel(s));
                    services.AddSingleton<Func<CreateOrderViewModel>>(s => () => s.GetRequiredService<CreateOrderViewModel>());
                    services.AddSingleton<SubNavigationService<OrderListingViewModel, CreateOrderViewModel>>();

                    services.AddSingleton<Manager>();
                    services.AddSingleton<ManagerStore>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ManagerDbContextFactory managerDbContextFactory = _host.Services.GetRequiredService<ManagerDbContextFactory>();
            using ManagerDbContext dbContext = managerDbContextFactory.CreateDbContext();
            dbContext.Database.Migrate();

            NavigationService<DashboardViewModel> dashboardViewModelNavigationService = _host.Services.GetRequiredService<NavigationService<DashboardViewModel>>();
            dashboardViewModelNavigationService.Navigate();

            MainWindow mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }

        private CreateOrderViewModel CreateCreateOrderViewModel(IServiceProvider s)
        {
            return CreateOrderViewModel.LoadViewModel(
               s.GetRequiredService<ManagerStore>(),
               s.GetRequiredService<NavigationStore>(),
               s.GetRequiredService<SubNavigationService<OrderListingViewModel, CreateOrderViewModel>>()
               );
        }
        private OrderListingViewModel CrateOrderListingViewModel(IServiceProvider s)
        {
            return OrderListingViewModel.LoadViewModel(
              s.GetRequiredService<ManagerStore>(),
              s.GetRequiredService<NavigationStore>()
              );
        }
        private CreateArticleViewModel CreateCreateArticleViewModel(IServiceProvider s)
        {
            return CreateArticleViewModel.LoadViewModel(
              s.GetRequiredService<ManagerStore>(),
              s.GetRequiredService<NavigationService<ArticleListingViewModel>>(),
              s.GetRequiredService<IArticleValidator>()
              );
        }
        private ArticleListingViewModel CreateArticleListingViewModel(IServiceProvider s)
        {
            return ArticleListingViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<NavigationService<ArticleListingViewModel>>(),
                s.GetRequiredService<NavigationService<CreateArticleViewModel>>(),
                s.GetRequiredService<IArticleValidator>()
                );
        }
        private CreateArticleGroupViewModel CreateCreateArticleGroupViewModel(IServiceProvider s)
        {
            return CreateArticleGroupViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationService<ArticleGroupListingViewModel>>(),
                s.GetRequiredService<IArticleGroupValidator>()
                );
        }
        private ArticleGroupListingViewModel CreateArticleGroupListingViewModel(IServiceProvider s)
        {
            return ArticleGroupListingViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<NavigationService<ArticleGroupListingViewModel>>(),
                s.GetRequiredService<NavigationService<CreateArticleGroupViewModel>>(),
                s.GetRequiredService<IArticleGroupValidator>()
                );
        }
        private CustomerListingViewModel CreateCustomerListingViewModel(IServiceProvider s)
        {
            return CustomerListingViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<NavigationService<CreateCustomerViewModel>>(),
                s.GetRequiredService<NavigationService<CustomerListingViewModel>>(),
                s.GetRequiredService<ICustomerValidator>(),
                s.GetRequiredService<IDialogService>()
                );
        }

        private readonly IHost _host;
        private const string CONNECTION_STRING = "Server=.;Database=OrderManagerTestV2;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;";
    }
}
