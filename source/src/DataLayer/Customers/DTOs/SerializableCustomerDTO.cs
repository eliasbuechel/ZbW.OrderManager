using System.Xml.Serialization;

namespace DataLayer.Customers.DTOs
{
    [XmlRoot]
    public class SerializableCustomerDTO
    {
        public SerializableCustomerDTO()
        {}
        public SerializableCustomerDTO(string customerNr, string firstName, string lastName, SerializableAddressDTO address, string emailAddress, string websiteURL, string hasedPassword)
        {
            CustomerNr = customerNr;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            EmailAddress = emailAddress;
            WebsiteURL = websiteURL;
            HashedPassword = hasedPassword;
        }

        public string CustomerNr { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public SerializableAddressDTO Address { get; set; } = new SerializableAddressDTO();
        public string EmailAddress { get; set; } = string.Empty;
        public string WebsiteURL { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}
