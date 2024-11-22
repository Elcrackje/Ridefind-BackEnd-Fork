

using RideFind_BackEnd.Vehicles.Domain.Model.Entities;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;

public static class CategoryResourceFromEntityAssembler
{
    public static CategoryResource ToResourceFromEntity(Category entity)
    {
        return new CategoryResource(entity.Id, entity.Name);
    }
}