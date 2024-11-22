
using RideFind_BackEnd.Vehicles.Domain.Model.Entities;
using RideFind_BackEnd.Vehicles.Domain.Model.ValueObjects;

namespace RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;

public partial class Vehicle : IPublishable
{
    public Vehicle()
    {
        VehicleName = string.Empty;
        Description = string.Empty;
        Assets = new List<Asset>();
        Status = EPublishingStatus.Disponible;
    }

    public ICollection<Asset> Assets { get; }
    public EPublishingStatus Status { get; protected set; }

    public bool Readable => HasReadableAssets;
    public bool Viewable => HasViewableAssets;

    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);
    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);

    public void SendToDisponible()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.Disponible))
            Status = EPublishingStatus.Disponible;
    }

    public void SendToOccupied()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.Ocupado))
            Status = EPublishingStatus.Ocupado;
    }

    private bool HasAllAssetsWithStatus(EPublishingStatus status)
    {
        return Assets.All(asset => asset.Status == status);
    }


    public List<ContentItem> GetContent()
    {
        var content = new List<ContentItem>();
        if (Assets.Count > 0)
            content.AddRange(Assets.Select(
                asset => new ContentItem(asset.Type.ToString(),
                    asset.GetContent() as string ?? string.Empty)));
        return content;
    }

    public void AddImage(string imageUrl)
    {
        if (ExistsImageByUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }

    private bool ExistsImageByUrl(string imageUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Image
                                   && (string)asset.GetContent() == imageUrl);
    }


    public void RemoveAsset(AcmeAssetIdentifier identifier)
    {
        var asset = Assets.FirstOrDefault(a => a.AssetIdentifier == identifier);
        if (asset != null) Assets.Remove(asset);
    }

    public void ClearAsseet()
    {
        Assets.Clear();
    }
}