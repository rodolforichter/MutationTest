using Richter.MutationModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class PurchaseValueTest
    {
        [Fact]
        [Trait("Wait-For-Exception", "Valor da compra menor ou igual a 0")]
        public void Test_Ctor_PurchaseValue_MinorOrEqual_Zero_WaitFor_Exception()
        {
            Assert.Throws<InvalidPurchaseValueException>(() => new Purchase(0.0M));
        }
    }
}
