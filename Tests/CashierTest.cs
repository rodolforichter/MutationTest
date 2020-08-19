using Richter.MutationModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class CashierTest
    {
        #region Attributes
        public static IEnumerable<object[]> FullAvailableChangeMoney =>
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

        public static IEnumerable<object[]> UnavailableChangeMoney =>
        new List<object[]>
        {
            new object[] { 40.0D, 40.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 40.0D, 30.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 40.0D, 50.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 43.0D, 50.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 42.25D, 50.0M, new List<Money> { { new Money(5.0M, MoneyType.BankNote) } } },
            new object[] { 2.21D, 50.0M, new List<Money> { { new Money(10.0M, MoneyType.BankNote) } } },
            new object[] { 42.24D, 50.0M, new List<Money> { { new Money(2.0M, MoneyType.BankNote) }, {new Money(0.25M, MoneyType.Coin) } } } };

        #endregion

        [Theory]
        [MemberData(nameof(FullAvailableChangeMoney))]
        public void Test_Cashier_Full_Available_ChangeMoney_ShouldBeOk(decimal valorCompra, decimal valorEntrada)
        {
            Cashier cashier = new Cashier();

            IList<Money> troco = cashier.GetChangeMoney(valorCompra, valorEntrada);

            decimal total = valorCompra + troco.Sum(x => x.Value);

            Assert.Equal(total, valorEntrada);
        }

        [Theory]
        [MemberData(nameof(UnavailableChangeMoney))]
        public void Test_Cashier_UnAvailable_ChangeMoney_ShouldBeOK(decimal valorCompra, decimal valorEntrada, List<Money> unAvailableChangeMoney)
        {
            Cashier cashier = new Cashier(unAvailableChangeMoney);

            IList<Money> troco = cashier.GetChangeMoney(valorCompra, valorEntrada);

            decimal total = valorCompra + troco.Sum(x => x.Value);

            bool resultNotContainMoney = unAvailableChangeMoney.TrueForAll(x => !troco.Contains(x));

            Assert.True(resultNotContainMoney);

            Assert.Equal(total, valorEntrada);
        }

        [Fact]
        public void Test_ChangeMoney_Exceed_Max_Qt_WaitFor_Exception()
        {
            IList<Money> unAvailable = new List<Money> {
                { new Money(10.0M, MoneyType.BankNote) },
                { new Money(5.0M, MoneyType.BankNote) },
                { new Money(2.0M, MoneyType.BankNote) },
                { new Money(1.0M, MoneyType.Coin) },
                { new Money(0.50M, MoneyType.Coin) },
                { new Money(0.25M, MoneyType.Coin) },
                { new Money(0.10M, MoneyType.Coin) }
            };

            Cashier cashier = new Cashier(unAvailable);
            Assert.Throws<InvalidChangeMoneyException>(() => cashier.GetChangeMoney(40.0M, 50.0M));
        }
    }
}
