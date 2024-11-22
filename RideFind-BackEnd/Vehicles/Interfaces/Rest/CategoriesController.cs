using System.Net.Mime;
using RideFind_BackEnd.Vehicles.Domain.Model.Queries;
using RideFind_BackEnd.Vehicles.Domain.Services;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CategoriesController(
    ICategoryCommandServices categoryCommandServices,
    ICategoryQueryServices categoryQueryServices) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a category",
        Description = "Creates a category with a given name",
        OperationId = "CreateCategpry")]
    [SwaggerResponse(201, "The category was created", typeof(CategoryResource))]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryResource createCategoryResource)
    {
        var createCategoryCommand =
            CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(createCategoryResource);
        var category = await categoryCommandServices.Handle(createCategoryCommand);
        if (category is null) return BadRequest();
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = resource.Id }, resource);
    }

    [HttpGet("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Get a category by id",
        Description = "Gets a category for a given identifier",
        OperationId = "GetCategoryById")]
    [SwaggerResponse(200, "The category was found", typeof(CategoryResource))]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryServices.Handle(getCategoryByIdQuery);
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all categories",
        Description = "Gets all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(200, "The categories were found", typeof(IEnumerable<CategoryResource>))]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categories = await categoryQueryServices.Handle(getAllCategoriesQuery);
        var resources = categories.Select(CategoryResourceFromEntityAssembler
            .ToResourceFromEntity);
        return Ok(resources);
    }
}