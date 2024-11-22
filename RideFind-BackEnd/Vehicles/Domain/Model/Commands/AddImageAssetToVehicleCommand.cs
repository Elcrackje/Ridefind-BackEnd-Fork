namespace RideFind_BackEnd.Vehicles.Domain.Model.Commands;

public record AddImageAssetToVehicleCommand(
    string ImageUrl,
    int TutorialId);