using System.ComponentModel.DataAnnotations;

namespace WebApiGoodPracticesSample.Web.Model.Cars
{
    public class CreateUpdateCarModel
    {
        [Required]
        public string Manufaturer { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public Color Color { get; set; }
    }
}