using DataLayer.ArticleGroups.Models;
using DataLayer.ArticleGroups.Services.ArticleGroupCreators;
using DataLayer.ArticleGroups.Services.ArticleGroupDeletors;
using DataLayer.ArticleGroups.Services.ArticleGroupProviders;

namespace BusinessLayer.ArticleGroups.Models
{
    public class ArticleGroupList
    {
        public ArticleGroupList(IArticleGroupProvider articleGroupProvider, IArticleGroupCreator articleGroupCreator, IArticleGroupDeletor articleGroupDeletor)
        {
            _articleGroupProvider = articleGroupProvider;
            _articleGroupCreator = articleGroupCreator;
            _articleGroupDeletor = articleGroupDeletor;
        }

        public async Task<IEnumerable<ArticleGroup>> GetAllArticleGroups()
        {
            return await _articleGroupProvider.GetAllArticleGroups();
        }
        public async Task CreateArticleGroup(CreatingArticleGroup creatingArticleGroup)
        {
            await _articleGroupCreator.CreateArticleGroup(creatingArticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _articleGroupCreator.GetNextFreeArticleGroupIdAsync();
        }
        public async Task DeleteArticleGroup(ArticleGroup articleGroup)
        {
            await _articleGroupDeletor.DeleteArticleGroup(articleGroup);
        }

        private readonly IArticleGroupProvider _articleGroupProvider;
        private readonly IArticleGroupCreator _articleGroupCreator;
        private readonly IArticleGroupDeletor _articleGroupDeletor;
    }
}