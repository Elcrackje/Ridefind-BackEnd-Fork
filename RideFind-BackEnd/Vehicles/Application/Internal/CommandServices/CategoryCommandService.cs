
using RideFind_BackEnd.Shared.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Model.Commands;
using RideFind_BackEnd.Vehicles.Domain.Model.Entities;
using RideFind_BackEnd.Vehicles.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Services;

namespace RideFind_BackEnd.Vehicles.Application.Internal.CommandServices;

public class CategoryCommandService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : ICategoryCommandServices
{
    /**
     * Create Category Command Handler
     * <summary>
     *     This method is responable for handling the command and executing the business logic for creating a category.
     *     It is also responsible for calling the repository to persist the data.
     * </summary>
     * <param name="command"> the command containing the name for the category</param>
     * <returns>The category entity</returns>
     */
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command.Name);
        await categoryRepository.AddSync(category);
        await unitOfWork.CompleteAsync();
        return category;
    }
}