//using System;
//using System.Collections.Generic;

//namespace NET5.Gps
//{
//    internal class Comparator : IComparer<Localization>
//    {
//        private string SearchForPlateInit = "713";
//        private (int init, int final) _pointLatitude = (3146, 3276);
//        private (int init, int final) _pointLongitude = (2178, 2268);

//        public int Compare(Localization x, Localization y)
//        {
//            throw new NotImplementedException();
//        }

//        public int CompareTo(Localization other)
//        {
//            if (other.Car.Plate.Substring(0, 3).Equals(SearchForPlateInit) && int.Parse(other.Latitude) > _pointLatitude.init && int.Parse(other.Latitude) < _pointLatitude.final
//                && int.Parse(other.Longitude) > _pointLongitude.init && int.Parse(other.Longitude) < _pointLongitude.final)
//            {
//                return 0;
//            }

//            return -1;
//        }
//    }
//}