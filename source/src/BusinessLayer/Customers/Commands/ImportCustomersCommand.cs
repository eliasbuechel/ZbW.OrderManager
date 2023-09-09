using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Base.Stores;
using DataLayer.Customers.DTOs;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessLayer.Customers.Commands
{
    public class ImportCustomersCommand : BaseAsyncCommand
    {
        public ImportCustomersCommand(ManagerStore managerStore, IDialogService dialogService)
        {
            _managerStore = managerStore;
            _dialogService = dialogService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            string filePath = _dialogService.OpenFileDialogue("Json files (*.json)|*.json|Xml files (*.xml)|*.xml");

            if (string.IsNullOrEmpty(filePath))
                return;

            if (!File.Exists(filePath))
            {
                _dialogService.MessageBoxDialog($"Not able to find file:'{filePath}'!");
                return;
            }

            using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IEnumerable<SerializableCustomerDTO>? customers;

            try
            {
                if (Path.GetExtension(filePath) == ".json")
                {
                    using StreamReader reader = new StreamReader(stream);
                    string serializedCustomers = reader.ReadToEnd();
                    customers = JsonSerializer.Deserialize<IEnumerable<SerializableCustomerDTO>>(serializedCustomers);
                }
                else
                {
                    using XmlReader xmlReader = XmlReader.Create(stream);
                    customers = (IEnumerable<SerializableCustomerDTO>?) new XmlSerializer(typeof(List<SerializableCustomerDTO>)).Deserialize(xmlReader);
                }
            }
            catch (Exception e)
            {
                _dialogService.MessageBoxDialog($"Not able to deserialize customers! More info: {e.Message}");
                return;
            }

            if (customers == null)
            {
                _dialogService.MessageBoxDialog($"No data found in file!");
                return;
            }

            IEnumerable<CustomerDTO> addableCustomers = customers.Select(c => new CustomerDTO(
                c.Id,
                c.FirstName,
                c.LastName,
                c.Address.StreetName,
                c.Address.HouseNumber,
                c.Address.City,
                c.Address.PostalCode,
                c.EmailAddress,
                c.WebsiteURL,
                c.Password
                ));

            int addedCustomersCount = 0;

            foreach (var c in addableCustomers)
            {
                try
                {
                    await _managerStore.CreateCustomerAsync(c);
                }
                catch (Exception)
                {
                    continue;
                }

                addedCustomersCount++;
            }

            _dialogService.MessageBoxDialog($"Successfully added {addedCustomersCount}/{addableCustomers.Count()} customers!");
        }

        private readonly ManagerStore _managerStore;
        private readonly IDialogService _dialogService;
    }
}