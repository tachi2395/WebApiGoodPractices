using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiGoodPracticesSample.Web.Model.Cars
{
    public class CarModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public Color? Color { get; set; }

        public IEnumerable<CarDriverModel> Drivers { get; set; }
    }
}
