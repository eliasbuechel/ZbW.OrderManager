using BusinessLayer.ArticleGroups.Models;
using BusinessLayer.Articles.Models;
using BusinessLayer.Customers.Models;
using BusinessLayer.Orders.Models;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.Articles.DTOs;
using DataLayer.Customers.DTOs;
using DataLayer.Orders.DTOs;

namespace BusinessLayer.Base.Models
{
    public class Manager
    {
        public Manager(CustomerList customerList, ArticleGroups.Models.ArticleGroupList articleGroupList, ArticleList articleList, OrderList orderList)
        {
            _customerList = customerList;
            _articleGroupList = articleGroupList;
            _articleList = articleList;
            _orderList = orderList;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            return await _customerList.GetAllCustomersAsync();
        }
        public async Task CreateCustomerAsync(CustomerDTO customer)
        {
            await _customerList.CreateCustomerAsync(customer);
        }
        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _customerList.GetNextFreeCustomerIdAsync();
        }
        public async Task DeleteCustomerAsync(CustomerDTO customer)
        {
            await _customerList.DeleteCustomerAsync(customer);
        }
        public async Task EditCustomerAsync(CustomerDTO initialCustomer, CustomerDTO editedCustomer)
        {
            await _customerList.EditCustomerAsync(initialCustomer, editedCustomer);
        }

        public async Task CreateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup)
        {
            await _articleGroupList.CreateArticleGroupAsync(creatingArticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _articleGroupList.GetNextFreeArticleGroupIdAsync();
        }
        public async Task<IEnumerable<ArticleGroupDTO>> GetAllArticleGroupsAsync()
        {
            return await _articleGroupList.GetAllArticleGroupsAsync();
        }
        public async Task DeleteArticleGroupAsync(ArticleGroupDTO articleGroup)
        {
            await _articleGroupList.DeleteArticleGroupAsync(articleGroup);
        }
        public async Task UpdateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO articleGroup)
        {
            await _articleGroupList.UpdateArticleGroupAsync(articleGroup);
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            return await _articleList.GetAllArticlesAsync();
        }
        public async Task CreateArticleAsync(ArticleDTO article)
        {
            await _articleList.CreateArticleAsync(article);
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            return await _articleList.GetNextFreeArticleIdAsync();
        }
        public async Task DeleteArticleAsync(ArticleDTO article)
        {
            await _articleList.DeleteArticleAsync(article);
        }
        public async Task SaveChangesToArticleAsync(ArticleDTO initialArticle, ArticleDTO editedArticle)
        {
            await _articleList.SaveChangesToArticleAsync(initialArticle, editedArticle);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _orderList.GetAllOrdersAsync();
        }
        public async Task<OrderDTO> CreateOrderAsync(CreatingOrderDTO order)
        {
            return await _orderList.CreateOrderAsync(order);
        }
        public async Task DeleteOrderAsync(OrderDTO order)
        {
            await _orderList.DeleteOrderAsync(order);
        }

        private readonly CustomerList _customerList;
        private readonly ArticleGroupList _articleGroupList;
        private readonly ArticleList _articleList;
        private readonly OrderList _orderList;
    }
}
