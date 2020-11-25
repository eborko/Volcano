using System;
using System.Collections.Generic;
using System.Text;

namespace Volcano.Model
{
    public class Ticket
    {
        public int GameNumber { get; set; }
        public Player Player { get; set; }
        public List<int> Numbers { get; private set; }
        public decimal BetAmmount { get; set; }
        public List<int> JackpootNumbers { get; private set; }
        public decimal JackpotAmmount { get; set; }
        public bool IsPayable 
        { 
            get
            {
                List<int> gameNumbers = new List<int>();
                foreach (var gameStat in GameStatistic.StatisticOfNumbers)
                {
                    if (gameStat.Key == GameNumber)
                    {
                        gameNumbers = gameStat.Value;
                    }
                }

                
                // 35 is number of exited nums
                if (gameNumbers.Count < 35)
                {
                    return false;
                }

                int hitCounter = 0;
                foreach (var num in gameNumbers)
                {
                    if (Numbers!= null && Numbers.Contains(num))
                    {
                        hitCounter++;
                    }
                }
                
                return hitCounter == 6;
            }
        }
        public decimal WinAmount { get; set; }

        public void Cashout()
        {
            if (IsPayable && Player.Account != null)
            {                
                if (IsJackpotTicket())
                {
                    Player.Account.AddMoney(JackpotAmmount + WinAmount);
                }
                else
                {
                    Player.Account.AddMoney(WinAmount);
                }
            }
        }

        public bool IsJackpotTicket()
        {
            return JackpootNumbers.Count == 5;
        }

        public void RegisterJPNumber(int number)
        {
            if (!JackpootNumbers.Contains(number))
            {
                JackpootNumbers.Add(number);
            }
        }

        public bool AddNumber(int num)
        {
            if (Numbers == null)
            {
                Numbers = new List<int>();
            }

            if (Numbers.Contains(num) || Numbers.Count == 6)
            {
                return false;
            }
            
            Numbers.Add(num);
            return true;
        }

        public bool IsValid()
        {
            return Numbers.Count == 6 && Player != null;
        }
    }
}
