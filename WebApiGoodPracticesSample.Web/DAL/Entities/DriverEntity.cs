namespace WebApiGoodPracticesSample.Web.DAL.Entities
{
    public class DriverEntity : CommonEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int? CarId { get; set; }
    }
}
