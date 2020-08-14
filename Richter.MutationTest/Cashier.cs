using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Richter.MutationTest
{
    public class Cashier
    {
        public readonly IList<MoneyValue> _money = new List<MoneyValue> {
            new MoneyValue(0.01M, MoneyType.Coin),
            new MoneyValue(0.05M, MoneyType.Coin),
            new MoneyValue(0.10M, MoneyType.Coin),
            new MoneyValue(0.25M, MoneyType.Coin),
            new MoneyValue(0.50M, MoneyType.Coin),
            new MoneyValue(1.00M, MoneyType.Coin),
            new MoneyValue(2.00M, MoneyType.BankNote),
            new MoneyValue(5.00M, MoneyType.BankNote),
            new MoneyValue(10.00M, MoneyType.BankNote),
            new MoneyValue(20.00M, MoneyType.BankNote),
            new MoneyValue(50.00M, MoneyType.BankNote),
            new MoneyValue(100.00M, MoneyType.BankNote)
        };

        private IList<MoneyValue> _moneyUnavailable;

        public Cashier(IList<MoneyValue> moneyUnavailable)
        {
            moneyUnavailable = _moneyUnavailable;
        }

        public Cashier()
        {
        }

        public IList<MoneyValue> GetChangeMoney(decimal value, params MoneyValue[] moneyValues)
        {
            return GetChangeMoney(moneyValues.ToList(), new PurchaseValue { Value = value });
        }

        private IList<MoneyValue> GetChangeMoney(List<MoneyValue> clientMoney, PurchaseValue purchaseValue)
        {
            IList<MoneyValue> moneyChange = new List<MoneyValue>();

            decimal totalInput = clientMoney.Sum(x => x.Value);

            if(totalInput < purchaseValue.Value)
            {
                moneyChange.Add(new MoneyValue(totalInput - purchaseValue.Value, MoneyType.Unknown));
                return moneyChange;
            }

            decimal changeMoney = totalInput - purchaseValue.Value;

            while (changeMoney != 0)
            {
                MoneyValue vlr = GetFirstOptionNote(changeMoney);
                moneyChange.Add(vlr);
                changeMoney = changeMoney - vlr.Value;
            }

            return moneyChange;
        }

        private MoneyValue GetFirstOptionNote(decimal changeMoney)
        {
            return _money.Reverse().Where(x => x.Value <= changeMoney).FirstOrDefault();
        }
    }
}
