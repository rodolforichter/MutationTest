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
        [Theory]
        [MemberData(nameof(Data))]
        public void TestCashier_ShouldBeEqual(decimal valorCompra, params MoneyValue[] moneyValues)
        {
            Cashier cashier = new Cashier();

            IList<MoneyValue> troco = cashier.GetChangeMoney(valorCompra, moneyValues);

            decimal total = valorCompra + troco.Sum(x => x.Value);

            decimal moneyClient =  moneyValues.Sum(x => x.Value);

            Assert.Equal(total, moneyClient);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 40.0D, new MoneyValue[]{ new MoneyValue(30.0M, MoneyType.BankNote) } },
            new object[] { 40.0D, new MoneyValue[]{ new MoneyValue(50.0M, MoneyType.BankNote) } },
            new object[] { 43.0D, new MoneyValue[]{ new MoneyValue(50.0M, MoneyType.BankNote) } },
            new object[] { 42.25D, new MoneyValue[]{ new MoneyValue(50.0M, MoneyType.BankNote) } }
        };
    }
}
