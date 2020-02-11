using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTraining.Services;

namespace ProjectTraining.Controllers
{

   
    [Route("api/[controller]")]
    public class UserApiController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public UserApiController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// API acction get all user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAll()
        {
            var getAll = _userService.GetUserViewModels();
            return Ok(getAll);
        }
    }
}