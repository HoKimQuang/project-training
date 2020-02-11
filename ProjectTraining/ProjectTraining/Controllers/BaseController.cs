using Microsoft.AspNetCore.Mvc;
using ProjectTraining.Filters;

namespace ProjectTraining.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller base project
    /// </summary>
    [ServiceFilter(typeof(ExpireDateUserFilter))]
    public class BaseController : Controller
    {}
}