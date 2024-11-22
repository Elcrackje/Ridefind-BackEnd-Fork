
using RideFind_BackEnd.Vehicles.Domain.Model.Queries;
using RideFind_BackEnd.Vehicles.Domain.Services;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
public class VehiclesController(
    IVehicleCommandServices vehicleCommandServices,
    IVehicleQueryServices vehicleQueryServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTutorial([FromBody] CreateTutorialResource createTutorialResource)
    {
        var createTutorialCommand =
            CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(createTutorialResource);
        var tutorial = await vehicleCommandServices.Handle(createTutorialCommand);
        if (tutorial is null) return BadRequest();
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return CreatedAtAction(nameof(GetTutorialById), new { tutorialId = resource.Id }, resource);
    }

    [HttpGet("{tutorialId:int}")]
    public async Task<IActionResult> GetTutorialById([FromRoute] int tutorialId)
    {
        var tutorial = await vehicleQueryServices.Handle(new GetVehicleByIdQuery(tutorialId));
        if (tutorial is null) return BadRequest();
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTutorials()
    {
        var getAllTutorialsQuery = new GetAllVehiclesQuery();
        var tutorials = await vehicleQueryServices.Handle(getAllTutorialsQuery);
        var resources = tutorials.Select(VehicleResourceFromEntityAssembler
            .ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost("{tutorialId}/ImageUrl")]
    public async Task<IActionResult> AddImageToTutorial([FromBody] AddImageAssetToTutorialResource
        addImageAssetToTutorialResource, [FromRoute] int tutorialId)
    {
        var addImageAssetToTutorialCommand =
            AddImageAssetToVehicleCommandFromResourceAssembler.ToCommandFromResource(addImageAssetToTutorialResource,
                tutorialId);
        var tutorial = await vehicleCommandServices.Handle(addImageAssetToTutorialCommand);
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return CreatedAtAction(nameof(GetTutorialById), new { tutorialId = resource.Id },
            resource);
    }
}