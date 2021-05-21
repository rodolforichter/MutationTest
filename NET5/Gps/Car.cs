using System;

namespace NET5
{
    public class Car
    {
        public string Id { get; set; }
        public string Plate { get; set; }
    }

    public struct CarV2
    {
        public readonly int Id { get; init; }
        public string Plate { get; set; }

        public CarV2(int id, string plate)
        {
            Id = id;
            Plate = plate;
        }
    }

    public record CarV3
    {
        public int Id { get; init; }
        public string Plate { get; init; }

        public CarV3(int id, string plate)
        {
            Id = id;
            Plate = plate;
        }
    }
}
