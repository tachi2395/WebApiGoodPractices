using AutoMapper;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.Model.Cars;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarModel, CarEntity>();
            CreateMap<CreateUpdateCarModel, CarEntity>();
        }
    }
}
