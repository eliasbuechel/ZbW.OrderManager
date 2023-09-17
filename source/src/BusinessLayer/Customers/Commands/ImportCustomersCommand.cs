using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Exceptions;
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

            int addedCustomersCount = 0;

            List<string> errors = new List<string>();

            foreach (var c in customers)
            {
                try
                {
                    CustomerDTO customer = new CustomerDTO(
                    await _managerStore.GetNextFreeCustomerIdAsync(),
                    c.CustomerNr,
                    c.FirstName,
                    c.LastName,
                    c.Address.StreetName,
                    c.Address.HouseNumber,
                    c.Address.City,
                    c.Address.PostalCode,
                    c.EmailAddress,
                    c.WebsiteURL,
                    c.HashedPassword
                    );

                    await _managerStore.CreateCustomerAsync(customer);
                }
                catch (Exception e)
                {
                    errors.Add(e.Message);
                    continue;
                }

                addedCustomersCount++;
            }

            string message = $"Successfully added {addedCustomersCount}/{customers.Count()} customers! Additional info:{Environment.NewLine}";

            foreach (var error in errors)
            {
                message += error + Environment.NewLine;
            }

            _dialogService.MessageBoxDialog(message);
        }

        private readonly ManagerStore _managerStore;
        private readonly IDialogService _dialogService;
    }
}