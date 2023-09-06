namespace BusinessLayer.Base.Services
{
    public interface IDialogService
    {
        string OpenFileDialogue(string filter);
        public string SaveFileDialog(string filter);
        public void MessageBoxDialog(string message);
    }
}
