using BusinessLayer.ArticleGroups.Models;
using BusinessLayer.Articles.Models;
using BusinessLayer.Customers.Models;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.Articles.DTOs;
using DataLayer.Customers.DTOs;

namespace BusinessLayer.Base.Models
{
    public class Manager
    {
        public Manager(CustomerList customerList, ArticleGroups.Models.ArticleGroupList articleGroupList, ArticleList articleList)
        {
            _customerList = customerList;
            _articleGroupList = articleGroupList;
            _articleList = articleList;
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

        private readonly CustomerList _customerList;
        private readonly ArticleGroupList _articleGroupList;
        private readonly ArticleList _articleList;
    }
}
