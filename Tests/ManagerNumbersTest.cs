using Richter.MutationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Tests
{
    public class ManagerNumbersTest
    {
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 40.0D, 40.0M },
            new object[] { 40.0D, 30.0M },
            new object[] { 40.0D, 50.0M },
            new object[] { 43.0D, 50.0M },
            new object[] { 42.25D, 50.0M },
            new object[] { 2.21D, 50.0M },
            new object[] { 42.24D, 50.0M }
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Test_Caixa_TrocoMaisTotalCompra_Igual_DinheiroDadoPeloCliente(decimal valorCompra, decimal valorEntrada)
        {
            Cashier cashier = new Cashier();

            IList<Money> troco = cashier.GetChangeMoney(valorCompra, valorEntrada);

            decimal total = valorCompra + troco.Sum(x => x.Value);            

            Assert.Equal(total, valorEntrada);
        }
    }
}
