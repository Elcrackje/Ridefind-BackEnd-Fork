using RideFind_BackEnd.Shared.Domain.Repositories;

namespace RideFind_BackEnd.IAM;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);

    bool ExistsByUsername(string username);
}