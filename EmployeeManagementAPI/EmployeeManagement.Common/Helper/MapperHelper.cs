using AutoMapper;
using EmployeeManagement.Entities.Models.DTOModels;
using EmployeeManagement.Entities.Models.PayloadModel;
using EmployeeManagement.Services;

namespace EmployeeManagement.common.Helper
{
    public class MapperHelper
    {
        public readonly MapperConfiguration config;
        public readonly Mapper mapper;
        public MapperHelper()
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Employeepayload, EmployeeDTO>());
            mapper = new Mapper(config);
        }
    }
}
