using AutoMapper;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Cars;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarModel, CarEntity>();
            CreateMap<CreateUpdateCarModel, CarEntity>();

            CreateMap<CarEntity, CarModel>();
            CreateMap<DriverModel, CarDriverModel>();
        }
    }
}
