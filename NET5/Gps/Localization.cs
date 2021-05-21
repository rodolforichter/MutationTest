using NET5;

namespace NET5
{
    public class Localization
    {
        public Car Car { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Localization()
        {
            Car = new();
        }
    }

    public class LocalizationV2
    {
        public Car Car { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }

        public LocalizationV2()
        {
            Car = new();
        }
    }

    public readonly struct LocalizationV3
    {
        public Car Car { get; init; }
        public int Latitude { get; init; }
        public int Longitude { get; init; }

        public LocalizationV3(int latitude, int longitude, string carId, string carPlate)
        {
            Car = new Car { Id = carId, Plate = carPlate };
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public record LocalizationR
    {
        public CarV3 Car { get; init; }
        public int Latitude { get; init; }
        public int Longitude { get; init; }

        public LocalizationR(int latitude, int longitude, CarV3 car)
        {
            Car = car;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
