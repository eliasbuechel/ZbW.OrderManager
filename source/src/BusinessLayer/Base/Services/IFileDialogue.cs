namespace BusinessLayer.Base.Services
{
    public interface IFileDialogue
    {
        string OpenFileDialogue(string filter);
        public string SaveFileDialog(string filter);
    }
}
