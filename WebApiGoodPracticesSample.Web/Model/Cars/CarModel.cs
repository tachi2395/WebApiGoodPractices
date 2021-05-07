using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.Model.Drivers;

namespace WebApiGoodPracticesSample.Web.Model.Cars
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Manufaturer { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public Color Color { get; set; }

        public IEnumerable<DriverModel> Drivers { get; set; }
    }
}
