using System;
using Microsoft.AspNetCore.Mvc;
using ProjectTraining.Common.Resources;
using Serilog;

namespace ProjectTraining.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Acction get Error Bad Request
        /// </summary>
        /// <returns></returns>
        [Route("Error/400")]
        [HttpGet]
        public IActionResult Error400()
        {
            try
            {
                Log.Error(LoggerMessegeResource.Error400);
                return View();
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }
        
        /// <summary>
        /// Acction get error  Unauthorized
        /// </summary>
        /// <returns></returns>
        [Route("Error/401")]
        [HttpGet]
        public IActionResult Error401()
        {
            try
            {
                Log.Error(LoggerMessegeResource.Error401);
                return View();
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }
        
        /// <summary>
        /// Acction get error Page not found
        /// </summary>
        /// <returns></returns>
        [Route("Error/404")]
        [HttpGet]
        public IActionResult Error404()
        {
            try
            {
                Log.Error(LoggerMessegeResource.Error404);
                return View();
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }
        
        /// <summary>
        /// Acction get error Internal Server Error
        /// </summary>
        /// <returns></returns>
        [Route("Error/500")]
        [HttpGet]
        public IActionResult Error500()
        {
            try
            {
                Log.Error(LoggerMessegeResource.Error500);
                return View();
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }
    }
}