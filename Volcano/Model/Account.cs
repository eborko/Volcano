using System;
using System.Collections.Generic;
using System.Text;

namespace Volcano.Model
{
    class Account
    {
        public Player Owner { get; set; }
        public decimal Saldo { get; private set; }
        public void AddMoney(decimal moneyAmmount)
        {
            Saldo += moneyAmmount;
        }
        public void Pay(decimal moneyAmmount)
        {
            Saldo -= moneyAmmount;
        }
    }
}
