namespace IdentityService.Application.Auth.Models;

public class TokenResponse
{
    public string Token { get; private set; }

    public string RefreshToken { get; private set; }


    public TokenResponse(string token, string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }
}