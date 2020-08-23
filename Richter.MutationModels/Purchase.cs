using System;

namespace Richter.MutationModels
{
    public struct Purchase
    {
        public decimal Value { get; private set; }

        public Purchase(decimal value)
        {
            Value = value;
            CheckValue();
        }

        private void CheckValue()
        {
            if(Value <= 0)
            {
                throw new InvalidPurchaseValueException();
            }
        }
    }
}
