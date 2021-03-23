using System.Collections;
using System.Collections.Immutable;

namespace ClassLibrary.NET5
{
    public record AddressRecord
    {
        public string Street { get; init; }
        public int Number { get; init; }
        public string Cep { get; init; }
        public string District { get; init; }
        public int CityId { get; init; }
        public int StateId { get; init; }
        
        public void Deconstruct(out string street, out int number) => (street, number) = (Street, Number);
    }
}
