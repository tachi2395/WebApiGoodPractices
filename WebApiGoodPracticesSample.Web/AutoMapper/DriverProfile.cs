using AutoMapper;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.DTO.Drivers;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverDto, DriverEntity>();
            CreateMap<CreateUpdateDriverDto, DriverEntity>();
        }
    }
}
