using System.Collections.Generic;

namespace WebApiGoodPracticesSample.Web.Model.Cars
{
    public class CarQueryModel
    {
        public IEnumerable<int> Id { get; set; }

        public IEnumerable<string> Manufacturer { get; set; }

        public IEnumerable<string> Name { get; set; }

        public IEnumerable<string> Field { get; set; }

        public string Sort { get; set; } = nameof(Id);
    }
}
