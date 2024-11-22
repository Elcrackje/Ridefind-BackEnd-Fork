namespace RideFind_BackEnd.Profiles;

public record ProfileResource(
    int Id,
    string FullName,
    string Email,
    string StreetAddress,
    string phoneNumber,
    string vehicleName);