namespace RideFind_BackEnd.Vehicles.Domain.Model.Commands;

public record CreateVehicleCommand(string VehicleName, string Description, int CategoryId);