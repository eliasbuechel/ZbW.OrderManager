using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Customers.Models;
using DataLayer.Customers.DTOs;
using DataLayer.Customers.Services.CustomerCreators;
using DataLayer.Customers.Services.CustomerDeletors;
using DataLayer.Customers.Services.CustomerEditors;
using DataLayer.Customers.Services.CustomerProviders;
using DataLayer.Customers.Validation;

[TestFixture]
public class CustomerListTests
{
    private CustomerList _customerList;
    private Mock<ICustomerProvider> _mockCustomerProvider;
    private Mock<ICustomerCreator> _mockCustomerCreator;
    private Mock<ICustomerDeletor> _mockCustomerDeletor;
    private Mock<ICustomerEditor> _mockCustomerEditor;
    private Mock<ICustomerValidator> _mockCustomerValidator;

    [SetUp]
    public void SetUp()
    {
        _mockCustomerProvider = new Mock<ICustomerProvider>();
        _mockCustomerCreator = new Mock<ICustomerCreator>();
        _mockCustomerDeletor = new Mock<ICustomerDeletor>();
        _mockCustomerEditor = new Mock<ICustomerEditor>();
        _mockCustomerValidator = new Mock<ICustomerValidator>();

        _customerList = new CustomerList(
            _mockCustomerProvider.Object,
            _mockCustomerCreator.Object,
            _mockCustomerDeletor.Object,
            _mockCustomerEditor.Object,
            _mockCustomerValidator.Object);
    }

    [Test]
    public async Task GetAllCustomersAsync_ReturnsExpectedCustomers()
    {
        // Arrange
        var expectedCustomers = new List<CustomerDTO> {  };
        _mockCustomerProvider.Setup(m => m.GetAllCustomersAsync()).ReturnsAsync(expectedCustomers);

        // Act
        var result = await _customerList.GetAllCustomersAsync();

        // Assert
        Assert.AreEqual(expectedCustomers, result);
    }
}
