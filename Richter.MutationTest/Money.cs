namespace Richter.MutationTest
{
    public class Money
    {
        public decimal Value { get; private set; }
        public MoneyType MoneyType { get; private set; }

        public Money(decimal value, MoneyType moneyType)
        {
            Value = value;
            MoneyType = moneyType;
        }
    }
}
