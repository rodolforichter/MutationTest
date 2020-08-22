using System;
using System.Collections.Generic;
using System.Text;

namespace Richter.MutationModels
{
    public class Customer
    {
        public decimal Value { get; private set; }

        public Customer(decimal value)
        {
            Value = value;
            CheckValue();
        }

        private void CheckValue()
        {
            if (Value <= 0)
            {
                throw new InvalidCustomerValueException();
            }
        }
    }
}
