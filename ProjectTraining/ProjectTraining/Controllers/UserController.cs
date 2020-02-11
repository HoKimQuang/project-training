using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTraining.Common.Resources;
using ProjectTraining.Models;
using ProjectTraining.Models.TableDataServerSide;
using ProjectTraining.Services;
using Serilog;

namespace ProjectTraining.Controllers
{
    [Authorize(Policy = "AllowAll")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Reclare user service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// UserControler constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get list user by server side
        /// </summary>
        /// <returns> view Index</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get list user by jquery datatable front end
        /// </summary>
        /// <returns> View queryDataTableFrontEnd </returns>
        [HttpGet]
        public IActionResult JqueryDataTableFrontEnd()
        {
            var listUserViewModel = _userService.GetUserViewModels();
            return View(listUserViewModel);
        }

        #region 

        /// <summary>
        /// Action get Details user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: User/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        /// <summary>
        /// Action Create user
        /// </summary>
        /// <returns></returns>
        // GET: User/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            try
            {
                if (!ModelState.IsValid) return View(user);
                _userService.CreateUser(user);
                TempData["CreateSuccess"] = MessageResource.CreateSuccess;
                Log.Information("CreateSuccess: " , user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }

        /// <summary>
        /// Action Edit user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: User/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }

            return View(user);
        }

        /// <summary>
        /// Post edit information user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }

                _userService.Update(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }

        /// <summary>
        /// post delete information user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                _userService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }

        #endregion

        #region ServerSideProcessing

        /// <summary>
        /// Action Loadtable convert Object User to Json
        /// </summary>
        /// <param name="dtParameters"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LoadTable([FromBody] DTParameters dtParameters)
        {
            try
            {
                var result = _userService.LoatData(dtParameters);
                return Json(new
                {
                    draw = dtParameters.Draw,
                    recordsTotal = result.Item3,
                    recordsFiltered = result.Item2,
                    data = result.Item1
                        .Skip(dtParameters.Start)
                        .Take(dtParameters.Length)
                        .ToList()
                });
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong + e);
                throw;
            }
        }

        #endregion
    }
}