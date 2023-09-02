using DataLayer.ArticleGroups.DTOs;
using DataLayer.ArticleGroups.Exceptions;
using DataLayer.ArticleGroups.Models;
using DataLayer.ArticleGroups.Validation;
using DataLayer.Base.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ArticleGroups.Services.ArticleGroupUpdator
{
    public class DatabaseArticleGroupUpdator : IArticleGroupUpdator
    {
        public DatabaseArticleGroupUpdator(ManagerDbContextFactory managerDbContextFactory, IArticleGroupValidator articleGroupValidator)
        {
            _managerDbContextFactory = managerDbContextFactory;
            _articleGroupValidator = articleGroupValidator;
        }

        public async Task UpdateArticleGroupAsync(CreatedOrUpdatedArticleGroupDTO updatingArticleGroup)
        {
            ManagerDbContext context = _managerDbContextFactory.CreateDbContext();

            ValidateArticleGroup(updatingArticleGroup);

            ArticleGroup? databseArticleGroup =
                await context.ArticleGroups
                    .Where(a => a.Id == updatingArticleGroup.Id)
                    .Include(a => a.SuperiorArticleGroup)
                    .FirstOrDefaultAsync()
                ?? throw new NotContainingUpdatingArticleGroupInDatabaseException("Not containing the updating article group in the database.", updatingArticleGroup);

            if (!HasDataChanged(databseArticleGroup, updatingArticleGroup))
                throw new NoChangesMadeException<CreatedOrUpdatedArticleGroupDTO>(updatingArticleGroup);

            UpdateArticleGroupData(databseArticleGroup, updatingArticleGroup, context);

            await context.SaveChangesAsync();
        }

        private void ValidateArticleGroup(CreatedOrUpdatedArticleGroupDTO updatingArticleGroup)
        {
            if (!_articleGroupValidator.Validate(updatingArticleGroup))
                throw new ValidationException();
        }
        private static bool HasDataChanged(ArticleGroup databaseArticleGroup, CreatedOrUpdatedArticleGroupDTO updatedArticleGroup)
        {
            if (databaseArticleGroup.Name != updatedArticleGroup.Name)
                return true;

            if (databaseArticleGroup.SuperiorArticleGroup == null)
            {
                if (updatedArticleGroup.SuperiorArticleGroup == null)
                    return false;

                return true;
            }

            if (updatedArticleGroup.SuperiorArticleGroup == null)
                return true;

            if (databaseArticleGroup.SuperiorArticleGroup.Id == updatedArticleGroup.SuperiorArticleGroup.Id)
                return false;

            return true;
        }
        private static void UpdateArticleGroupData(ArticleGroup databaseArticleGroup, CreatedOrUpdatedArticleGroupDTO updatingArticleGroup, ManagerDbContext context)
        {
            const int MAX_NAME_LENGHT = 100;

            string name = updatingArticleGroup.Name;

            if (string.IsNullOrEmpty(name))
                throw new InvalidDataException<CreatedOrUpdatedArticleGroupDTO>($"Data property {nameof(CreatedOrUpdatedArticleGroupDTO.Name)} cannot be null or empty!", updatingArticleGroup);

            if (updatingArticleGroup.Name.Length > MAX_NAME_LENGHT)
                throw new InvalidDataException<CreatedOrUpdatedArticleGroupDTO>($"Data property {nameof(CreatedOrUpdatedArticleGroupDTO.Name)} cannot larger than {MAX_NAME_LENGHT} caracters!", updatingArticleGroup);


            databaseArticleGroup.Name = updatingArticleGroup.Name;

            if (updatingArticleGroup.SuperiorArticleGroup == null)
            {
                databaseArticleGroup.SuperiorArticleGroup = null;
                return;
            }

            ArticleGroup? superiorArticleGroup =
                context.ArticleGroups
                    .Where(a => a.Id == updatingArticleGroup.SuperiorArticleGroup.Id)
                    .FirstOrDefault()
                    ?? throw new NotContainingArticleGroupInDatabaseException("Refered superior article group not found in database!", updatingArticleGroup.SuperiorArticleGroup);

            databaseArticleGroup.SuperiorArticleGroup = superiorArticleGroup;
        }

        private readonly ManagerDbContextFactory _managerDbContextFactory;
        private readonly IArticleGroupValidator _articleGroupValidator;
    }
}
