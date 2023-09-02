using DataLayer.Base.DataValidators;

namespace DataLayer.ArticleGroups.Validation
{
    public class ArticleGroupValidator : IArticleGroupValidator
    {
        public bool Validate(IValidatableArticleGroup articleGroup)
        {
            return ValidateName(articleGroup.Name);
        }

        public bool ValidateName(string name)
        {
            return StringValidator.Validate(name, NAME_VALIDATION_PATTERN);
        }

        private const string NAME_VALIDATION_PATTERN = "/^[^\\s].{0,39}$";
    }
}