using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Services.ArticleGroupCreators;
using DataLayer.ArticleGroups.Services.ArticleGroupDeletors;
using DataLayer.ArticleGroups.Services.ArticleGroupProviders;
using DataLayer.ArticleGroups.Services.ArticleGroupUpdator;

namespace BusinessLayer.ArticleGroups.Models
{
    public class ArticleGroupList
    {
        public ArticleGroupList(IArticleGroupProvider articleGroupProvider, IArticleGroupCreator articleGroupCreator, IArticleGroupDeletor articleGroupDeletor, IArticleGroupUpdator articleGroupUpdator)
        {
            _articleGroupProvider = articleGroupProvider;
            _articleGroupCreator = articleGroupCreator;
            _articleGroupDeletor = articleGroupDeletor;
            _articleGroupUpdator = articleGroupUpdator;
        }

        public async Task<IEnumerable<ArticleGroupDTO>> GetAllArticleGroups()
        {
            return await _articleGroupProvider.GetAllArticleGroups();
        }
        public async Task CreateArticleGroup(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup)
        {
            await _articleGroupCreator.CreateArticleGroup(creatingArticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _articleGroupCreator.GetNextFreeArticleGroupIdAsync();
        }
        public async Task DeleteArticleGroup(ArticleGroupDTO articleGroup)
        {
            await _articleGroupDeletor.DeleteArticleGroup(articleGroup);
        }

        public async Task UpdateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO articleGroup)
        {
            await _articleGroupUpdator.UpdateArticleGroupAsync(articleGroup);
        }

        private readonly IArticleGroupProvider _articleGroupProvider;
        private readonly IArticleGroupCreator _articleGroupCreator;
        private readonly IArticleGroupDeletor _articleGroupDeletor;
        private readonly IArticleGroupUpdator _articleGroupUpdator;
    }
}