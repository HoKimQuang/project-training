using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjectTraining.Common.Resources;
using ProjectTraining.Models;
using ProjectTraining.Services;
using ProjectTraining.ViewModels;
using Serilog;

namespace ProjectTraining.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Reclare user service
        /// </summary>
      private readonly IUserService _usersService;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="usersService"></param>
        public AccountController(IUserService usersService)
        {
            _usersService = usersService;
        }
        
        /// <summary>
        /// Redirect to login view
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                var user = _usersService.Login(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty,MessageResource.AccountLoginFailed);
                    Log.Error(MessageResource.AccountLoginFailed);
                    return View(model);
                }
                await LoginAsync(user);
                return Redirect(model.ReturnUrl ?? "/");
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task LoginAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
//                new Claim(ClaimTypes.Expiration, user.ExpireDate.ToShortDateString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> Logout(string returnUrl)
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }

        /// <summary>
        /// Acction account renewal 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AccountRenewal()
        {
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _usersService.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }

        /// <summary>
        /// Post edit information user
        /// </summary>
        /// <param name="accountRenewal"></param>
        /// <returns></returns>
        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AccountRenewal(int accountRenewal)
        {
            try
            {
                var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _usersService.UpdateExprireDate(id,accountRenewal);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }
    }
}