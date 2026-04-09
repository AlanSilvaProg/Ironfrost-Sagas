using Ironfrost.Authentication.Middlewares.Auth;

namespace Ironfrost.Authentication.Extensions;

public static class AuthenticationMiddlewareExtensions
{
    /// <summary>
    /// Authentication Middleware
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseAuthenticationValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}