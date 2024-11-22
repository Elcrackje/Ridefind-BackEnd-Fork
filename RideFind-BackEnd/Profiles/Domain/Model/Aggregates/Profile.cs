namespace RideFind_BackEnd.Profiles;

public partial class Profile
{
    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Address = new StreetAddress();
        phoneNumber = string.Empty;
        vehicleName = string.Empty;
    }

    public Profile(string firstName, string lastName, string email,
        string street,
        string number,
        string city,
        string postalCode,
        string country, string phoneNumber, string vehicleName)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Address = new StreetAddress(street, number, city, postalCode, country);
        this.phoneNumber = phoneNumber;
        this.vehicleName = vehicleName;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.firstName, command.lastName);
        Email = new EmailAddress(command.email);
        Address = new StreetAddress(command.street, command.number, command.city,
            command.postalCode, command.country);
        phoneNumber = command.phoneNumber;
        vehicleName = command.vehicleName;
    }

    public int Id { get; }
    public PersonName Name { get; }
    public EmailAddress Email { get; }
    public StreetAddress Address { get; }

    public string phoneNumber { get; private set; }

    public string vehicleName { get; private set; }

    public string FullName => Name.FullName;
    public string EmailAddress => Email.Address;
    public string StreetAddress => Address.FullAddress;
}