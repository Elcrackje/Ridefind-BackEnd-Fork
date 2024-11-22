
using RideFind_BackEnd.Vehicles.Domain.Model.ValueObjects;

namespace RideFind_BackEnd.Vehicles.Domain.Model.Entities;

public class ImageAsset : Asset
{
    public ImageAsset() : base(EAssetType.Image)
    {
        ImageUri = null;
    }

    public ImageAsset(string imageUrl) : base(EAssetType.Image)
    {
        ImageUri = new Uri(imageUrl);
    }

    public Uri? ImageUri { get; }
    public override bool Readable => false;
    public override bool Viewable => true;

    public override string GetContent()
    {
        return ImageUri != null ? ImageUri.AbsoluteUri : string.Empty;
    }
}