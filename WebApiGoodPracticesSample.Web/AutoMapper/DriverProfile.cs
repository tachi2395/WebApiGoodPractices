using AutoMapper;
using WebApiGoodPracticesSample.Web.DTO.Drivers;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverDto, DriverModel>();
            CreateMap<CreateUpdateDriverDto, DriverModel>();
        }
    }
}
