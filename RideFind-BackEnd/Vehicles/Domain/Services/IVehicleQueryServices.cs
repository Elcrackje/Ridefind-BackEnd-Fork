
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Domain.Model.Queries;

namespace RideFind_BackEnd.Vehicles.Domain.Services;

public interface IVehicleQueryServices
{
    Task<Vehicle?> Handle(GetVehicleByIdQuery query);

    Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query);

    Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesByCategoryIdQuery query);
}