using BusinessLayer.ArticleGroups.Models;
using BusinessLayer.Articles.Models;
using BusinessLayer.Customers.Models;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.Models;
using DataLayer.Customers.Models;

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

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerList.GetAllCustomers();
        }
        public async Task CreateCustomer(Customer customer)
        {
            await _customerList.CreateCustomer(customer);
        }
        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _customerList.GetNextFreeCustomerIdAsync();
        }
        public async Task DeleteCustomer(Customer customer)
        {
            await _customerList.DeleteCustomer(customer);
        }
        public async Task EditCustomer(Customer initialCustomer, Customer editedCustomer)
        {
            await _customerList.EditCustomer(initialCustomer, editedCustomer);
        }

        public async Task CreateArticleGroup(CreatingArticleGroup creatingArticleGroup)
        {
            await _articleGroupList.CreateArticleGroup(creatingArticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _articleGroupList.GetNextFreeArticleGroupIdAsync();
        }
        public async Task<IEnumerable<ArticleGroup>> GetAllArticleGroups()
        {
            return await _articleGroupList.GetAllArticleGroups();
        }
        public async Task DeleteArticleGroup(ArticleGroup articleGroup)
        {
            await _articleGroupList.DeleteArticleGroup(articleGroup);
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _articleList.GetAllArticlesAsync();
        }
        public async Task CreateArticleAsync(Article article)
        {
            await _articleList.CreateArticleAsync(article);
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            return await _articleList.GetNextFreeArticleIdAsync();
        }
        public async Task DeleteArticleAsync(Article article)
        {
            await _articleList.DeleteArticleAsync(article);
        }
        public async Task SaveChangesToArticleAsync(Article initialArticle, Article editedArticle)
        {
            await _articleList.SaveChangesToArticleAsync(initialArticle, editedArticle);
        }

        private readonly CustomerList _customerList;
        private readonly ArticleGroupList _articleGroupList;
        private readonly ArticleList _articleList;
    }
}
