using System;
using System.Collections.Generic;
using System.Linq;

namespace Richter.MutationTest
{
    public class Cashier
    {
        #region Attributes

        private IList<MoneyValue> _money = new List<MoneyValue> {
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

        #endregion

        public Cashier(IList<MoneyValue> moneyUnavailable)
        {
            moneyUnavailable = _moneyUnavailable;
        }

        public Cashier()
        {
        }

        /// <summary>
        /// Obtém o troco do cliente.
        /// </summary>
        /// <param name="purchaseValue">purchaseValue</param>
        /// <param name="enterValue">enterValue</param>
        /// <returns>IList<MoneyValue></returns>
        public IList<MoneyValue> GetChangeMoney(decimal purchaseValue, decimal enterValue)
        {
            return GetChangeMoney(new PurchaseValue { Value = purchaseValue }, enterValue);
        }

        /// <summary>
        /// Método que obtém o troco do cliente.
        /// </summary>
        /// <param name="purchaseValue">purchaseValue</param>
        /// <param name="enterValue">enterValue</param>
        /// <returns>IList<MoneyValue></returns>
        private IList<MoneyValue> GetChangeMoney(PurchaseValue purchaseValue, decimal enterValue)
        {
            IList<MoneyValue> moneyChange = new List<MoneyValue>();

            if(enterValue < purchaseValue.Value)
            {
                moneyChange.Add(new MoneyValue(enterValue - purchaseValue.Value, MoneyType.Unknown));
                return moneyChange;
            }

            decimal changeMoney = enterValue - purchaseValue.Value;

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
