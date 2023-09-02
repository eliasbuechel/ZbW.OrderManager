namespace DataLayer.ArticleGroups.Validation
{
    public interface IArticleGroupValidator
    {
        bool Validate(IValidatableArticleGroup articleGroup);
        bool ValidateName(string name);
    }
}
