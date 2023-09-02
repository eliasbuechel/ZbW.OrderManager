namespace DataLayer.Articles.Validation
{
    public interface IArticleValidator
    {
        bool Validate(IValidatableArticle article);
        bool ValidateName(string name);
    }
}
