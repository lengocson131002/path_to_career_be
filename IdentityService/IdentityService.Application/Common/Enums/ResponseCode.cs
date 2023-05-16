using System.ComponentModel;

namespace IdentityService.Application.Common.Enums;

public enum ResponseCode
{
    [Description("Common Error")] ErrorCommon = 1,

    [Description("Validation Error")] ErrorValidation = 2,

    [Description("Mapping Error")] ErrorMapping = 3,
    
    // Auth
    [Description("Invalid username or password")] AuthErrorInvalidUserOrPassword = 10,
    
    [Description("Invalid refresh token")] AuthErrorInvalidRefreshToken = 11,
    
    [Description("Invalid google ID token")] AuthErrorInvalidGoogleIdToken = 12,

    
    // Account
    [Description("Username existed")] AccountErrorUsernameExisted = 12,


}