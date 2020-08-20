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
            return GetChangeMoney(new PurchaseValue(purchaseValue), new CustomerValue(enterValue));
        }

        /// <summary>
        /// Método que obtém o troco do cliente.
        /// </summary>
        /// <param name="purchaseValue">purchaseValue</param>
        /// <param name="enterValue">enterValue</param>
        /// <returns>IList<Money></returns>
        private IList<Money> GetChangeMoney(PurchaseValue purchaseValue, CustomerValue customerValue)
        {
            IList<Money> moneyChange = new List<Money>();

            decimal changeMoney = customerValue.Value - purchaseValue.Value;

            while (changeMoney != 0)
            {
                Money vlr = GetFirstOptionNote(changeMoney);

                if (ContainsUnavailableMoney(vlr))
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

        /// <summary>
        /// Verifica se existem notas ou moedas indisponíveis no caixa.
        /// </summary>
        /// <param name="vlr">Money</param>
        /// <returns>bool</returns>
        private bool ContainsUnavailableMoney(Money vlr)
        {
            return _moneyUnavailable != null && _moneyUnavailable.Contains(vlr);
        }

        /// <summary>
        /// Verifica se a quantidade de notas que está sendo retornada é absurda.
        /// </summary>
        /// <param name="moneyChange">IList<Money></param>
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

        /// <summary>
        /// Obtém a Nota ou Moeda necessária para compor o troco.
        /// </summary>
        /// <param name="changeMoney"></param>
        /// <returns></returns>
        private Money GetFirstOptionNote(decimal changeMoney)
        {
            return _money.Reverse().Where(x => x.Value <= changeMoney).FirstOrDefault();
        }
    }
}
