using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ironfrost.Authentication.Models;
using Ironfrost.Authentication.Services;
using Ironfrost.Authentication.Services.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ironfrost.Authentication.Extensions;

public static class LoginResponseExtension
{
    private static readonly Regex s_Regex = new Regex(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        RegexOptions.Compiled);

    private static readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

    public static bool IsValidPassword(this LoginResponse loginResponse)
    {
        if (string.IsNullOrEmpty(loginResponse.Password))
            return false;

        return s_Regex.IsMatch(loginResponse.Password);
    }

    public static bool IsValidEmail(this LoginResponse loginResponse)
    {
        try
        {
            var mail = new MailAddress(loginResponse.Email);
            return mail.Address.Equals(loginResponse.Email);
        }
        catch
        {
            return false;
        }
    }

    #region Jwt

    private static string GetJwtTokenUsingEmail(this LoginResponse loginResponse, string policy, string? secretKey,
        string? issuer)
    {
        return GenerateJwtToken(loginResponse.Email, policy, secretKey ?? "", issuer ?? "");
    }

    private static string GenerateJwtToken(string username, string role, string secretKey, string issuer)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role), // papel do usuário
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion

    #region Account Creation

    public static async Task<Response> CreateAccount(this LoginResponse loginResponse, IConfiguration configuration,
        IDataBaseService dataBaseService)
    {
        try
        {
            string token = string.Empty;
            try
            {
                if (loginResponse.IsValidEmail())
                {
                    token = loginResponse.GetJwtTokenUsingEmail(loginResponse.IsAdmin ? "Admin" : "User",
                        configuration["Jwt:Key"], configuration["Jwt:Issuer"]);
                }
            }
            catch (Exception exception)
            {
                return new Response(false, string.Empty, "Invalid Email");
            }

            try
            {
                var existentUser = dataBaseService.Users().AsNoTracking()
                    .FirstOrDefault(u => u.email == loginResponse.Email);

                if (existentUser != null)
                    return new Response(false, string.Empty, $"Email has already been used");

                loginResponse = loginResponse with
                {
                    Password = HashPassword(loginResponse.Password)
                };

                dataBaseService.Users().Add(new Users(Guid.NewGuid(), loginResponse.Username, loginResponse.Email,
                    loginResponse.Password, loginResponse.IsAdmin));
                var result = await dataBaseService.SaveChangesAsync();
                if (result == 0) throw new Exception("Data Base Save Changes async failed");
            }
            catch (Exception e)
            {
                return new Response(false, string.Empty, e.InnerException?.Message ?? e.Message);
            }


            return new Response(true, token, "Confirm email");
        }
        catch
        {
            return new Response(false, string.Empty,
                "Error while trying to create a new user. Please get in touch with support");
        }
    }

    #endregion

    #region Login

    public static async Task<Response> Login(this LoginResponse response, IConfiguration configuration,
        IDataBaseService dataBaseService)
    {
        var noTrackingUsers = dataBaseService.Users().AsNoTracking();

        if (response.IsValidEmail())
        {
            var userByEmail = noTrackingUsers.FirstOrDefault(u => u.email == response.Email);
            if (userByEmail != null)
            {

                if (VerifyPassword(userByEmail.password, response.Password))
                {
                    return new Response(true,
                        response.GetJwtTokenUsingEmail(userByEmail.is_admin ? "Admin" : "User",
                            configuration["Jwt:Key"], configuration["Jwt:Issuer"]), $"successfully logged in");
                }

                return new Response(false, string.Empty, $"Incorrect password");
            }
        }

        if (response.Username == string.Empty)
        {
            return new Response(false, string.Empty, "Invalid username or email");
        }

        var userByName = noTrackingUsers.Where(u => u.username == response.Username);

        if (!userByName.Any())
        {
            return new Response(false, string.Empty, "Invalid username or email");
        }

        foreach (var userElement in userByName)
        {
            if (VerifyPassword(userElement.password, response.Password))
            {
                response = response with { Email = userElement.email };
                return new Response(true,
                    response.GetJwtTokenUsingEmail(userElement.is_admin ? "Admin" : "User", configuration["Jwt:Key"],
                        configuration["Jwt:Issuer"]), $"successfully logged in");
            }
        }

        return new Response(false, string.Empty, $"Invalid username or email");
    }

    #endregion

    private static string HashPassword(string password)
    {
        return _passwordHasher.HashPassword("DefaultUser", password);
    }

    private static bool VerifyPassword(string hashedPassword, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword("DefaultUser", hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }

}
