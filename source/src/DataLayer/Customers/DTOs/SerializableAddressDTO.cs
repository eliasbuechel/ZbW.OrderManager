using System.Xml.Serialization;

namespace DataLayer.Customers.DTOs
{
    [XmlRoot]
    public class SerializableAddressDTO
    {
        public SerializableAddressDTO()
        {
            
        }
        public SerializableAddressDTO(string streetName, string houseNumber, string city, string postalCode)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
        }

        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
