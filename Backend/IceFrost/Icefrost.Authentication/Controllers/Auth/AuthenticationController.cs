using Microsoft.AspNetCore.Mvc;
using Icefrost.Authentication.Extensions;
using Icefrost.Authentication.Models;
using Icefrost.Authentication.Services;

namespace Icefrost.Authentication.Controllers;

/// <summary>
/// Controller for handling authentication operations
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController(IConfiguration configuration, IDataBaseService dataBaseService) : Controller
{
    /// <summary>
    /// Authenticates a user and returns a JWT token
    /// </summary>
    /// <param name="loginResponse">The login credentials</param>
    /// <returns>JWT token if authentication is successful</returns>
    /// <response code="200">Returns the JWT token</response>
    /// <response code="401">If the credentials are invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> Login([FromBody] LoginResponse loginResponse)
    {
        var loginResult = await loginResponse.Login(configuration, dataBaseService);
        
        return Ok(loginResult);
    }

    /// <summary>
    /// Creates a new user account
    /// </summary>
    /// <param name="loginResponse">The user registration information</param>
    /// <returns>Response indicating success or failure</returns>
    /// <response code="200">Returns success response if account creation is successful</response>
    /// <response code="400">If the registration information is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> CreateAccount([FromBody] LoginResponse loginResponse)
    {
        var createAccountResult = await loginResponse.CreateAccount(configuration, dataBaseService);
        
        return Ok(createAccountResult);
    }
}