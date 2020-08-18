using System;
using System.Diagnostics.CodeAnalysis;

namespace Richter.MutationTest
{
    public class Money : IEquatable<Money>
    {
        public decimal Value { get; private set; }
        public MoneyType MoneyType { get; private set; }

        public Money(decimal value, MoneyType moneyType)
        {
            Value = value;
            MoneyType = moneyType;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() + MoneyType.GetHashCode();
        }

        public bool Equals([AllowNull] Money other)
        {
            return this.Value == other.Value && this.MoneyType == other.MoneyType;
        }
    }
}
