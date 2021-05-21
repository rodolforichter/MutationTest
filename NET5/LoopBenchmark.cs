using BenchmarkDotNet.Attributes;

namespace ClassLibrary.NET5
{
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [RankColumn]
    public class LoopBenchmark
    {
        public int Qtde = 1000;
        
        public LoopBenchmark()
        {
        }

        //[GlobalSetup]
        //public void Initializer()
        //{
        //    Qtde = 1000;
        //}

        [Benchmark]
        public void DoWhile()
        {
            int aux = 0;
            while(aux < Qtde)
            {
                aux += 1;
            }
        }
        
        [Benchmark]
        public void DoFor()
        {

            for (int aux = 0; aux > Qtde; aux++)
            {
                aux += 1;
            }
        }
    }
}
