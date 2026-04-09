using System.Text.Json;
using Ironfrost.Authentication.Extensions;
using Ironfrost.Authentication.Models;

namespace Ironfrost.Authentication.Middlewares.Auth;

public class AuthenticationMiddleware 
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        
#region Login
        if (context.Request.Path.Equals("/api/authentication/login", StringComparison.OrdinalIgnoreCase)&&
            context.Request.Method == HttpMethods.Post)
        {
            context.Request.EnableBuffering();
            
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            
            context.Request.Body.Position = 0;
            
            try
            {
                var login = JsonSerializer.Deserialize<LoginResponse>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            
                if (login == null || string.IsNullOrWhiteSpace(login.Email)
                                  && string.IsNullOrWhiteSpace(login.Username))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Email or username is needed");
                    return;
                }
            
                if (string.IsNullOrWhiteSpace(login.Username) && !login.IsValidEmail())
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid Email.");
                    return;
                }
            }
            catch
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Invalid Json format.");
                return;
            }
        }
#endregion

#region Account Creation

if (context.Request.Path.Equals("/api/authentication/createaccount", StringComparison.OrdinalIgnoreCase)&&
    context.Request.Method == HttpMethods.Post)
{
    context.Request.EnableBuffering();

    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
    var body = await reader.ReadToEndAsync();
            
    context.Request.Body.Position = 0;
            
    try
    {
        var login = JsonSerializer.Deserialize<LoginResponse>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (login == null || string.IsNullOrWhiteSpace(login.Email)
            || string.IsNullOrWhiteSpace(login.Username))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Email and username is needed");
            return;
        }

        if (!login.IsValidEmail())
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid Email.");
            return;
        }

        if (!login.IsValidPassword())
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Password isn't following the rules.");
            return;
        }
    }
    catch
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Invalid Json format.");
        return;
    }
}

#endregion

        await _next(context);
    }
}