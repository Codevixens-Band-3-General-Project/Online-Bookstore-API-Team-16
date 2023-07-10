using BookstoreAPI.Configurations;
using BookstoreAPI.Models;
using BookstoreAPI.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BookstoreAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        // private readonly JWTConfig _jwtConfig;
        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterationDTO userRegisterationDTO)
        {
            if (!Regex.IsMatch(userRegisterationDTO.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                return BadRequest(error: new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                   "Email is not valid"
                    }

                });
            }
            else if(userRegisterationDTO.Password.Length < 8)
            {
               return BadRequest(error:new AuthResult()
               {
                   Result = false,
                   Errors = new List<string>()
                   {
                   "Password is too short"
                    }

                   });
            }
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(userRegisterationDTO.Email);
                
                if (userExist != null)
                {
                    return BadRequest(error: new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "This email aleardy exist"
                        }

                    });
                }
                var newUser = new IdentityUser()
                {
                    Email = userRegisterationDTO.Email,
                    UserName = userRegisterationDTO.Username,

                };
                var isCreated = await _userManager.CreateAsync(newUser, userRegisterationDTO.Password);
                if (isCreated.Succeeded)
                {
                    var token = GenerateJWTToken(newUser);
                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token

                    });

                }
                else
                {

                    return BadRequest(error: new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                    {
                        "Couldn't create user"
                    }

                    }); ;
                }
            }
            else return BadRequest();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(userLoginDTO.Email);
                if (existingUser == null)
                {
                    return BadRequest(new AuthResult()
                    {

                        Result = false,
                        Errors = new List<string>()
                        {
                            "This email does not exist"
                        }
                    });

                }
                else
                {
                    var isMatch = await _userManager.CheckPasswordAsync(existingUser, userLoginDTO.Password);
                    if (!isMatch)
                    {
                        return BadRequest(new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Invaild email or password"
                            }
                        });
                    }
                    else
                    {
                        var token = GenerateJWTToken(existingUser);
                        return Ok(new AuthResult()
                        {
                            Token = token,
                            Result = true,

                        });
                    }
                }
            }
            else
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Invaild email or password"
                    }
                });

            }
        }
        private string GenerateJWTToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection(key: "JwtConfig:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(type: "Id", value: user.Id),
                        new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email
                        ),
                        new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email),
                        new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                        new Claim(type: JwtRegisteredClaimNames.Iat, value: DateTime.Now.ToUniversalTime().ToString())
                    }
                    ),


                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);//convert jwtToken to a string

        }
    }
}
