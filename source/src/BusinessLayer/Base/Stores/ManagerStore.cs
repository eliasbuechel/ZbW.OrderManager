using BusinessLayer.Base.Models;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.Articles.Models;
using DataLayer.Customers.Models;
using Microsoft.Identity.Client;

namespace BusinessLayer.Base.Stores
{
    public class ManagerStore
    {
        public ManagerStore(Manager manager)
        {
            _manager = manager;

            _customers = new List<Customer>();
            _articleGroups = new List<ArticleGroup>();
            _articles = new List<Article>();

            _inizializeLazy = new Lazy<Task>(Inizialize);
        }

        public event Action<Customer>? CustomerCreated;
        public event Action<Customer>? CustomerDeleted;
        public event Action<ArticleGroup>? RootArticleGroupCreated;
        public event Action<ArticleGroup, ArticleGroup>? SubordinateArticleGroupCreated;
        public event Action<ArticleGroup>? RootArticleGroupDeleted;
        public event Action<ArticleGroup, ArticleGroup>? SubordinateArticleGroupDeleted;
        public event Action<Article>? ArticleCreated;
        public event Action<Article>? ArticleDeleted;


        public IEnumerable<Customer> Customers => _customers;
        public IEnumerable<ArticleGroup> ArticleGroups => _articleGroups;
        public IEnumerable<Article> Articles => _articles;

        public async Task Load()
        {
            try
            {
                await _inizializeLazy.Value;
            }
            catch (Exception)
            {
                _inizializeLazy = new Lazy<Task>(Inizialize);
                throw;
            }
        }

        public async Task CreateCustomer(Customer customer)
        {
            await _manager.CreateCustomer(customer);
            _customers.Add(customer);
            OnCustomerCreated(customer);
        }
        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _manager.GetNextFreeCustomerIdAsync();
        }
        public async Task DeleteCustomer(Customer customer)
        {
            await _manager.DeleteCustomer(customer);
            _customers.Remove(customer);
            OnCustomerDeleted(customer);
        }
        public async Task EditCustomer(Customer initialCustomer, Customer editedCustomer)
        {
            await _manager.EditCustomer(initialCustomer, editedCustomer);
            int initialCustomerIndex = _customers.IndexOf(initialCustomer);
            _customers[initialCustomerIndex] = editedCustomer;
        }

