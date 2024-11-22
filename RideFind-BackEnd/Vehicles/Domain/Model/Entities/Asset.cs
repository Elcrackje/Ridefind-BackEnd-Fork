
using RideFind_BackEnd.Vehicles.Domain.Model.ValueObjects;

namespace RideFind_BackEnd.Vehicles.Domain.Model.Entities;

public partial class Asset : IPublishable
{
    public Asset(EAssetType type)
    {
        Type = type;
        Status = EPublishingStatus.Disponible;
        AssetIdentifier = new AcmeAssetIdentifier();
    }

    public int Id { get; }

    public AcmeAssetIdentifier AssetIdentifier { get; private set; }

    public EPublishingStatus Status { get; protected set; }

    public EAssetType Type { get; private set; }

    public virtual bool Readable => false;

    public virtual bool Viewable => false;

    public void SendToDisponible()
    {
        Status = EPublishingStatus.Disponible;
    }

    public void SendToOccupied()
    {
        Status = EPublishingStatus.Ocupado;
    }


    public virtual object GetContent()
    {
        return string.Empty;
    }
}