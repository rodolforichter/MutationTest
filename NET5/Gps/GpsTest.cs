using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NET5.Gps
{
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [RankColumn]
    public class GpsTest
    {
        private IList<Localization> _localizations;
        private int Count { get; set; } = 500;
        private static string SearchForPlateInit = "247";

        private (int init, int final) _pointLatitude = (3146, 3276);
        private (int init, int final) _pointLongitude = (2178, 2268);


        [GlobalSetup]
        public void Initialize()
        {
            _localizations = new List<Localization>();
            for(int i = 0; i < Count; i++)
            {
                Localization localization = new();
                localization.Car.Id = i.ToString();
                localization.Car.Plate = GetPlate(i);
                localization.Latitude = (i * 13).ToString();
                localization.Longitude = (i * 9).ToString();
                _localizations.Add(localization);
            }

            _localizations[247].Car.Plate = "713325";
        }

        private string GetPlate(int i)
        {
            string num = new Random().Next(1, 9).ToString();
            string letters = i.ToString().PadLeft(3, num[0]);
            string plate = string.Concat(letters, num, i.ToString(), i.ToString());
            return plate;
        }

        //[Benchmark]
        //public void Search01()
        //{
        //    Localization localizationSearched = _localizations.Where(x => x.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(x.Latitude) > _pointLatitude.init && int.Parse(x.Latitude) < _pointLatitude.final
        //    && int.Parse(x.Longitude) > _pointLongitude.init && int.Parse(x.Longitude) < _pointLongitude.final).FirstOrDefault();
        //}

        //[Benchmark]
        //public void Search02()
        //{
        //    Localization localizationSearched = _localizations.Where(_ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(_.Latitude) > _pointLatitude.init && int.Parse(_.Latitude) < _pointLatitude.final
        //    && int.Parse(_.Longitude) > _pointLongitude.init && int.Parse(_.Longitude) < _pointLongitude.final).FirstOrDefault();
        //}

        //[Benchmark]
        //public void Search03()
        //{
        //    Func<Localization, bool> predicate = _ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(_.Latitude) > _pointLatitude.init && int.Parse(_.Latitude) < _pointLatitude.final
        //    && int.Parse(_.Longitude) > _pointLongitude.init && int.Parse(_.Longitude) < _pointLongitude.final;

        //    Localization localizationSearched = _localizations.Where(predicate).FirstOrDefault();
        //}

        //[Benchmark]
        //public void Search04()
        //{
        //    Func<Localization, bool> predicate = _ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(_.Latitude) > _pointLatitude.init && int.Parse(_.Latitude) < _pointLatitude.final
        //    && int.Parse(_.Longitude) > _pointLongitude.init && int.Parse(_.Longitude) < _pointLongitude.final;

        //    Localization localizationSearched = _localizations.FirstOrDefault(predicate);
        //}

        //[Benchmark]
        //public void Search05()
        //{
        //    (int init, int final) pointLatitude = (3146, 3276);
        //    (int init, int final) pointLongitude = (2178, 2268);

        //    Func<Localization, bool> predicate = _ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //        && int.Parse(_.Latitude) > pointLatitude.init && int.Parse(_.Latitude) < pointLatitude.final
        //        && int.Parse(_.Longitude) > pointLongitude.init && int.Parse(_.Longitude) < pointLongitude.final;

        //    Localization localizationSearched = _localizations.FirstOrDefault(predicate);
        //}

        [Benchmark]
        public void Search06()
        {
            IList<Localization> localizations = new List<Localization>();

            for (int i = 0; i < Count; i++)
            {
                Localization localization = new();
                localization.Car.Id = i.ToString();
                localization.Car.Plate = GetPlate(i);
                localization.Latitude = (i * 13).ToString();
                localization.Longitude = (i * 9).ToString();
                localizations.Add(localization);
            }

            localizations[247].Car.Plate = "713325";
            Predicate<Localization> predicate01 = _ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
            && int.Parse(_.Latitude) > _pointLatitude.init && int.Parse(_.Latitude) < _pointLatitude.final
            && int.Parse(_.Longitude) > _pointLongitude.init && int.Parse(_.Longitude) < _pointLongitude.final;

            Localization localizationSearched = localizations.ToList().Find(predicate01);
        }

        //[Benchmark]
        //public void Search07()
        //{
        //    IList<Localization> localizations = new List<Localization>();

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";
        //    Localization localizationSearched = localizations.ToList().Find(_ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(_.Latitude) > _pointLatitude.init && int.Parse(_.Latitude) < _pointLatitude.final
        //    && int.Parse(_.Longitude) > _pointLongitude.init && int.Parse(_.Longitude) < _pointLongitude.final);
        //}

        //[Benchmark]
        //public void Search08()
        //{
        //    IList<Localization> localizations = new List<Localization>();

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";
        //    Localization localizationSearched = localizations.ToList().Find(_ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(_.Latitude) > 3146 && int.Parse(_.Latitude) < 3276
        //    && int.Parse(_.Longitude) > 2178 && int.Parse(_.Longitude) < 2268);
        //}

        //[Benchmark]
        //public void Search09()
        //{
        //    IList<Localization> localizations = new List<Localization>();

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    Localization localizationSearched = localizations.ToList().Find(_ => _.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit)
        //    && int.Parse(_.Latitude) > 3146 && int.Parse(_.Latitude) < 3276
        //    && int.Parse(_.Longitude) > 2178 && int.Parse(_.Longitude) < 2268);
        //}

        //[Benchmark]
        //public void Search10()
        //{
        //    IList<Localization> localizations = new List<Localization>();

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);           
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.ToList().Find(_ => IsPlate(_.Car.Plate)
        //    && int.Parse(_.Latitude) > 3146 && int.Parse(_.Latitude) < 3276
        //    && int.Parse(_.Longitude) > 2178 && int.Parse(_.Longitude) < 2268);
        //}

        //[Benchmark]
        //public void Search11()
        //{
        //    List<Localization> localizations = new List<Localization>();

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlate(_.Car.Plate)
        //    && int.Parse(_.Latitude) > 3146 && int.Parse(_.Latitude) < 3276
        //    && int.Parse(_.Longitude) > 2178 && int.Parse(_.Longitude) < 2268);
        //}

        //[Benchmark]
        //public void Search12()
        //{
        //    List<Localization> localizations = new List<Localization>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlate(_.Car.Plate)
        //    && int.Parse(_.Latitude) > 3146 && int.Parse(_.Latitude) < 3276
        //    && int.Parse(_.Longitude) > 2178 && int.Parse(_.Longitude) < 2268);
        //}
        //[Benchmark]
        //public void Search13()
        //{
        //    Collection<Localization> localizations = new();

        //    for (int i = 0; i < Count; i++)
        //    {
        //        Localization localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13).ToString();
        //        localization.Longitude = (i * 9).ToString();
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Where(_ => IsPlate(_.Car.Plate)
        //    && int.Parse(_.Latitude) > 3146 && int.Parse(_.Latitude) < 3276
        //    && int.Parse(_.Longitude) > 2178 && int.Parse(_.Longitude) < 2268).SingleOrDefault();
        //}

        //[Benchmark]
        //public void Search14()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlate(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268);
        //}

        //[Benchmark]
        //public void Search15()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Where(_ => IsPlate(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268).FirstOrDefault();
        //}

        //[Benchmark]
        //public void Search16()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Where(_ => IsPlate01(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268).FirstOrDefault();
        //}

        //[Benchmark]
        //public void Search17()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlate01(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268);
        //}

        //[Benchmark]
        //public void Search19Steps()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.FindAll(_ => _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268).Find(_ => IsPlate01(_.Car.Plate));
        //}

        //[Benchmark]
        //public void Search193Steps()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(Count);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    var result = localizations.FindAll(_ => _.Latitude > 3146 && _.Latitude < 3276)
        //        .FindAll(_ => _.Longitude > 2178 && _.Longitude < 2268)
        //        .Find(_ => IsPlate01(_.Car.Plate));
        //}

        [Benchmark]
        public void Search193StepsAndRecord()
        {
            List<LocalizationR> localizations = new List<LocalizationR>(Count);

            for (int i = 0; i < Count; i++)
            {
                LocalizationR localization = new((i * 13), (i * 9), new CarV3(i, GetPlate(i)));               
                localizations.Add(localization);
            }

            var result = localizations
                .FindAll(_ => _.Latitude > 3146 && _.Latitude < 3276)
                .FindAll(_ => _.Longitude > 2178 && _.Longitude < 2268)
                .Find(_ => IsPlate01(_.Car.Plate));
        }

        [Benchmark]
        public void Search193StepsAndRecord5()
        {
            List<LocalizationR> localizations = new List<LocalizationR>(Count);

            for (int i = 0; i < Count; i++)
            {
                LocalizationR localization = new((i * 13), (i * 9), new CarV3(i, GetPlate(i)));
                localizations.Add(localization);
            }

            var result = localizations
                .FindAll(_ => _.Latitude > 3146 && _.Latitude < 3276)
                .FindAll(_ => _.Longitude > 2178 && _.Longitude < 2268)
                .Find(_ => IsPlate0CompareWhitoutEquals(_.Car.Plate));
        }

        //[Benchmark]
        //public void Search193StepsAndRecord5SequenceEquals()
        //{
        //    List<LocalizationR> localizations = new List<LocalizationR>(Count);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationR localization = new((i * 13), (i * 9), new CarV3(i, GetPlate(i)));
        //        localizations.Add(localization);
        //    }

        //    var result = localizations
        //        .FindAll(_ => _.Latitude > 3146 && _.Latitude < 3276)
        //        .FindAll(_ => _.Longitude > 2178 && _.Longitude < 2268)
        //        .Find(_ => IsPlateSequenceEquals(_.Car.Plate));
        //}

        //[Benchmark]
        //public void Search193StepsAndRecord3()
        //{
        //    List<LocalizationR> localizations = new List<LocalizationR>(Count);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationR localization = new((i * 13), (i * 9), new CarV3(i, GetPlate(i)));
        //        localizations.Add(localization);
        //    }

        //    Predicate<LocalizationR> predicatePlate = _ => IsPlate01(_.Car.Plate);
        //    Predicate<LocalizationR> predicateLatitude = _ => _.Latitude > _pointLatitude.init && _.Latitude < _pointLatitude.final;
        //    Predicate<LocalizationR> predicateLongitude = _ => _.Longitude > _pointLongitude.init && _.Longitude < _pointLongitude.final;

        //    var result = localizations
        //        .FindAll(predicateLatitude)
        //        .FindAll(predicateLongitude)
        //        .Find(predicatePlate);
        //}

        [Benchmark]
        public void Search193StepsAndRecord4()
        {
            List<LocalizationR> localizations = new List<LocalizationR>(Count);

            for (int i = 0; i < Count; i++)
            {
                LocalizationR localization = new((i * 13), (i * 9), new CarV3(i, GetPlate(i)));
                localizations.Add(localization);
            }

            Predicate<LocalizationR> predicatePlate = _ => IsPlateSubstring(_.Car.Plate);
            Predicate<LocalizationR> predicateLatitude = _ => _.Latitude > _pointLatitude.init && _.Latitude < _pointLatitude.final;
            Predicate<LocalizationR> predicateLongitude = _ => _.Longitude > _pointLongitude.init && _.Longitude < _pointLongitude.final;

            var result = localizations
                .FindAll(predicateLatitude)
                .FindAll(predicateLongitude)
                .Find(predicatePlate);
        }

        [Benchmark]
        public void Search193StepsAndRecord4B()
        {
            List<LocalizationR> localizations = new List<LocalizationR>(Count);

            for (int i = 0; i < Count; i++)
            {
                localizations.Add(new LocalizationR((i * 13), (i * 9), new CarV3(i, GetPlate(i))));
            }

            Predicate<LocalizationR> predicatePlate = _ => IsPlateSubstring(_.Car.Plate);
            Predicate<LocalizationR> predicateLatitude = _ => _.Latitude > _pointLatitude.init && _.Latitude < _pointLatitude.final;
            Predicate<LocalizationR> predicateLongitude = _ => _.Longitude > _pointLongitude.init && _.Longitude < _pointLongitude.final;

            var result = localizations
                .FindAll(predicateLatitude)
                .FindAll(predicateLongitude)
                .Find(predicatePlate);
        }

        [Benchmark]
        public void Search193StepsAndRecord2()
        {
            List<LocalizationR> localizations = new List<LocalizationR>(Count);

            for (int i = 0; i < Count; i++)
            {
                LocalizationR localization = new((i * 13), (i * 9), new CarV3(i, GetPlate(i)));
                localizations.Add(localization);
            }

            var result = localizations
                .FindAll(_ => IsPlate01(_.Car.Plate))
                .FindAll(_ => _.Longitude > 2178 && _.Longitude < 2268)
                .Find(_ => _.Latitude > 3146 && _.Latitude < 3276);
        }

        //[Benchmark]
        //public void Search193StepsAsParallel()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(Count);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.AsParallel().Where(_ => _.Latitude > 3146 && _.Latitude < 3276
        //       && _.Longitude > 2178 && _.Longitude < 2268 && IsPlate01(_.Car.Plate)).FirstOrDefault();
        //}

        //[Benchmark]
        //public void SearchForeach()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(Count);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";
        //    LocalizationV2 result;
        //    Parallel.ForEach(localizations, x =>
        //    {
        //        if (IsPlate01(x.Car.Plate)
        //    && x.Latitude > 3146 && x.Latitude < 3276
        //    && x.Longitude > 2178 && x.Longitude < 2268)
        //        {
        //            result = x;
        //        }
        //    });
        //}

        //[Benchmark]
        //public void SearchForeach()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";
        //    LocalizationV2 result;
        //    foreach(var x in localizations)
        //    {
        //        if (IsPlate01(x.Car.Plate)
        //    && x.Latitude > 3146 && x.Latitude < 3276
        //    && x.Longitude > 2178 && x.Longitude < 2268)
        //        {
        //            result = x;
        //        }
        //    }         
        //}

        //[Benchmark]
        //public void Search18IsPlateSubstring()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlateSubstring(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268);
        //}

        private bool IsPlateSubstring(string plate)
        {
            return plate.Substring(0, 3).Equals(SearchForPlateInit);
        }

        //[Benchmark]
        //public void Search17FnIsPlateStatic()
        //{
        //    List<LocalizationV2> localizations = new List<LocalizationV2>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV2 localization = new();
        //        localization.Car.Id = i.ToString();
        //        localization.Car.Plate = GetPlate(i);
        //        localization.Latitude = (i * 13);
        //        localization.Longitude = (i * 9);
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Where(_ => IsPlate02(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268).FirstOrDefault();
        //}

        //[Benchmark]
        //public void Search16()
        //{
        //    List<LocalizationV3> localizations = new List<LocalizationV3>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV3 localization = new((i * 13), (i * 9), i.ToString(), GetPlate(i));
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlate(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268);
        //}

        //[Benchmark]
        //public void Search17()
        //{
        //    List<LocalizationV4> localizations = new List<LocalizationV4>(500);

        //    for (int i = 0; i < Count; i++)
        //    {
        //        LocalizationV4 localization = new((i * 13), (i * 9), i, GetPlate(i));
        //        localizations.Add(localization);
        //    }

        //    localizations[247].Car.Plate = "713325";

        //    var result = localizations.Find(_ => IsPlate(_.Car.Plate)
        //    && _.Latitude > 3146 && _.Latitude < 3276
        //    && _.Longitude > 2178 && _.Longitude < 2268);
        //}

        private bool IsPlate(string plate)
        {
            ReadOnlySpan<char> span = plate;
            var sliceled = span.Slice(0, 3);
            ReadOnlySpan<char> toCompare = SearchForPlateInit;
            return sliceled.Equals(toCompare, StringComparison.InvariantCulture);
        }

        private bool IsPlate01(string plate)
        {
            ReadOnlySpan<char> span = plate;
            var sliceled = span.Slice(0, 3);
           
            return sliceled.ToString().Equals(SearchForPlateInit);
        }

        private bool IsPlate0CompareWhitoutEquals(string plate)
        {
            ReadOnlySpan<char> span = plate;
            return span.Slice(0, 3).ToString() == SearchForPlateInit;
        }

        private bool IsPlateSequenceEquals(string plate)
        { 
            const string search = "247";
            ReadOnlySpan<char> span = plate;
            ReadOnlySpan<char> spanConst = search;
            return span.Slice(0, 3).SequenceEqual(spanConst);
        }

        //private static bool IsPlate02(string plate)
        //{
        //    ReadOnlySpan<char> span = plate;
        //    var sliceled = span.Slice(0, 3);

        //    return sliceled.ToString().Equals(SearchForPlateInit);
        //}
    }
}
