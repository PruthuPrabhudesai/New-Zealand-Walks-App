using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;     // UserManager is a class provided by ASP.NET Core Identity that provides methods for managing users, such as creating users, deleting users, and validating user credentials.
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)  // We've injected Identity inside Program.cs file, so we can use it here in the constructor of AuthController. This is called dependency injection.
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        // POST: api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);  // userManager.CreateAsync() is basically the step where you create the user record in the Identity database.

            if (identityResult.Succeeded)    // Succeeded is a boolean property of IdentityResult that indicates whether the operation was successful or not.
            {
                // Add roles to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles); // AddToRolesAsync() is basically adding role information related to that user into another table in the Identity database.
                    if (identityResult.Succeeded)
                    {
                        return Ok(new { Message = "User registered successfully with roles." });
                    }
                }
            }
            return BadRequest("Something went wrong while adding roles.");
        }

        // POST: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // Get the roles for this user
                    var roles = await userManager.GetRolesAsync(user);  // CheckPasswordAsync is an ASP.NET Core Identity method that verifies whether a given password matches the hashed password stored for a user.

                    // create token
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);        // Remember that whatever sent to Ok () will be serialized to JSON and sent back to the client as the response body.
                    }
                    // You can use the response which comes in the Swagger to test the API in Postman. You can copy the JWT token from the response and use it in the Authorization header of subsequent requests to access protected endpoints.
                }
            }
            return BadRequest("Invalid username or password.");
        }
    }
}
