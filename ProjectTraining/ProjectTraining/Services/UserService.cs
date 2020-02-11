using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjectTraining.Common.Resources;
using ProjectTraining.Extensions;
using ProjectTraining.Models;
using ProjectTraining.Models.TableDataServerSide;
using ProjectTraining.Repositories;
using ProjectTraining.ViewModels;
using Serilog;

namespace ProjectTraining.Services
{
    /// <summary>
    /// Interface User service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        User Login(LoginViewModel model);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void CreateUser(User model);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserViewModel> GetUserViewModels();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserViewModel GetById(int? id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToAdd"></param>
        void Add(User entityToAdd);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(User entityToUpdate);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete(int? id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtParameters"></param>
        /// <returns></returns>
        Tuple<IEnumerable<UserViewModel>, int, int> LoatData(DTParameters dtParameters);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expireDate"></param>
        void UpdateExprireDate(int id, int expireDate);
    }

    /// <summary>
    /// User service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// User service constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Services

        /// <summary>
        /// Service login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public User Login(LoginViewModel model)
        {
            var account =
                _unitOfWork.UserRepository.ObjectContext.FirstOrDefault(m =>
                    m.UserName == model.UserName && m.Password == model.Pass);
            return account;
        }

        /// <summary>
        /// Service Create user
        /// </summary>
        /// <param name="model"></param>
        public void CreateUser(User model)
        {
            _unitOfWork.UserRepository.Add(model);
            model.CreateDate = DateTime.Now.AddDays(1);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Service Get list user view model
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetUserViewModels()
        {
            var user = _unitOfWork.UserRepository.GetAll();
            var useViewModel = _mapper.Map<IEnumerable<UserViewModel>>(user);
            return useViewModel;
        }

        /// <summary>
        /// Service Get list user view model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserViewModel GetById(int? id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);
            var listUserViewModelById = _mapper.Map<UserViewModel>(user);
            return listUserViewModelById;
        }

        /// <summary>
        /// Service Create user
        /// </summary>
        /// <param name="entityToAdd"></param>
        public void Add(User entityToAdd)
        {
            _unitOfWork.UserRepository.Add(entityToAdd);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Service update user
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public void Update(User entityToUpdate)
        {
            _unitOfWork.UserRepository.Update(entityToUpdate);
           entityToUpdate.CreateDate=DateTime.Now;
            _unitOfWork.Save();
        }

        /// <summary>
        /// update Expridate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountRenewal"></param>
        public void UpdateExprireDate(int id, int accountRenewal)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetById(id);
                if (user.ExpireDate <= DateTime.Now)
                {
                    user.ExpireDate = DateTime.Now.AddSeconds(accountRenewal);
                    _unitOfWork.Save();
                }
                else
                {
                    user.ExpireDate = user.ExpireDate.AddSeconds(accountRenewal);
                    _unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                Log.Error(LoggerMessegeResource.SomethingWentWrong, e);
                throw;
            }
        }

        /// <summary>
        /// Service delete user
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Service Get datatable server side processing.
        /// </summary>
        /// <param name="dtParameters"></param>
        /// <returns> result </returns>
        public Tuple<IEnumerable<UserViewModel>, int, int> LoatData(DTParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            string orderCriteria;
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "Id";
            }

            var result = GetUserViewModels();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.UserName != null && r.UserName.ToUpper().Contains(searchBy.ToUpper())
                    )
                    .ToList();
            }

            result = orderAscendingDirection
                ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Asc).ToList()
                : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtensions.Order.Desc).ToList();

            var userViewModels = result.ToList();
            var filteredResultsCount = userViewModels.Count();
            var totalResultsCount = GetUserViewModels().Count();
            Tuple<IEnumerable<UserViewModel>, int, int> tuple =
                new Tuple<IEnumerable<UserViewModel>, int, int>(userViewModels, filteredResultsCount, totalResultsCount);
            return tuple;
        }




        #endregion
    }
}