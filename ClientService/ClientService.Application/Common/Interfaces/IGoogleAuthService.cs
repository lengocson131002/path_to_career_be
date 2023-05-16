using Google.Apis.Auth;

namespace ClientService.Application.Common.Interfaces;

public interface IGoogleAuthService
{
    public Task<GoogleJsonWebSignature.Payload?> VerifyGoogleIdToken(string idToken);
}