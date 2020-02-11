using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjectTraining.Models;
using ProjectTraining.Services;
using ProjectTraining.ViewModels;

namespace ProjectTraining.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// This Authentication controller json wed token
    /// </summary>
    [Route("api/authentication")]
    public class TokenController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        /// <summary>
        /// TokenController construsctor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="userService"></param>
        public TokenController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        /// <summary>
        /// Acction Create token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            var user = _userService.Login(login);

            if (user == null) return response;
            
            var tokenString = BuildToken(user);
            response = Ok(new { token = tokenString });

            return response;
        }

        /// <summary>
        /// method create key token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string BuildToken(User user)
        {
            //the claims set information user in payload JWT
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //encoding key token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30), //expire time là 30 phút
                signingCredentials: creds);
            // Create key token.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}