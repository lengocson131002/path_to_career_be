using Google.Apis.Auth;

namespace IdentityService.Application.Common.Interfaces;

public interface IGoogleAuthService
{
    public Task<GoogleJsonWebSignature.Payload?> VerifyGoogleIdToken(string idToken);
}