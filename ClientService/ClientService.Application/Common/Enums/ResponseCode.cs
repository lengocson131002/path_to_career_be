using System.ComponentModel;

namespace ClientService.Application.Common.Enums;

public enum ResponseCode
{
    [Description("Common Error")] ErrorCommon = 1,

    [Description("Validation Error")] ErrorValidation = 2,

    [Description("Mapping Error")] ErrorMapping = 3
}