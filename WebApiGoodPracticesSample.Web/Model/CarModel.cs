using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Model
{
    public class CarModel : CommonEntity
    {
        public string Manufaturer { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public Color Color { get; set; }

        public IEnumerable<DriverModel> Drivers { get; set; }
    }
}