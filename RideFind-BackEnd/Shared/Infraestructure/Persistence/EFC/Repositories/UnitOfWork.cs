using RideFind_BackEnd.Shared.Domain.Repositories;
using RideFind_BackEnd.Shared.Interfaces.ASP.Configuration;

namespace RideFind_BackEnd.Shared.Infraestructure.Persistence.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}