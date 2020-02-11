using AutoMapper;
using ProjectTraining.Models;
using ProjectTraining.ViewModels;

namespace ProjectTraningxUnitTest.DataFakeInMemory
{
    public class AutoMapperConfig
    {
        private static object _thisLock = new object();
        private static bool _initialized;

        private static IMapper _mapper;
        // Centralize automapper initialize
        public static void Initialize()
        {
            lock (_thisLock)
            {
                if (_initialized) return;
                var config = new MapperConfiguration(opts =>
                {
                    opts.CreateMap<User, UserViewModel>();
                });
                _initialized = true;
                _mapper = config.CreateMapper();
            }
        }

        public static IMapper GetMapper()
        {
            return _mapper;
        }
    }
}