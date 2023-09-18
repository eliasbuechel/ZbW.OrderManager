using BusinessLayer.Articles.ViewModels;
using BusinessLayer.Base.Stores;
using BusinessLayer.Base.Services;
using BusinessLayer.Articles.Commands;
using DataLayer.Articles.DTOs;

namespace BusinessLayer.Tests
{
    using BusinessLayer.Articles.Models;
    using BusinessLayer.Base.Models;
    using BusinessLayer.Customers.Models;
    using DataLayer.ArticleGroups.DTOs;
    using DataLayer.Customers.Services.CustomerCreators;
    using DataLayer.Customers.Services.CustomerDeletors;
    using DataLayer.Customers.Services.CustomerEditors;
    using DataLayer.Customers.Services.CustomerProviders;
    using DataLayer.Customers.Validation;
    using Moq;
    using NUnit.Framework;
    using System.Threading.Tasks;
    public class CreateArticleCommandTests
    {
        private Mock<ManagerStore> _mockManagerStore;
        private Mock<CreateArticleViewModel> _mockCreateArticleViewModel;
        private Mock<NavigationService> _mockNavigationService;
        private CreateArticleCommand _command;

        [SetUp]
        public void Setup()
        {
            var mockArticleGroupUpdatable = new Mock<IArticleGroupUpdatable>();

            mockArticleGroupUpdatable.Setup(x => x.UpdateArticleGroups(It.IsAny<IEnumerable<ArticleGroupDTO>>()))
                                     .Callback(() => { });

            // Mocking Abhängigkeiten von CustomerList
            var mockCustomerProvider = new Mock<ICustomerProvider>();
            var mockCustomerCreator = new Mock<ICustomerCreator>();
            var mockCustomerDeletor = new Mock<ICustomerDeletor>();
            var mockCustomerEditor = new Mock<ICustomerEditor>();
            var mockCustomerValidator = new Mock<ICustomerValidator>();

            //Mock für CustomerList unter Verwendung der gemockten Abhängigkeiten
            var mockCustomerList = new Mock<CustomerList>(mockCustomerProvider.Object, mockCustomerCreator.Object, mockCustomerDeletor.Object, mockCustomerEditor.Object, mockCustomerValidator.Object);

            var mockArticleGroupList = new Mock<ArticleGroups.Models.ArticleGroupList>();
            var mockArticleList = new Mock<ArticleList>();

            // Mock für Manager unter Verwendung der gemockten Abhängigkeiten
            var mockManager = new Mock<Manager>(mockCustomerList.Object, mockArticleGroupList.Object, mockArticleList.Object);

            // Haupt-Instanz von ManagerStore
            _mockCreateArticleViewModel = new Mock<CreateArticleViewModel>();
            _mockNavigationService = new Mock<NavigationService>();

            _mockManagerStore = new Mock<ManagerStore>(mockManager.Object);
        }

        [Test]
        public void CanExecute_ReturnsFalse_WhenViewModelHasErrors()
        {
            _mockCreateArticleViewModel.Setup(vm => vm.HasErrors).Returns(true);

            bool result = _command.CanExecute(null);

            Assert.IsFalse(result);
        }

        //[test]
        //public void executeasync_doesnotcreatearticle_whenarticlegroupisnull()
        //{
        //    _mockcreatearticleviewmodel.setup(vm => vm.articlegroup).returns((string)null);

        //    _command.executeasync(null);

        //    _mockmanagerstore.verify(ms => ms.createarticleasync(it.isany<articledto>()), times.never);
        //}
    }
}
