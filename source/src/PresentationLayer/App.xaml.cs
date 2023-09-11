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
using DataLayer.Orders.Services.OrderCreators;
using DataLayer.Orders.Services.OrderDeletors;
using DataLayer.Orders.Services.OrderProviders;
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

                    services.AddTransient(s => DashboardViewModel.LoadViewModel());
                    services.AddSingleton(GetReqiredService<DashboardViewModel>);

                    services.AddSingleton<NavigationService<DashboardViewModel>>();

                    services.AddSingleton(s =>
                        new NavigationService<DashboardViewModel>(
                            s.GetRequiredService<NavigationStore>(),
                            s.GetRequiredService<Func<DashboardViewModel>>()
                            )
                        );

                    services.AddTransient(CreateCustomerListingViewModel);
                    services.AddSingleton(GetReqiredService<CustomerListingViewModel>);
                    services.AddSingleton<NavigationService<CustomerListingViewModel>>();

                    services.AddTransient(CreateArticleGroupListingViewModel);
                    services.AddSingleton(GetReqiredService<ArticleGroupListingViewModel>);
                    services.AddSingleton<NavigationService<ArticleGroupListingViewModel>>();

                    services.AddTransient(CreateCreateArticleGroupViewModel);
                    services.AddSingleton(GetReqiredService<CreateArticleGroupViewModel>);
                    services.AddSingleton<NavigationService<CreateArticleGroupViewModel>>();

                    services.AddTransient(CreateArticleListingViewModel);
                    services.AddSingleton(GetReqiredService<ArticleListingViewModel>);
                    services.AddSingleton<NavigationService<ArticleListingViewModel>>();

                    services.AddTransient(CreateCreateArticleViewModel);
                    services.AddSingleton(GetReqiredService<CreateArticleViewModel>);
                    services.AddSingleton<NavigationService<CreateArticleViewModel>>();

                    services.AddTransient(CrateOrderListingViewModel);
                    services.AddSingleton(GetReqiredService<OrderListingViewModel>);
                    services.AddSingleton<NavigationService<OrderListingViewModel>>();

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

        private Func<T> GetReqiredService<T>(IServiceProvider s) where T : notnull
        {
            return () => s.GetRequiredService<T>();
        }
        private static OrderListingViewModel CrateOrderListingViewModel(IServiceProvider s)
        {
            return OrderListingViewModel.LoadViewModel(
              s.GetRequiredService<ManagerStore>(),
              s.GetRequiredService<NavigationStore>()
              );
        }
        private static CreateArticleViewModel CreateCreateArticleViewModel(IServiceProvider s)
        {
            return CreateArticleViewModel.LoadViewModel(
              s.GetRequiredService<ManagerStore>(),
              s.GetRequiredService<NavigationService<ArticleListingViewModel>>(),
              s.GetRequiredService<IArticleValidator>()
              );
        }
        private static ArticleListingViewModel CreateArticleListingViewModel(IServiceProvider s)
        {
            return ArticleListingViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<NavigationService<ArticleListingViewModel>>(),
                s.GetRequiredService<NavigationService<CreateArticleViewModel>>(),
                s.GetRequiredService<IArticleValidator>()
                );
        }
        private static CreateArticleGroupViewModel CreateCreateArticleGroupViewModel(IServiceProvider s)
        {
            return CreateArticleGroupViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationService<ArticleGroupListingViewModel>>(),
                s.GetRequiredService<IArticleGroupValidator>()
                );
        }
        private static ArticleGroupListingViewModel CreateArticleGroupListingViewModel(IServiceProvider s)
        {
            return ArticleGroupListingViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<NavigationService<ArticleGroupListingViewModel>>(),
                s.GetRequiredService<NavigationService<CreateArticleGroupViewModel>>(),
                s.GetRequiredService<IArticleGroupValidator>()
                );
        }
        private static CustomerListingViewModel CreateCustomerListingViewModel(IServiceProvider s)
        {
            return CustomerListingViewModel.LoadViewModel(
                s.GetRequiredService<ManagerStore>(),
                s.GetRequiredService<NavigationStore>(),
                s.GetRequiredService<ICustomerValidator>(),
                s.GetRequiredService<IDialogService>()
                );
        }

        private readonly IHost _host;
        private const string CONNECTION_STRING = "Server=.;Database=OrderManagerTestV2;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False;";
    }
}