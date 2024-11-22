namespace RideFind_BackEnd.Profiles;

public interface IProfilesContextFacade
{
    Task<int> CreateProfile(string firstName, string lastName, string email,
        string street,
        string number,
        string city,
        string postalCode,
        string country,
        string phoneNumber, string vehicleName);

    Task<int> FetchProfileIdByEmail(string email);
}