namespace RideFind_BackEnd.Vehicles.Domain.Model.Entities;

public record AcmeAssetIdentifier(Guid Identifier)
{
    public AcmeAssetIdentifier() : this(Guid.NewGuid())
    {
    }
}