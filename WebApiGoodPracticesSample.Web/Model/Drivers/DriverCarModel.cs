using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiGoodPracticesSample.Web.Model.Common;

namespace WebApiGoodPracticesSample.Web.Model.Drivers
{
    public class DriverCarModel
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

        public IEnumerable<LinkObjModel> Links { get; set; }
    }
}
