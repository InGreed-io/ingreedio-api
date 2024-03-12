using InGreedIoApi.Model;
using InGreedIoApi.Model.Requests;

namespace InGreedIoApi.Services;

public interface IAuthenticationService
{
    public Task<AuthResult> Register(UserRegistrationRequest registrationRequest);
    public  Task<AuthResult> Login(UserLoginRequest loginRequest);
}