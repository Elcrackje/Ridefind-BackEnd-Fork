
using RideFind_BackEnd.Vehicles.Domain.Model.Commands;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;

public static class CreateVehicleCommandFromResourceAssembler
{
    public static CreateVehicleCommand ToCommandFromResource(CreateTutorialResource resource)
    {
        return new CreateVehicleCommand(resource.VehicleName, resource.Description, resource.CategoryId);
    }
}