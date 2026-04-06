namespace Icefrost.Authentication.Models;

public record LoginResponse(string Username, string Email, string Password, bool IsAdmin);