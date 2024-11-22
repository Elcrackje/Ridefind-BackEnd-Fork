

using RideFind_BackEnd.Vehicles.Domain.Model.Commands;
using RideFind_BackEnd.Vehicles.Interfaces.Rest.Resources;

namespace RideFind_BackEnd.Vehicles.Interfaces.Rest.Transform;

public class AddImageAssetToVehicleCommandFromResourceAssembler
{
    public static AddImageAssetToVehicleCommand ToCommandFromResource(
        AddImageAssetToTutorialResource addImageAssetToTutorialResource,
        int tutorialId)
    {
        return new AddImageAssetToVehicleCommand(addImageAssetToTutorialResource.ImageUrl, tutorialId);
    }
}