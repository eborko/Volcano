using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Volcano
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _random;

        private List<Model.Player> _players;
        private int _luckeyPosition1;
        private int _luckeyPosition2;

        private List<int> _numbersInGame;
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            PrepareView();
            InitAll();
            SetWildPositions();
        }

        private void InitAll()
        {
            _random = new Random();

            // Init numbers from 1 to 48
            _numbersInGame = new List<int>();
            for (int i = 1; i < 49; i++)
            {
                _numbersInGame.Add(i);
            }
            _players = new List<Model.Player>();


            #region TetsPlayers
            // Lets say we have those players
            _players.Add(new Model.Player("Borko"));
            _players.Add(new Model.Player("Teodora"));
            _players.Add(new Model.Player("Mico"));
            _players.Add(new Model.Player("Zeljko"));
            _players.Add(new Model.Player("Vukoman"));
            _players.Add(new Model.Player("Milica"));
            _players.Add(new Model.Player("Ivana"));

            List<string> playerNames = new List<string> { "Borko", 
                "Teodora", 
                "Mico", 
                "Zeljko", 
                "Vukoman",
                "Milica",
                "Ivana"
            };

            cbPlayerName.ItemsSource = playerNames;
            #endregion
        }

        private void SetWildPositions()
        {
            int luckeyPosition1 = _random.Next(1, 36);
            int luckeyPosition2 = 0;

            // Be sure that we set two distinct positions
            // and legal value for luckeyPosition2
            while (luckeyPosition1 == luckeyPosition2 || luckeyPosition2 == 0)
            {
                luckeyPosition2 = _random.Next(1, 36);
            }

            ((Label)this.FindName($"lblNum{luckeyPosition1}")).Content = "*";
            ((Label)this.FindName($"lblNum{luckeyPosition2}")).Content = "*";

            // Init fields
            _luckeyPosition1 = luckeyPosition1;
            _luckeyPosition2 = luckeyPosition2;
        }

        private void PrepareView()
        {
            ClearAllNumericLabels();
        }

        private void ClearAllNumericLabels()
        {
            // Clear history of gambling
            for (int i = 1; i < 36; i++)
            {
                ((Label)this.FindName($"lblNum{i}")).Content = "";
            }

            // Clear current number
            lblCurrentNumber.Content = "";
        }

        private void btnSaveTicket_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket();
            Model.Ticket mTicket = new Model.Ticket();
            mTicket.BetAmmount = 1.0m;

            for (int i = 1; i < 7; i++)
            {
                TextBox tbTemp = ((TextBox)this.FindName($"tbBetNum{i}"));
                int number = Int32.Parse(tbTemp.Text);

                // validation
                if (number < 1 || number > 49)
                {
                    return;
                }
                tbTemp.Text = "";

                ((Label)ticket.FindName($"lblBetNum{i}")).Content = number.ToString();

                mTicket.AddNumber(number);
            }

            foreach (var player in _players)
            {
                if (player.Name.Equals(cbPlayerName.SelectedItem.ToString()))
                {
                    player.AddTicket(mTicket);
                    mTicket.Player = player;

                    ticket.lblPlayerName.Content = player.Name;
                }
            }

            ticket.lblBetAmmount.Content = mTicket.BetAmmount.ToString();
            spTicketContainer.Children.Add(ticket);


        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
            await Task.Run(() =>
            {
                // Game Loop
                for (int i = 0; i < 35; i++)
                {
                    int index = _random.Next(0, _numbersInGame.Count);
                    int number = _numbersInGame.ToArray()[index];
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        lblCurrentNumber.Content = number.ToString();
                    
                        (this.FindName($"lblNum{i + 1}") as Label).Content = number.ToString();

                        Thread.Sleep(500);
                        lblCurrentNumber.Content = "";

                        _numbersInGame.RemoveAt(index);
                    }));
                }
            });
        }

    }
}
