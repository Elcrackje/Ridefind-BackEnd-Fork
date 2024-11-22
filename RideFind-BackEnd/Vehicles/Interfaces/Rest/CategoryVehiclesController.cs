using System.Net.Mime;

using RideFind_BackEnd.Vehicles.Domain.Model.Queries;
using RideFind_BackEnd.Vehicles.Domain.Services;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest;

[ApiController]
[Route("/api/v1/categories/{categoryId}/tutorials")]
[Produces(MediaTypeNames.Application.Json)]
public class CategoryVehiclesController(IVehicleQueryServices vehicleQueryServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTutorialsByCategoryId([FromRoute] int categoryId)
    {
        var getAllTutorialsByCategoryIdQuery = new GetAllVehiclesByCategoryIdQuery(categoryId);
        var tutorials = await vehicleQueryServices.Handle(getAllTutorialsByCategoryIdQuery);
        var resource = tutorials.Select(VehicleResourceFromEntityAssembler
            .ToResourceFromEntity);
        return Ok(resource);
    }
}