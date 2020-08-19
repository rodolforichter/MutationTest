using System;
using System.Collections.Generic;
using System.Linq;

namespace Richter.MutationModels
{
    public class Cashier
    {
        #region Attributes

        public const int MaxMoneyTypeItemsCount = 50;

        private IList<Money> _money = new List<Money> {
            new Money(0.01M, MoneyType.Coin),
            new Money(0.05M, MoneyType.Coin),
            new Money(0.10M, MoneyType.Coin),
            new Money(0.25M, MoneyType.Coin),
            new Money(0.50M, MoneyType.Coin),
            new Money(1.00M, MoneyType.Coin),
            new Money(2.00M, MoneyType.BankNote),
            new Money(5.00M, MoneyType.BankNote),
            new Money(10.00M, MoneyType.BankNote),
            new Money(20.00M, MoneyType.BankNote),
            new Money(50.00M, MoneyType.BankNote),
            new Money(100.00M, MoneyType.BankNote)
        };

        private IList<Money>  _moneyUnavailable = new List<Money>();

        #endregion

        public Cashier(IList<Money> moneyUnavailable)
        {
            _moneyUnavailable = moneyUnavailable;
        }

        public Cashier()
        {   
        }

        /// <summary>
        /// Obtém o troco do cliente.
        /// </summary>
        /// <param name="purchaseValue">purchaseValue</param>
        /// <param name="enterValue">enterValue</param>
        /// <returns>IList<Money></returns>
        public IList<Money> GetChangeMoney(decimal purchaseValue, decimal enterValue)
        {
            return GetChangeMoney(new PurchaseValue { Value = purchaseValue }, enterValue);
        }

        /// <summary>
        /// Método que obtém o troco do cliente.
        /// </summary>
        /// <param name="purchaseValue">purchaseValue</param>
        /// <param name="enterValue">enterValue</param>
        /// <returns>IList<Money></returns>
        private IList<Money> GetChangeMoney(PurchaseValue purchaseValue, decimal enterValue)
        {
            IList<Money> moneyChange = new List<Money>();

            if(enterValue < purchaseValue.Value)
            {
                moneyChange.Add(new Money(enterValue - purchaseValue.Value, MoneyType.Unknown));
                return moneyChange;
            }

            decimal changeMoney = enterValue - purchaseValue.Value;

            while (changeMoney != 0)
            {
                Money vlr = GetFirstOptionNote(changeMoney);

                if (_moneyUnavailable != null && _moneyUnavailable.Contains(vlr))
                {
                    _money.Remove(vlr);
                    continue;
                }
                moneyChange.Add(vlr);
                changeMoney = changeMoney - vlr.Value;
            }

            CheckMaxQuantityMoneyType(moneyChange);

            return moneyChange;
        }

        private void CheckMaxQuantityMoneyType(IList<Money> moneyChange)
        {
            if (moneyChange != null)
            {
                var countByMoneyType = from t in moneyChange
                                       group t by t.MoneyType into g
                                       select g.Count();

                foreach (var count in countByMoneyType)
                {
                    if (count > MaxMoneyTypeItemsCount)
                    {
                        throw new InvalidChangeMoneyException();
                    }
                }
            }
        }

        private Money GetFirstOptionNote(decimal changeMoney)
        {
            return _money.Reverse().Where(x => x.Value <= changeMoney).FirstOrDefault();
        }
    }
}
