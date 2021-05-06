using AutoMapper;
using WebApiGoodPracticesSample.Web.DAL.Entities;
using WebApiGoodPracticesSample.Web.DTO.Cars;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarDto, CarEntity>();
            CreateMap<CreateUpdateCarDto, CarEntity>();
        }
    }
}
