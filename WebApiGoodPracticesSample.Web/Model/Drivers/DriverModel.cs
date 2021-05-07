using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiGoodPracticesSample.Web.Model.Common;

namespace WebApiGoodPracticesSample.Web.Model.Drivers
{
    public class DriverModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public int CarId { get; set; }

        public IEnumerable<LinkObjModel> Links { get; set; }
    }
}
