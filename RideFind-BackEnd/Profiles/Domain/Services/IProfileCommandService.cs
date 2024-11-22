namespace RideFind_BackEnd.Profiles;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
}