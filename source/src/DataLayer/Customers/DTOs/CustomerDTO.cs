namespace DataLayer.Customers.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string StreetName { get; }
        public string HouseNumber { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string EmailAddress { get; }
        public string WebsiteURL { get; }
        public string Password { get; }

        public CustomerDTO(int id, string firstName, string lastName, string streetName, string houseNumber, string city, string postalCode, string emailAddress, string websiteURL, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            StreetName = streetName;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            WebsiteURL = websiteURL;
            Password = password;
        }
    }
}
