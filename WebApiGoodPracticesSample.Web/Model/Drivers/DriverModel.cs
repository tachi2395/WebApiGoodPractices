﻿using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.Model.Common;

namespace WebApiGoodPracticesSample.Web.Model.Drivers
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int CarId { get; set; }
        public IEnumerable<LinkObjModel> Links { get; set; }
    }
}
