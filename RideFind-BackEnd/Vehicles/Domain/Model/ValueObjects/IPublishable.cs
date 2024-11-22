namespace RideFind_BackEnd.Vehicles.Domain.Model.ValueObjects;

public interface IPublishable
{
    void SendToOccupied();

    void SendToDisponible();
}