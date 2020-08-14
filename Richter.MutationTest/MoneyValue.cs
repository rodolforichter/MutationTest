namespace Richter.MutationTest
{
    public class MoneyValue
    {
        public decimal Value { get; private set; }
        public MoneyType MoneyType { get; private set; }

        public MoneyValue(decimal value, MoneyType moneyType)
        {
            Value = value;
            MoneyType = moneyType;
        }
    }
}
