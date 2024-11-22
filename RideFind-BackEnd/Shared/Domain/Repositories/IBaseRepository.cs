namespace RideFind_BackEnd.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity>
{
    //CRUD
    //Insert
    Task AddSync(TEntity entity);

    //Get - Select
    Task<TEntity?> FindByIdAsync(int id);
    Task<IEnumerable<TEntity>> ListAsync();

    //Update
    void Update(TEntity entity);

    //Delete
    void Remove(TEntity entity);
}