using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Model
{
    public class DriverModel : CommonEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int CarId { get; set; }
    }
}
