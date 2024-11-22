namespace RideFind_BackEnd.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}