        public async Task CreateArticleGroup(CreatingArticleGroup articleGroup)
        {
            await _manager.CreateArticleGroup(articleGroup);

            ArticleGroup? superiorAticleGroup = articleGroup.SuperiorArticleGroup;
            ArticleGroup createdArticleGroup = new ArticleGroup(articleGroup.Id ,articleGroup.Name);

            if (superiorAticleGroup == null)
                CreateRootArticleGroup(createdArticleGroup);
            else
                CreateSubordinateArticleGroup(superiorAticleGroup, createdArticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _manager.GetNextFreeArticleGroupIdAsync();
        }
        public async Task DeleteArticleGroup(ArticleGroup articleGroup)
        {
            if (articleGroup.SubordinateArticleGroups.Count > 0)
                throw new DeletingNonLeaveArticleGroupException(articleGroup);

            await _manager.DeleteArticleGroup(articleGroup);

            foreach (ArticleGroup rootArticleGroup in _articleGroups)
            {
                if (rootArticleGroup.Id == articleGroup.Id)
                {
                    _articleGroups.Remove(articleGroup);
                    OnRootArticleGroupDeleted(articleGroup);
                    return;
                }

                ArticleGroup? superiorArticleGroup = GetSuperiorArticleGroupRecursive(rootArticleGroup, articleGroup);

                if (superiorArticleGroup != null)
                {
                    superiorArticleGroup.SubordinateArticleGroups.Remove(articleGroup);
                    OnSubordinateArticleGroupDeleted(articleGroup, superiorArticleGroup);
                    return;
                }
            }
        }


        public async Task CreateArticleAsync(Article article)
        {
            await _manager.CreateArticleAsync(article);
            _articles.Add(article);
            OnArticleCreated(article);
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            return await _manager.GetNextFreeArticleIdAsync();
        }
        public async Task DeleteArticleAsync(Article article)
        {
            await _manager.DeleteArticleAsync(article);
            _articles.Remove(article);
            OnArticleDeleted(article);
        }


        private void OnCustomerCreated(Customer customer)
        {
            CustomerCreated?.Invoke(customer);
        }
        private void OnCustomerDeleted(Customer customer)
        {
            CustomerDeleted?.Invoke(customer);
        }

        private void CreateSubordinateArticleGroup(ArticleGroup superiorAticleGroup, ArticleGroup createdArticleGroup)
        {
            superiorAticleGroup.SubordinateArticleGroups.Add(createdArticleGroup);
            OnSubordinateArticleGroupCreated(createdArticleGroup, superiorAticleGroup);
        }
        private void CreateRootArticleGroup(ArticleGroup createdArticleGroup)
        {
            _articleGroups.Add(createdArticleGroup);
            OnRootArticleGroupCreated(createdArticleGroup);
        }
        private void OnSubordinateArticleGroupCreated(ArticleGroup createdArticleGroup, ArticleGroup superiorArticleGroup)
        {
            SubordinateArticleGroupCreated?.Invoke(createdArticleGroup, superiorArticleGroup);
        }
        private void OnRootArticleGroupCreated(ArticleGroup createdArticleGroup)
        {
            RootArticleGroupCreated?.Invoke(createdArticleGroup);
        }
        private void OnRootArticleGroupDeleted(ArticleGroup articleGroup)
        {
            RootArticleGroupDeleted?.Invoke(articleGroup);
        }
        private void OnSubordinateArticleGroupDeleted(ArticleGroup articleGroup, ArticleGroup superiorArticleGroup)
        {
            SubordinateArticleGroupDeleted?.Invoke(articleGroup, superiorArticleGroup);
        }
        private ArticleGroup? GetSuperiorArticleGroupRecursive(ArticleGroup superiorArticleGroup, ArticleGroup articleGroup)
        {
            foreach (ArticleGroup a in superiorArticleGroup.SubordinateArticleGroups)
            {
                if (a.Id == articleGroup.Id)
                    return superiorArticleGroup;

                ArticleGroup? matchingSuperiorArticleGroup = GetSuperiorArticleGroupRecursive(a, articleGroup);

                if (matchingSuperiorArticleGroup != null)
                    return matchingSuperiorArticleGroup;
            }

            return null;
        }

        private void OnArticleCreated(Article article)
        {
            ArticleCreated?.Invoke(article);
        }
        private void OnArticleDeleted(Article article)
        {
            ArticleDeleted?.Invoke(article);
        }

        private async Task Inizialize()
        {
            IEnumerable<Customer> customers = await _manager.GetAllCustomers();
            _customers.Clear();
            _customers.AddRange(customers);

            IEnumerable<ArticleGroup> articleGroups = await _manager.GetAllArticleGroups();
            _articleGroups.Clear();
            _articleGroups.AddRange(articleGroups);

            IEnumerable<Article> articles = await _manager.GetAllArticlesAsync();
            _articles.Clear();
            _articles.AddRange(articles);
        }

        internal async Task SaveChangesToArticleAsync(Article initialArticle, Article editedArticle)
        {
            await _manager.SaveChangesToArticleAsync(initialArticle, editedArticle);
            int articleIndex = _articles.IndexOf(initialArticle);
            _articles[articleIndex] = editedArticle;
        }

        private Lazy<Task> _inizializeLazy;

        private readonly Manager _manager;
        private readonly List<Customer> _customers;
        private readonly List<ArticleGroup> _articleGroups;
        private readonly List<Article> _articles;
    }
}
