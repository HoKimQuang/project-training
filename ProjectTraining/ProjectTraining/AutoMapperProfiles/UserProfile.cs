using AutoMapper;
using ProjectTraining.Models;
using ProjectTraining.ViewModels;

namespace ProjectTraining.AutoMapperProfiles
{
    /// <summary>
    /// This class Create Mapper User with User ViewModel
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Constructor UserProfile Map User to User ViewModel
        /// </summary>
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}