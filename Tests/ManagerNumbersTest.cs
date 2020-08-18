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
        public static IEnumerable<object[]> DataAllValuesAvailable =>
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

        public static IEnumerable<object[]> DataAllValuesUnAvailable =>
        new List<object[]>
        {
            new object[] { 40.0D, 40.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 40.0D, 30.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 40.0D, 50.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 43.0D, 50.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 42.25D, 50.0M, new List<Money> { { new Money(5.0M, MoneyType.BankNote) } } },
            new object[] { 2.21D, 50.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 42.24D, 50.0M, new List<Money> { { new Money(2.0M, MoneyType.BankNote) }, {new Money(0.25M, MoneyType.Coin) } } } };

        [Theory]
        [MemberData(nameof(DataAllValuesAvailable))]
        public void Test_Caixa_TrocoMaisTotalCompra_Igual_DinheiroDadoPeloCliente(decimal valorCompra, decimal valorEntrada)
        {
            Cashier cashier = new Cashier(null);

            IList<Money> troco = cashier.GetChangeMoney(valorCompra, valorEntrada);

            decimal total = valorCompra + troco.Sum(x => x.Value);            

            Assert.Equal(total, valorEntrada);
        }

        [Theory]
        [MemberData(nameof(DataAllValuesUnAvailable))]
        public void Test_Caixa_NotaOuMoedaIndisponivel_TrocoMaisTotalCompra_Igual_DinheiroDadoPeloCliente(decimal valorCompra, decimal valorEntrada, List<Money> trocoIndisponivel)
        {
            Cashier cashier = new Cashier(trocoIndisponivel);

            IList<Money> troco = cashier.GetChangeMoney(valorCompra, valorEntrada);

            decimal total = valorCompra + troco.Sum(x => x.Value);

            bool resultNotContainMoney = trocoIndisponivel.TrueForAll(x => !troco.Contains(x));

            Assert.True(resultNotContainMoney);

            Assert.Equal(total, valorEntrada);
        }
    }
}
