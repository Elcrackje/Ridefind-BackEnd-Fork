namespace RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;

public record VehicleResource(
    int Id,
    string VehicleName,
    string Description,
    CategoryResource Category,
    string Status);