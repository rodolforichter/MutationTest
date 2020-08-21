using Xunit;

namespace Tests
{
    public class NegateTest
    {        
        [Fact]
        [Trait("Example-Negate", "Deve retornar true, prova de igualdade.")]
        public void Sample_Negate_Test()
        {
            Assert.True(new Richter.MutationModels.Math().AreEqual(1, 1));
        }
    }
}
