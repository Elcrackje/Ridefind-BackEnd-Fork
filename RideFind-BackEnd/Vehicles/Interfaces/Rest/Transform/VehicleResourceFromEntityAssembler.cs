
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;
using Microsoft.OpenApi.Extensions;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;

public static class VehicleResourceFromEntityAssembler
{
    public static VehicleResource ToResourceFromEntity(Vehicle vehicle)
    {
        return new VehicleResource(
            vehicle.Id,
            vehicle.VehicleName,
            vehicle.Description,
            CategoryResourceFromEntityAssembler.ToResourceFromEntity(vehicle.Category),
            vehicle.Status.GetDisplayName());
    }
}