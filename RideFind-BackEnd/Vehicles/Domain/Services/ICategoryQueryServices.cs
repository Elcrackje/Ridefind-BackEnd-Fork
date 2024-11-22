
using RideFind_BackEnd.Vehicles.Domain.Model.Entities;
using RideFind_BackEnd.Vehicles.Domain.Model.Queries;

namespace RideFind_BackEnd.Vehicles.Domain.Services;

public interface ICategoryQueryServices
{
    Task<Category?> Handle(GetCategoryByIdQuery query);

    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query);
}