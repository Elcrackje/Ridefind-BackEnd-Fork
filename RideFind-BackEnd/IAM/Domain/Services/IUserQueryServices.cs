namespace RideFind_BackEnd.IAM;

public interface IUserQueryServices
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
}