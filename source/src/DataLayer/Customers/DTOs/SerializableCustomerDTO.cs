using System.Xml.Serialization;

namespace DataLayer.Customers.DTOs
{
    [XmlRoot]
    public class SerializableCustomerDTO
    {
        public SerializableCustomerDTO()
        {

        }
        public SerializableCustomerDTO(int id, string firstName, string lastName, SerializableAddressDTO address, string emailAddress, string websiteURL, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            EmailAddress = emailAddress;
            WebsiteURL = websiteURL;
            Password = password;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SerializableAddressDTO Address { get; set; }
        public string EmailAddress { get; set; }
        public string WebsiteURL { get; set; }
        public string Password { get; set; }
    }
}
