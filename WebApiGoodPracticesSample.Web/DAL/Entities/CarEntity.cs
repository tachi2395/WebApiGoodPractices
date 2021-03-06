using System.Collections.Generic;

namespace WebApiGoodPracticesSample.Web.DAL.Entities
{
    public class CarEntity : CommonEntity
    {
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public Color? Color { get; set; }
    }
}
