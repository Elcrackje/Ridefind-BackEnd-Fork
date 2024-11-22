
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Domain.Model.Queries;
using RideFind_BackEnd.Vehicles.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Services;

namespace RideFind_BackEnd.Vehicles.Application.Internal.QueryServices;

public class VehicleQueryService(IVehicleRepository vehicleRepository) : IVehicleQueryServices
{
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query)
    {
        return await vehicleRepository.FindByIdAsync(query.TutorialId);
    }

    public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query)
    {
        return await vehicleRepository.ListAsync();
    }

    public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesByCategoryIdQuery query)
    {
        return await vehicleRepository.FindByCategoryIdAsync(query.CategoryId);
    }
}