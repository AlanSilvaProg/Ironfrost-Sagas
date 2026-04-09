using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ironfrost.Authentication.Extensions;
using Ironfrost.Authentication.Models;
using Ironfrost.Authentication.Services;

namespace Ironfrost.Authentication.Controllers.BackOffice;

/// <summary>
/// Controller for managing user creation operations
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class UserCreatorController(IConfiguration configuration, IDataBaseService dataBaseService) : Controller
{
    /// <summary>
    /// Creates a new user with admin privileges
    /// </summary>
    /// <param name="loginResponse">The user information to create</param>
    /// <returns>Response indicating success or failure</returns>
    /// <response code="200">Returns success response if user creation is successful</response>
    /// <response code="400">If the user information is invalid</response>
    [HttpPost]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] LoginResponse loginResponse)
    {
        return Ok(await loginResponse.CreateAccount(configuration, dataBaseService));
    }
}