using IdentityService.Application.Common.Enums;
using IdentityService.Application.Common.Extentions;

namespace IdentityService.Application.Common.Exceptions;

public class ApiException : Exception
{
    private readonly ResponseCode _responseCode;

    public ApiException(ResponseCode responseCode)
    {
        _responseCode = responseCode;
    }

    public int ErrorCode => (int)_responseCode;

    public string Error => _responseCode.ToString();

    public string Message => _responseCode.GetDescription();
}