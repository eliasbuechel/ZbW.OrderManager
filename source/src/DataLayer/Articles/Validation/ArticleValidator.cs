using DataLayer.Base.DataValidators;

namespace DataLayer.Articles.Validation
{
    public class ArticleValidator : IArticleValidator
    {
        public bool Validate(IValidatableArticle article)
        {
            return ValidateName(article.Name);
        }
        public bool ValidateName(string name)
        {
            return StringValidator.Validate(name, NAME_VALIDATION_PATTERN);
        }

        private const string NAME_VALIDATION_PATTERN = @"^.+$";
    }
}
