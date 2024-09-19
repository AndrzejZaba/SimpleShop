

namespace SimpleShop.Application.Common.Interfaces;

public interface IEmail
{
    Task Send(string subject, string body, string to, string attachmentPath = null);
}
