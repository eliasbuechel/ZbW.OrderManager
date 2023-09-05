using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Customers.DTOs;

namespace BusinessLayer.Customers.Commands
{
    public class ImportCustomersCommand : BaseAsyncCommand
    {
        public ImportCustomersCommand(ManagerStore managerStore, IFileDialogue fileDialogue)
        {
            _managerStore = managerStore;
            _fileDialogue = fileDialogue;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            IEnumerable<CustomerDTO> customer = _managerStore.Customers;

            string filePath = _fileDialogue.OpenFileDialogue("Json files (*.json)|*.json|Xml files (*.xml)|*.xml");
        }

        private readonly ManagerStore _managerStore;
        private readonly IFileDialogue _fileDialogue;
    }
}