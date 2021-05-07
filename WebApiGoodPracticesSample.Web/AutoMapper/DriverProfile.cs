using AutoMapper;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverModel, DriverEntity>();
            CreateMap<CreateUpdateDriverModel, DriverEntity>();
        }
    }
}
