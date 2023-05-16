using System.ComponentModel;

namespace ClientService.Application.Common.Enums;

public enum ResponseCode
{
    [Description("Common Error")] ErrorCommon = 1,

    [Description("Validation Error")] ErrorValidation = 2,

    [Description("Mapping Error")] ErrorMapping = 3,
    
    // File 
    
    [Description("File not found")] ErrorFileNotFound = 10,

    [Description("Delete file failed")] ErrorDeleteFileFailed = 11,
    
    [Description("Upload file failed")] ErrorUploadFileFailed = 12,
    
    // Auth
        
    [Description("Invalid email or password")] AuthErrorInvalidEmailOrPassword = 20,
    
    [Description("Invalid refresh token")] AuthErrorInvalidRefreshToken = 21,
    
    [Description("Invalid google ID token")] AuthErrorInvalidGoogleIdToken = 22,

    
    // Account
    [Description("Email existed")] AccountErrorEmailExisted = 30,
}