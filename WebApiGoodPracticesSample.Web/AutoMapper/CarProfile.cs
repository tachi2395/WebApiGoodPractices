using AutoMapper;
using WebApiGoodPracticesSample.Web.DTO.Cars;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarDto, CarModel>();
            CreateMap<CreateUpdateCarDto, CarModel>();
        }
    }
}
