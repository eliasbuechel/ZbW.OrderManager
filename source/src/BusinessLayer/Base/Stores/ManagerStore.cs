using BusinessLayer.Base.Models;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.Articles.DTOs;
using DataLayer.Customers.DTOs;
using DataLayer.Orders.DTOs;
using System.Net.Http.Headers;

namespace BusinessLayer.Base.Stores
{
    public class ManagerStore
    {
        public ManagerStore(Manager manager)
        {
            _manager = manager;
            _inizializeLazy = new Lazy<Task>(Inizialize);
        }

        public event Action<CustomerDTO>? CustomerCreated;
        public event Action<CustomerDTO>? CustomerDeleted;
        public event Action<ArticleGroupDTO>? RootArticleGroupCreated;
        public event Action<ArticleGroupDTO, ArticleGroupDTO>? SubordinateArticleGroupCreated;
        public event Action<ArticleGroupDTO>? RootArticleGroupDeleted;
        public event Action<ArticleGroupDTO, ArticleGroupDTO>? SubordinateArticleGroupDeleted;
        public event Action<ArticleDTO>? ArticleCreated;
        public event Action<ArticleDTO>? ArticleDeleted;
        public event Action<OrderDTO>? OrderCreated;
        public event Action<OrderDTO>? OrderDeleted;

        public IEnumerable<CustomerDTO> Customers => _customers;
        public IEnumerable<ArticleGroupDTO> ArticleGroups => _articleGroups;
        public IEnumerable<ArticleDTO> Articles => _articles;
        public IEnumerable<OrderDTO> Orders => _orders;

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

        public async Task<IEnumerable<SerializableCustomerDTO>> GetAllSerializableCustomersAsync()
        {
            return await _manager.GetAllSerializableCustomersAsync();
        }
        public async Task CreateCustomerAsync(CustomerDTO customer)
        {
            await _manager.CreateCustomerAsync(customer);
            _customers.Add(customer);
            OnCustomerCreated(customer);
        }
        public async Task<int> GetNextFreeCustomerIdAsync()
        {
            return await _manager.GetNextFreeCustomerIdAsync();
        }
        public async Task DeleteCustomerAsync(CustomerDTO customer)
        {
            await _manager.DeleteCustomerAsync(customer);
            _customers.Remove(customer);
            OnCustomerDeleted(customer);
        }
        public async Task EditCustomerAsync(CustomerDTO initialCustomer, CustomerDTO editedCustomer)
        {
            await _manager.EditCustomerAsync(initialCustomer, editedCustomer);
            int initialCustomerIndex = _customers.IndexOf(initialCustomer);
            _customers[initialCustomerIndex] = editedCustomer;
        }

