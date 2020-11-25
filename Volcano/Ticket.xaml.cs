using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Volcano
{
    /// <summary>
    /// Interaction logic for Ticket.xaml
    /// </summary>
    public partial class Ticket : UserControl
    {
        private Random _random;
        public Model.Ticket MTicket { get; set; }
        private KeyValuePair<int, List<int>> bettingCircle;

        public Ticket()
        {
            InitializeComponent();

            _random = new Random();
            bettingCircle = new KeyValuePair<int, List<int>>();
        }

        public void UpdateNumbersInfo(int number)
        {
            if (MTicket.Numbers.Contains(number))
            {
                UpdateTicketNumbers(number);
                
                //bettingCircle.Value.Add(number);
            }
        }

        private void UpdateTicketNumbers(int number)
        {
            for (int i = 1; i < 7; i++)
            {
                Label label = (Label)this.FindName($"lblBetNum{i}");
                if (number == Int32.Parse(label.Content.ToString()))
                {
                    label.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }
    }
}
