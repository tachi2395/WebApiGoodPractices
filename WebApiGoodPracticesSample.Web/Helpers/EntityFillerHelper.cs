using System;
using System.Collections.Generic;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.DAL.Entities;

namespace WebApiGoodPracticesSample.Web.Helpers
{
    public class EntityFillerHelper
    {
        private const int CARS = 50;
        private const int DRIVERS = 150;

        public static void FillDataBase(IDataRepository<CarEntity> carRepository, IDataRepository<DriverEntity> driverRepo)
        {
            // filling cars
            var colors = Enum.GetValues(typeof(Color));
            for (var i = 0; i < CARS; i++)
            {
                carRepository.Create(new CarEntity
                {
                    Manufacturer = _manufacturers[new Random().Next(_manufacturers.Count)],
                    Name = _modelCatalog[new Random().Next(_modelCatalog.Count)],
                    Model = new Random().Next(1950, 2021) + "",
                    Color = (Color)colors.GetValue(new Random().Next(colors.Length)),
                    SerialNumber = Guid.NewGuid().ToString("n").Substring(0, 8),
                    Drivers = new List<DriverEntity>(),
                    Id = 0
                });
            }

            // filling drivers
            var cars = carRepository.Get(x => true) as List<CarEntity>;

            for (var i = 0; i < DRIVERS; i++)
            {
                driverRepo.Create(new DriverEntity
                {
                    Id = 0,
                    Age = new Random().Next(19, 65),
                    LastName = _lastNames[new Random().Next(_lastNames.Count)],
                    Name = _driverNames[new Random().Next(_driverNames.Count)],
                    CarId = cars[new Random().Next(cars.Count)].Id,
                });
            }
        }

        private static List<string> _lastNames = new()
        {
            "Smith",
            "Johnson",
            "Williams",
            "Brown",
            "Jones",
            "Garcia",
            "Miller",
            "Davis",
            "Rodriguez",
            "Martinez",
            "Hernandez",
            "Lopez",
            "Gonzalez"
        };

        private static List<string> _driverNames = new()
        {
            "Noah",
            "Emma",
            "Oliver",
            "Ava",
            "William",
            "Sophia",
            "Elijah",
            "Isabella",
            "James",
            "Charlotte",
            "Benjamin",
            "Amelia",
            "Lucas",
            "Mia",
            "Liam",
            "Olivia",
            "Mason",
            "Harper",
            "Ethan",
            "Evelyn"
        };

        private static List<string> _modelCatalog = new()
        {
            "4Runner",
            "Cordoba",
            "Gran Fury",
            "Nubira",
            "Sonic",
            "Acadia",
            "Corniche",
            "Gran Turismo",
            "Oasis",
            "Sonoma",
            "Accent",
            //"Corolla",
            //"Grand Am",
            //"Odyssey",
            //"Sorento",
            //"Acclaim",
            //"Coronet",
            //"Grand Prix",
            //"Omega",
            //"Soul",
            //"Accord",
            //"Corrado",
            //"Grand Vitara",
            //"Omni",
            //"Spark",
            //"Achieva",
            //"Corsair",
            //"Grand Voyager",
            //"Optima",
            //"Spectra",
            //"Aerio",
            //"Corsica Greiz",
            //"Outback Spectrum"
        };

        private static List<string> _manufacturers = new()
        {
            "BMW",
            "Volkswagen",
            "Mercedes",
            "Chevrolet",
            "Ford",
            "Mazda",
            "Suzuki",
            "Hummer",
            "Ferrari",
            "Lamburgini"
        };
    }
}
