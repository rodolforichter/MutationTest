using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET5
{
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [RankColumn]
    public class SpanClassTest
    {
        //[Benchmark]
        //public void TestList()
        //{
        //    List<string> numbers = new();
        //    List<int> Numbers2 = new();
        //    for (int i = 0; i < 500; i++)
        //    {
        //        numbers.Add(i.ToString());
        //    }

        //    for (int i = 0; i < 500; i++)
        //    {
        //        int num = int.Parse(numbers[i]);
        //        Numbers2.Add(num);
        //    }
        //}

        [Benchmark]
        public void TestList2()
        {
            List<string> numbers = new();
            

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i.ToString());             
            }

            Span<string> spanStr = new Span<string>(numbers.ToArray());

            var numero = spanStr.BinarySearch(new CompareString());
        }

        [Benchmark]
        public void TestList3()
        {
            List<string> numbers = new(500);


            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i.ToString());
            }

            _ = numbers.Where(_ => _.Equals("249")).FirstOrDefault();
        }

        [Benchmark]
        public void TestList4()
        {
            List<string> numbers = new();

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i.ToString());
            }

            var numero = numbers.BinarySearch("249");
        }

        [Benchmark]
        public void TestList5()
        {
            List<string> numbers = new();

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i.ToString());
            }

            Span<string> spanStr = new Span<string>(numbers.ToArray());

            var numero = spanStr.BinarySearch("249",new CompareSpanItem());
        }

        [Benchmark]
        public void TestList6()
        {
            List<string> numbers = new();

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i.ToString());
            }

            _ = new ReadOnlySpan<string>(numbers.ToArray()).BinarySearch("249", new CompareSpanItem());
        }

        [Benchmark]
        public void TestList7()
        {
            List<int> numbers = new();

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i);
            }

            _ = new ReadOnlySpan<int>(numbers.ToArray()).BinarySearch(249, new CompareSpanItemInt());
        }

        [Benchmark]
        public void TestList8()
        {
            Collection<int> numbers = new();

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i);
            }

            _ = new ReadOnlySpan<int>(numbers.ToArray()).BinarySearch(249, new CompareSpanItemInt());
        }

        [Benchmark]
        public void TestList9()
        {
            List<int> numbers = new(500);

            for (int i = 0; i < 500; i++)
            {
                numbers.Add(i);
            }

            _ = new ReadOnlySpan<int>(numbers.ToArray()).BinarySearch(249, new CompareSpanItemInt());
        }
    }

    public sealed class CompareSpanItemInt : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x.Equals(y))
            {
                return 0;
            }
            
            return -1;
        }    
    }

    public sealed class CompareSpanItem : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Equals(y))
            {
                return 0;
            }

            return -1;
        }
    }
}
