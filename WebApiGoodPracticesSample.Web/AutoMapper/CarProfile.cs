using AutoMapper;
using WebApiGoodPracticesSample.Web.DTO;
using WebApiGoodPracticesSample.Web.Model;

namespace WebApiGoodPracticesSample.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<BulkUpdateCarDto, CarModel>();
            CreateMap<CarDto, CarModel>();
            CreateMap<CreateCarDto, CarModel>();
            CreateMap<SingleUpdateCarDto, CarModel>();
        }
    }
}
