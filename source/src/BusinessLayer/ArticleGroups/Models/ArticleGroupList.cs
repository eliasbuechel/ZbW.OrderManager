using BusinessLayer.Base.Exceptions;
using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Services.ArticleGroupCreators;
using DataLayer.ArticleGroups.Services.ArticleGroupDeletors;
using DataLayer.ArticleGroups.Services.ArticleGroupProviders;
using DataLayer.ArticleGroups.Services.ArticleGroupUpdator;
using DataLayer.ArticleGroups.Validation;

namespace BusinessLayer.ArticleGroups.Models
{
    public class ArticleGroupList
    {
        public ArticleGroupList(IArticleGroupProvider articleGroupProvider, IArticleGroupCreator articleGroupCreator, IArticleGroupDeletor articleGroupDeletor, IArticleGroupUpdator articleGroupUpdator, IArticleGroupValidator articleGroupValidator)
        {
            _articleGroupProvider = articleGroupProvider;
            _articleGroupCreator = articleGroupCreator;
            _articleGroupDeletor = articleGroupDeletor;
            _articleGroupUpdator = articleGroupUpdator;
            _articleGroupValidator = articleGroupValidator;
        }

        public async Task<IEnumerable<ArticleGroupDTO>> GetAllArticleGroupsAsync()
        {
            return await _articleGroupProvider.GetAllArticleGroupsAsync();
        }
        public async Task CreateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup)
        {
            ValidateArticleGroup(creatingArticleGroup);
            await _articleGroupCreator.CreateArticleGroupAsync(creatingArticleGroup);
        }
        public async Task<int> GetNextFreeArticleGroupIdAsync()
        {
            return await _articleGroupCreator.GetNextFreeArticleGroupIdAsync();
        }
        public async Task DeleteArticleGroupAsync(ArticleGroupDTO articleGroup)
        {
            await _articleGroupDeletor.DeleteArticleGroupAsync(articleGroup);
        }
        public async Task UpdateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO articleGroup)
        {
            ValidateArticleGroup(articleGroup);
            await _articleGroupUpdator.UpdateArticleGroupAsync(articleGroup);
        }

        private void ValidateArticleGroup(CreatedOrUpdatedArticleGroupDTO creatingArticleGroup)
        {
            
            if (!_articleGroupValidator.Validate(creatingArticleGroup))
                throw new ValidationErrorException();
        }

        private readonly IArticleGroupProvider _articleGroupProvider;
        private readonly IArticleGroupCreator _articleGroupCreator;
        private readonly IArticleGroupDeletor _articleGroupDeletor;
        private readonly IArticleGroupUpdator _articleGroupUpdator;
        private readonly IArticleGroupValidator _articleGroupValidator;
    }
}