namespace BusinessLayer.Base.ViewModels
{
    public interface IErrorInfo
    {
        bool HasErrorMessage { get; }
        string ErrorMessage { get; set; }
    }
}
