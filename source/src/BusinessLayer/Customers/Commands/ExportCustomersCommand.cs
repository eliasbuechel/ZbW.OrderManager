using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Customers.DTOs;
using System.Text.Json;

namespace BusinessLayer.Customers.Commands
{
    public class ExportCustomersCommand : BaseAsyncCommand
    {
        public ExportCustomersCommand(ManagerStore managerStore, IFileDialogue fileDialogue)
        {
            _managerStore = managerStore;
            _fileDialogue = fileDialogue;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            IEnumerable<CustomerDTO> customers = _managerStore.Customers;

            string fileName = _fileDialogue.SaveFileDialog("Json files (*.json)|*.json|Xml files (*.xml)|*.xml");

            if (File.Exists(fileName))
                File.Delete(fileName);

            using FileStream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);

            JsonSerializer.Serialize(stream, customers, customers.GetType(), JsonSerializerOptions.Default);
        }

        private readonly ManagerStore _managerStore;
        private readonly IFileDialogue _fileDialogue;
    }
}
