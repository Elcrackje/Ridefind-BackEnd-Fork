
using RideFind_BackEnd.Vehicles.Domain.Model.Commands;
using RideFind_BackEnd.Vehicles.Domain.Model.Entities;

namespace RideFind_BackEnd.Vehicles.Domain.Services;

public interface ICategoryCommandServices
{
    public Task<Category?> Handle(CreateCategoryCommand command);
}