        public async Task CreateRootArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO articleGroup)
        {
            await _manager.CreateArticleGroupAsync(articleGroup);

            ArticleGroupDTO createdArticleGroup = new ArticleGroupDTO(articleGroup.Id, articleGroup.Name);

            _articleGroups.Add(createdArticleGroup);
            OnRootArticleGroupCreated(createdArticleGroup);
        }
        public async Task CreateSubordinateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO articleGroup)
        {
            await _manager.CreateArticleGroupAsync(articleGroup);

            ArticleGroupDTO superiorAticleGroup = articleGroup.SuperiorArticleGroup ?? throw new ArgumentException("Cannot create subordinate article group without a superior article group!");
            ArticleGroupDTO createdArticleGroup = new ArticleGroupDTO(articleGroup.Id, articleGroup.Name);

            superiorAticleGroup.AddSubordinateArticleGroup(createdArticleGroup);
            OnSubordinateArticleGroupCreated(createdArticleGroup, superiorAticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _manager.GetNextFreeArticleGroupIdAsync();
        }
        public async Task DeleteArticleGroupAsync(ArticleGroupDTO articleGroup)
        {
            if (articleGroup.SubordinateArticleGroups.Count > 0)
                throw new DeletingNonLeaveArticleGroupException(articleGroup);

            try
            {
                await _manager.DeleteArticleGroupAsync(articleGroup);
            }
            catch
            {
                return;
            }

            if (articleGroup.SuperiorArticleGroup == null)
            {
                _articleGroups.Remove(articleGroup);
                OnRootArticleGroupDeleted(articleGroup);
                return;
            }

            ArticleGroupDTO superiorArticleGroup = articleGroup.SuperiorArticleGroup;
            articleGroup.SuperiorArticleGroup = null;
            OnSubordinateArticleGroupDeleted(articleGroup, superiorArticleGroup);
        }
        public async Task UpdateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO modifiedArticleGroup, ArticleGroupDTO initialArticleGroup)
        {
            try
            {
                await _manager.UpdateArticleGroupAsync(modifiedArticleGroup);
            }
            catch (NotContainingUpdatingArticleGroupInDatabaseException e)
            {
                throw new NotAffectedDataStorageException("Not able to update article group!", e);
            }
            catch (NoChangesMadeException<CreatedOrUpdatedArticleGroupDTO> e)
            {
                throw new NotAffectedDataStorageException("Not able to update article group!", e);
            }
            catch (InvalidDataException<CreatedOrUpdatedArticleGroupDTO> e)
            {
                throw new NotAffectedDataStorageException("Not able to update article group!", e);
            }
            catch (Exception e)
            {
                throw new NotAffectedDataStorageException("Not able to update article group!", e);
            }

            initialArticleGroup.Update(modifiedArticleGroup);

            if (modifiedArticleGroup.SuperiorArticleGroup == null)
                _articleGroups.Add(initialArticleGroup);
            else
            {
                foreach (ArticleGroupDTO a in _articleGroups)
                {
                    if (a.Id == initialArticleGroup.Id)
                    {
                        _articleGroups.Remove(a);
                        break;
                    }
                }
            }
        }

        public async Task CreateArticleAsync(ArticleDTO article)
        {
            await _manager.CreateArticleAsync(article);
            _articles.Add(article);
            OnArticleCreated(article);
        }
        public async Task<int> GetNextFreeArticleIdAsync()
        {
            return await _manager.GetNextFreeArticleIdAsync();
        }
        public async Task DeleteArticleAsync(ArticleDTO article)
        {
            await _manager.DeleteArticleAsync(article);
            _articles.Remove(article);
            OnArticleDeleted(article);
        }
        public async Task SaveChangesToArticleAsync(ArticleDTO initialArticle, ArticleDTO editedArticle)
        {
            await _manager.SaveChangesToArticleAsync(initialArticle, editedArticle);
            int articleIndex = _articles.IndexOf(initialArticle);
            _articles[articleIndex] = editedArticle;
        }

        public async Task CreateOrderAsync(CreatingOrderDTO order)
        {
            OrderDTO orderDTO = await _manager.CreateOrderAsync(order);
            OnOrderCreated(orderDTO);
        }
        public async Task DeleteOrderAsync(OrderDTO order)
        {
            await _manager.DeleteOrderAsync(order);
            OnOrderDeleted(order);
        }

        private void OnCustomerCreated(CustomerDTO customer)
        {
            CustomerCreated?.Invoke(customer);
        }
        private void OnCustomerDeleted(CustomerDTO customer)
        {
            CustomerDeleted?.Invoke(customer);
        }
        private void OnSubordinateArticleGroupCreated(ArticleGroupDTO createdArticleGroup, ArticleGroupDTO superiorArticleGroup)
        {
            SubordinateArticleGroupCreated?.Invoke(createdArticleGroup, superiorArticleGroup);
        }
        private void OnRootArticleGroupCreated(ArticleGroupDTO createdArticleGroup)
        {
            RootArticleGroupCreated?.Invoke(createdArticleGroup);
        }
        private void OnRootArticleGroupDeleted(ArticleGroupDTO articleGroup)
        {
            RootArticleGroupDeleted?.Invoke(articleGroup);
        }
        private void OnSubordinateArticleGroupDeleted(ArticleGroupDTO articleGroup, ArticleGroupDTO superiorArticleGroup)
        {
            SubordinateArticleGroupDeleted?.Invoke(articleGroup, superiorArticleGroup);
        }
        private void OnArticleCreated(ArticleDTO article)
        {
            ArticleCreated?.Invoke(article);
        }
        private void OnArticleDeleted(ArticleDTO article)
        {
            ArticleDeleted?.Invoke(article);
        }
        private void OnOrderCreated(OrderDTO order)
        {
            OrderCreated?.Invoke(order);
        }
        private void OnOrderDeleted(OrderDTO order)
        {
            OrderDeleted?.Invoke(order);
        }
        private async Task Inizialize()
        {
            IEnumerable<CustomerDTO> customers = await _manager.GetAllCustomersAsync();
            _customers.Clear();
            _customers.AddRange(customers);

            IEnumerable<ArticleGroupDTO> articleGroups = await _manager.GetAllArticleGroupsAsync();
            _articleGroups.Clear();
            _articleGroups.AddRange(articleGroups);

            IEnumerable<ArticleDTO> articles = await _manager.GetAllArticlesAsync();
            _articles.Clear();
            _articles.AddRange(articles);

            IEnumerable<OrderDTO> orders = await _manager.GetAllOrdersAsync();
            _orders.Clear();
            _orders.AddRange(orders);
        }


        private readonly Manager _manager;
        private Lazy<Task> _inizializeLazy;
        private readonly List<CustomerDTO> _customers = new List<CustomerDTO>();
        private readonly List<ArticleGroupDTO> _articleGroups = new List<ArticleGroupDTO>();
        private readonly List<ArticleDTO> _articles = new List<ArticleDTO>();
        private readonly List<OrderDTO> _orders = new List<OrderDTO>();
    }
}