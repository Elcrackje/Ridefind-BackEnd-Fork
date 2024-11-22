using RideFind_BackEnd.Shared.Domain.Repositories;

namespace RideFind_BackEnd.Profiles;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddSync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error ocurred while creating the profile: {e.Message}");
            return null;
        }
    }
}