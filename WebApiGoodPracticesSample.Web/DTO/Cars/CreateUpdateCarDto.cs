namespace WebApiGoodPracticesSample.Web.DTO.Cars
{
    public class CreateUpdateCarDto
    {
        public string Manufaturer { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public Color Color { get; set; }
    }
}