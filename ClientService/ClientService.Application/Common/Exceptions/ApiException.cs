using ClientService.Application.Common.Enums;
using ClientService.Application.Common.Extentions;

namespace ClientService.Application.Common.Exceptions;

public class ApiException
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