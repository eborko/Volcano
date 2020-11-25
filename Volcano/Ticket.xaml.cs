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
        private Model.Ticket ticket;
        public Ticket()
        {
            InitializeComponent();
            Model.Player player = new Model.Player("Borko");
            ticket = new Model.Ticket();
            ticket.Player = player;
        }
    }
}
