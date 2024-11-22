
using RideFind_BackEnd.Shared.Infraestructure.Persistence.EFC.Repositories;
using RideFind_BackEnd.Shared.Interfaces.ASP.Configuration;
using RideFind_BackEnd.Vehicles.Domain.Model.Entities;
using RideFind_BackEnd.Vehicles.Domain.Repositories;

namespace RideFind_BackEnd.Vehicles.Infrastructure.Persistence.EFC.Repositories;

public class CategoryRepository(AppDbContext context) : BaseRepository<Category>(context), ICategoryRepository;