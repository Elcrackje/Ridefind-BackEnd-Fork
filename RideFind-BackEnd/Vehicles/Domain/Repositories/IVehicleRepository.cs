
using RideFind_BackEnd.Shared.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;

namespace RideFind_BackEnd.Vehicles.Domain.Repositories;

public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> FindByCategoryIdAsync(int categoryId);
}