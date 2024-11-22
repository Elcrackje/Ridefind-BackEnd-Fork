namespace RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;

public record CreateTutorialResource(
    string VehicleName,
    string Description,
    int CategoryId);