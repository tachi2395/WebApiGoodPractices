﻿namespace WebApiGoodPracticesSample.Web.DTO.Cars
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string SerialNumber { get; set; }
        public Color Color { get; set; }
    }
}