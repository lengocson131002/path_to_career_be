using System.ComponentModel;

namespace ClientService.Application.Common.Enums;

public enum ResponseCode
{
    [Description("Common Error")] ErrorCommon = 1,

    [Description("Validation Error")] ErrorValidation = 2,

    [Description("Mapping Error")] ErrorMapping = 3,
    
    [Description("Unauthorized")] Unauthorized = 4,
    
    [Description("Invalid query")] InvalidQuery = 5,
    
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
    
    [Description("Password and Confirm password are not matched")] AccountErrorPasswordNotMatched = 31,

    [Description("Account not found")] AccountErrorNotFound = 32,

    // Review
    [Description("You had reviewed this account before")] ReviewErrorExisted = 40,

    [Description("Review not found")] ReviewErrorNotFound = 41,
    
    [Description("Can not review yourself")] ReviewErrorCannotReviewYourself = 42,

    

    // Post
    [Description("Post's application is not found")] PostApplicationNotFound = 52,
    [Description("Post is completely applied")] PostIsDone = 53,
    [Description("Post is not found")] PostNotFound = 54,
    [Description("You have applied this post before")] ExistApplication = 55,
    [Description("Post's status is invalid to do this function")] InvalidPostStatus = 56,
    [Description("Post's have not been paid")] PostNotPaid = 57,

    // Service
    [Description("Service not found")] ServiceErrorNotFound = 60,
    [Description("Service's name existed")] ServiceErrorExistedName = 61,
    [Description("Account has current active service")] ServiceErrorAccountHasCurrentActiveService = 62,
    [Description("Account has no current active service")] ServiceErrorAccountHasNoActiveService = 63,
    
}