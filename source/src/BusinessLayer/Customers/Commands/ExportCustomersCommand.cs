using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Customers.DTOs;
using System.Text.Json;
using System.Xml.Serialization;

namespace BusinessLayer.Customers.Commands
{
    public class ExportCustomersCommand : BaseAsyncCommand
    {
        public ExportCustomersCommand(ManagerStore managerStore, IDialogService dialogService)
        {
            _managerStore = managerStore;
            _dialogService = dialogService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            IEnumerable<SerializableCustomerDTO> customers;

            try
            {
                customers = await _managerStore.GetAllSerializableCustomersAsync();
            }
            catch (Exception)
            {
                return;
            }

            string fileName = _dialogService.SaveFileDialog("Json files (*.json)|*.json|Xml files (*.xml)|*.xml");

            if (string.IsNullOrEmpty(fileName))
                return;

            if (File.Exists(fileName))
                File.Delete(fileName);

            using FileStream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);

            if (Path.GetExtension(fileName) == ".json")
                JsonSerializer.Serialize(stream, customers, customers.GetType(), JsonSerializerOptions.Default);
            else
                new XmlSerializer(customers.GetType()).Serialize(stream, customers);

            _dialogService.MessageBoxDialog("Export successfully created!");
        }

        private readonly ManagerStore _managerStore;
        private readonly IDialogService _dialogService;
    }
}