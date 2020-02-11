using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectTraining.Common;
using ProjectTraining.Common.Resources;
using Serilog;

namespace ProjectTraining.Middleware
{
    /// <summary>
    /// This class Exception Handler Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        /// <summary>
        /// Reclare RequesDelegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor ExceptionHandlerMiddleware
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                switch (context.Response.StatusCode)
                {
                    case 400:
                        context.Request.Path = Constants.PathError400;
                        await _next(context);
                        break;
                  
                    case 401:
                        context.Request.Path = Constants.PathError401;
                        await _next(context);
                        break;
                  
                    case  404:
                        context.Request.Path = Constants.PathError404;
                        await _next(context);
                        break;
                  
                    case  500:
                        context.Request.Path = Constants.PathError500;
                        await _next(context);
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                Log.Information(context.Response.StatusCode.ToString());
                throw;
            }
           
        } 
        
    }
}