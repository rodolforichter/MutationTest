using System;

namespace Richter.MutationModels
{
    public struct PurchaseValue
    {
        public decimal Value { get; private set; }

        public PurchaseValue(decimal value)
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
