using BusinessLayer.Base.Services;
using Microsoft.Win32;
using System.Windows;

namespace PresentationLayer.Base.Services
{
    public class DialogService : IDialogService
    {
        public string OpenFileDialogue(string filter)
        {
            OpenFileDialog openFileDialogue = new OpenFileDialog();
            openFileDialogue.Filter = filter;
            openFileDialogue.Multiselect = false;
            openFileDialogue.ShowDialog();

            return openFileDialogue.FileName;
        }

        public string SaveFileDialog(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            saveFileDialog.ShowDialog();

            return saveFileDialog.FileName;
        }

        public void MessageBoxDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
