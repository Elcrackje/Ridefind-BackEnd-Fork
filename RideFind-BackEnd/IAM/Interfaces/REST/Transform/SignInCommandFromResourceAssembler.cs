using RideFind_BackEnd.IAM.Interfaces.Resources;

namespace RideFind_BackEnd.IAM.Interfaces.Transform;

public class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}