
using RideFind_BackEnd.Shared.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;
using RideFind_BackEnd.Vehicles.Domain.Model.Commands;
using RideFind_BackEnd.Vehicles.Domain.Repositories;
using RideFind_BackEnd.Vehicles.Domain.Services;

namespace RideFind_BackEnd.Vehicles.Application.Internal.CommandServices;

public class VehicleCommandService(
    IVehicleRepository vehicleRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IVehicleCommandServices
{
    public async Task<Vehicle?> Handle(AddImageAssetToVehicleCommand command)
    {
        var tutorial = await vehicleRepository.FindByIdAsync(command.TutorialId);
        if (tutorial is null) throw new Exception("Tutorial not found");
        tutorial.AddImage(command.ImageUrl);
        await unitOfWork.CompleteAsync();
        return tutorial;
    }

    public async Task<Vehicle?> Handle(CreateVehicleCommand command)
    {
        var tutorial = new Vehicle(command.VehicleName, command.Description, command.CategoryId);
        await vehicleRepository.AddSync(tutorial);
        await unitOfWork.CompleteAsync();
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        tutorial.Category = category;
        return tutorial;
    }
}