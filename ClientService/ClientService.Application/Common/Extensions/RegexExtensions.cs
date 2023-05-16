using System.Net.Mail;

namespace ClientService.Application.Common.Extensions;

public static class RegexExtensions
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var m = new MailAddress(email);
            return true;
        }
        catch (FormatException ex)
        {
            return false;
        }
    }
}