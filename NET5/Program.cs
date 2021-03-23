using BenchmarkDotNet.Running;
using ClassLibrary.NET5;
using NET5.Gps;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NET5
{
    class Program
    {
        private static string Name;
        static void Main(string[] args)
        {
            //Console.WriteLine(Name);
            TestRecord();
        }

        //[ModuleInitializer]
        //public static void Init()
        //{
        //    Name = "Richter name";
        //}

        private static void TestRecord()
        {
            //AddressRecord address = new AddressRecord { Street = "Afonso Celso", Number = 12, Cep = "09913115", District = "Centro", CityId = 13, StateId = 25 };
            //AddressRecord address3 = new AddressRecord { Street = "Afonso Celso", Number = 12, Cep = "09913115", District = "Centro", CityId = 13, StateId = 25 };
            ////address.Street = "Nova rua";

            //Console.WriteLine(address == address3);


            //Console.WriteLine(address.Equals(address3));
            //Console.WriteLine(ReferenceEquals(address, address3));

            //AddressRecord address2 = address with { Street = "Scarlet Johanson" };
            //PersonRecord person = new PersonRecord("Richter Walter", 18);
            //PersonRecord person2 = person with { Age = 21 };

            //var (street, number) = address;
            //string rua = street;
            //int numero = number;

            //int? numeroNullable = default(int?);

            //int value = numeroNullable ?? 13;

            //var summary = BenchmarkRunner.Run<LoopBenchmark>();
            //new SpanClassTest().TestList2();
            //new SpanClassTest().TestList3();

            //var summary = BenchmarkRunner.Run<SpanClassTest>();

            var obj = new GpsTest();
            obj.Initialize();
            // obj.Search10();
            // obj.Search17();
            //obj.Search193StepsAndRecord5SequenceEquals();
            var summary = BenchmarkRunner.Run<GpsTest>();

            //Console.Read();
        }
    }
}
