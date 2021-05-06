namespace WebApiGoodPracticesSample.Web.DTO.Drivers
{
    public class CreateUpdateDriverDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int CarId { get; set; }
    }
}
