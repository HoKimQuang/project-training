using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectTraining.Common.Resources;
using ProjectTraining.Services;
using Serilog;

namespace ProjectTraining.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// This class filter Expire Date Account
    /// </summary>
    public class ExpireDateUserFilter : IActionFilter
    {
        /// <summary>
        /// Reclare User sevice
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public ExpireDateUserFilter(IUserService userService)
        {
            _userService = userService;
        }
        /// <inheritdoc />
        /// <summary>
        /// Method On Acction Executing
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpcontext = context.HttpContext;
            var id = Convert.ToInt32(httpcontext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetById(id);
            if (user.ExpireDate >= DateTime.Now) return;
            Log.Information(LoggerMessegeResource.OnActionExecuting + context.RouteData);
            context.Result = new ViewResult
            {
                ViewName = "FilterExpired"
            };
        }
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {}
    }
}