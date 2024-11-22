
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Domain.Model.Commands;

namespace RideFind_BackEnd.Vehicles.Domain.Services;

public interface IVehicleCommandServices
{
    Task<Vehicle?> Handle(AddImageAssetToVehicleCommand command);

    Task<Vehicle?> Handle(CreateVehicleCommand command);
}