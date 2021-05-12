using System.ComponentModel.DataAnnotations;

namespace WebApiGoodPracticesSample.Web.Model.Drivers
{
    public class CreateUpdateDriverModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public int CarId { get; set; }
    }
}
