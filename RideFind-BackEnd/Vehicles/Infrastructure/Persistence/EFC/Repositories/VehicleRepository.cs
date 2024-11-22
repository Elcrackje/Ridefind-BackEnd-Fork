
using RideFind_BackEnd.Shared.Infraestructure.Persistence.EFC.Repositories;
using RideFind_BackEnd.Shared.Interfaces.ASP.Configuration;
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RideFind_BackEnd.Vehicles.Infrastructure.Persistence.EFC.Repositories;

public class VehicleRepository(AppDbContext context) : BaseRepository<Vehicle>(context), IVehicleRepository
{
    public new async Task<Vehicle?> FindByIdAsync(int id)
    {
        return await Context.Set<Vehicle>().Include(t => t.Category)
            .Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Vehicle>> FindByCategoryIdAsync(int categoryId)
    {
        return await Context.Set<Vehicle>()
            .Include(tutorial => tutorial.Category)
            .Where(tutorial => tutorial.CategoryId == categoryId)
            .ToListAsync();
    }
}