using IxIgul.BasicVaribles;
using IxIgul.Players;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace IxIgul.Windows
{
    /// <summary>
    /// Interaction logic for Player2Page.xaml
    /// </summary>
    public partial class Player2Page : Window
    {
        #region Varibles
        readonly Player plar = new();
        readonly Opponent opnt = new();
        readonly data dat = new();
        System.Timers.Timer timer = new();
        bool isTie = false;
        int counter = 0;
        int player1Score = 0;
        int player2Score = 0;
        int tieGame = 0;
        bool gameIsStart = false;
        bool gameIsEnd = false;
        #endregion

        #region constructor
        public Player2Page()
        {
            InitializeComponent();


            CheckWinner(ref plar.Shape, ref opnt.Shape);
            WhoWillPlayFirstMessage();
            gameIsStart = true;
        }
        #endregion

        // pleyer2 logic
        #region player2

        #region Tie Game
        private void GameTied(ref bool tie, ref bool plrIsWinner, ref bool cpIsWinner)
        {
            plrIsWinner = false;
            cpIsWinner = false;
            tie = true;
            if (!string.IsNullOrEmpty(btn1.Content as string) &&
                !string.IsNullOrEmpty(btn2.Content as string) &&
                !string.IsNullOrEmpty(btn3.Content as string) &&
                !string.IsNullOrEmpty(btn4.Content as string) &&
                !string.IsNullOrEmpty(btn5.Content as string) &&
                !string.IsNullOrEmpty(btn6.Content as string) &&
                !string.IsNullOrEmpty(btn7.Content as string) &&
                !string.IsNullOrEmpty(btn8.Content as string) &&
                !string.IsNullOrEmpty(btn9.Content as string))
            {
                StopTimer(ref timer);
                MessageBox.Show("The screen is full, so the result is tie", "CLEAN THE SCREEN",
                    MessageBoxButton.OK, MessageBoxImage.None);
                WindowScore(ref plar.Score, ref opnt.Score, ref tieGame);
                ConsistantScore(ref isTie);

                MessageBox.Show("Let me clean the screen\n: - )", "CLEAN THE SCREEN",
                    MessageBoxButton.OK, MessageBoxImage.None);
                // clear board
                ClearBoard();

                ResetAndStartTimer(ref counter, ref timer); // id the game is tied reset the timer
            }
            tie = false;
        }
        #endregion

        #region Clear Board
        private void ClearBoard()
        {
            // reset buttnos content
            btn1.Content = "";
            btn2.Content = "";
            btn3.Content = "";
            btn4.Content = "";
            btn5.Content = "";
            btn6.Content = "";
            btn7.Content = "";
            btn8.Content = "";
            btn9.Content = "";

            // enable to press the buttons
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            btn3.IsEnabled = true;
            btn4.IsEnabled = true;
            btn5.IsEnabled = true;
            btn6.IsEnabled = true;
            btn7.IsEnabled = true;
            btn8.IsEnabled = true;
            btn9.IsEnabled = true;

            // clear red line if there are winner
            rl1.Visibility = Visibility.Collapsed;
            rl2.Visibility = Visibility.Collapsed;
            rl3.Visibility = Visibility.Collapsed;
            rl4.Visibility = Visibility.Collapsed;
            rl5.Visibility = Visibility.Collapsed;
            rl6.Visibility = Visibility.Collapsed;
            rl7.Visibility = Visibility.Collapsed;
            rl8.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Score

        #region Score In Window
        private void WindowScore(ref int plrScore, ref int opnScore, ref int tie)
        {
            if (!plar.IsWinner && !opnt.IsWinner)
            {
                tie++;
            }
            MessageBox.Show("The score is: \nplayer1 - " + plrScore + "\nplayer2 - " + opnScore + "\ntie - " + tie, "SCORE",
                 MessageBoxButton.OK, MessageBoxImage.Information);
            FollowingStartingPlayerMessage();
        }
        #endregion

        #region Consistant Score
        private void ConsistantScore(ref bool tie)
        {
            if (plar.IsWinner)
            {
                player1Score = int.Parse(Player1Score.Text);
                player1Score++;
                Player1Score.Text = player1Score.ToString();
            }
            else if (opnt.IsWinner)
            {
                player2Score = int.Parse(Player2Score.Text);
                player2Score++;
                Player2Score.Text = player2Score.ToString();
            }
            else if (tie)
            {
                tieGame = int.Parse(tieScore.Text);
                tieGame++;
                tieScore.Text = tieGame.ToString();
            }
        }
        #endregion

        #region Reset Score Method
        private void ResetScore(out int plrScore, out int opnScore, out int tie)
        {
            plrScore = 0;
            opnScore = 0;
            tie = 0;
            player1Score = 0;
            player2Score = 0;
            tieGame = 0;
            Player1Score.Text = "0";
            Player2Score.Text = "0";
            tieScore.Text = "0";
        }
        #endregion

        #endregion

        #region Whos Start ToPlay Messages
        /// <summary>
        /// Who Start To Play
        /// </summary>
        private void WhoWillPlayFirstMessage()
        {
            StopTimer(ref timer);
            MessageBoxResult result = MessageBox.Show("WHO WILL PLAY FIRST? for p1 to start press yes, for the p2 to start press no please", "CHOOSE WHO START",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
            }
            ResetAndStartTimer(ref counter, ref timer);
        }

        /// <summary>
        /// Who Will Play First After 1 Game And Foeword
        /// </summary>
        private void FollowingStartingPlayerMessage()
        {
            StopTimer(ref timer);
            MessageBoxResult result = MessageBox.Show("Choose the starting player\nyes - player1 start\nno - player2 start\nIf you want to exit - cancel", "CHOOSE WHO PLAY FIRST",
            MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
                opnt.IsPlay = false;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
                plar.IsPlay = false;
            }
            else if (result == MessageBoxResult.Cancel)
            {
                Application.Current.Shutdown();
            }
            ResetAndStartTimer(ref counter, ref timer);
        }

        private void NewGameStartingPlayerMessage()
        {
            StopTimer(ref timer);
            MessageBoxResult result = MessageBox.Show("For new game choose the starting player\nyes - player start\nno - cp start", "NEW GAME",
          MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
                opnt.IsPlay = false;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
                plar.IsPlay = false;
            }
            ResetAndStartTimer(ref counter, ref timer);
            ResetScore(out plar.Score, out opnt.Score, out tieGame);
        }
        #endregion

        #region start a new game question
        private void StartNewGame(ref System.Timers.Timer t)
        {
            t.Stop();
            MessageBoxResult result = MessageBox.Show("Do you wanna start a new game?", "NEW GAME?",
              MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearBoard(); // reset score
                NewGameStartingPlayerMessage(); // reset board and score
            }
            else
            {
                t.Start();
            }
        }
        #endregion

        #region Who Is Won
        private void IsWin(ref bool gameEnd, ref bool gameStart)
        {
            gameStart = false;
            gameEnd = true;
            string player = "player1";
            string player2 = "player2";
            if (plar.IsWinner)
            {
                MessageBox.Show(player + " win!!", "WINNER", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else if (opnt.IsWinner)
            {
                MessageBox.Show(player2 + " win!!", "WINNER", MessageBoxButton.OK, MessageBoxImage.None);
            }
            WindowScore(ref plar.Score, ref opnt.Score, ref tieGame);
            ConsistantScore(ref isTie);
            ClearBoard();
        }
        #endregion

        #region Check Who Won
        private bool CheckWinner(ref string pl, ref string co)
        {
            // ------- player win -------
            if ((string)btn1.Content == pl && (string)btn2.Content == pl && (string)btn3.Content == pl)
            {
                rl6.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn4.Content == pl && (string)btn5.Content == pl && (string)btn6.Content == pl)
            {
                rl7.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn7.Content == pl && (string)btn8.Content == pl && (string)btn9.Content == pl)
            {
                rl8.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn1.Content == pl && (string)btn4.Content == pl && (string)btn7.Content == pl)
            {
                rl3.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn2.Content == pl && (string)btn5.Content == pl && (string)btn8.Content == pl)
            {
                rl4.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == pl && (string)btn6.Content == pl && (string)btn9.Content == pl)
            {
                rl5.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);

            }
            else if ((string)btn1.Content == pl && (string)btn5.Content == pl && (string)btn9.Content == pl)
            {
                rl1.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == pl && (string)btn5.Content == pl && (string)btn7.Content == pl)
            {
                rl2.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }

            // -------- cp win --------
            if ((string)btn1.Content == co && (string)btn2.Content == co && (string)btn3.Content == co)
            {
                rl6.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);

            }
            else if ((string)btn4.Content == co && (string)btn5.Content == co && (string)btn6.Content == co)
            {
                rl7.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn7.Content == co && (string)btn8.Content == co && (string)btn9.Content == co)
            {
                rl8.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn1.Content == co && (string)btn4.Content == co && (string)btn7.Content == co)
            {
                rl3.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn2.Content == co && (string)btn5.Content == co && (string)btn8.Content == co)
            {
                rl4.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == co && (string)btn6.Content == co && (string)btn9.Content == co)
            {
                rl5.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn1.Content == co && (string)btn5.Content == co && (string)btn9.Content == co)
            {
                rl1.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == co && (string)btn5.Content == co && (string)btn7.Content == co)
            {
                rl2.Visibility = Visibility.Visible; // Make red line visible
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                IsWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            GameTied(ref isTie, ref plar.IsWinner, ref opnt.IsWinner);

            return false;
        }
        #endregion

        #endregion
        // pleyer2 logic

        // buttons event
        #region buttons events
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            btn1.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn1.Content = plar.Shape;
                }
                else
                {
                    btn1.Content = opnt.Shape;
                }
                btn1.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay; // change turn
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            btn2.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn2.Content = plar.Shape;
                }
                else
                {
                    btn2.Content = opnt.Shape;
                }
                btn2.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
            
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            btn3.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn3.Content = plar.Shape;
                }
                else
                {
                    btn3.Content = opnt.Shape;
                }
                btn3.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
           
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            btn4.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn4.Content = plar.Shape;
                }
                else
                {
                    btn4.Content = opnt.Shape;
                }
                btn4.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
           
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            btn5.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn5.Content = plar.Shape;
                }
                else
                {
                    btn5.Content = opnt.Shape;
                }
                btn5.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
            
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            btn6.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn6.Content = plar.Shape;
                }
                else
                {
                    btn6.Content = opnt.Shape;
                }
                btn6.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
            
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            btn7.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn7.Content = plar.Shape;
                }
                else
                {
                    btn7.Content = opnt.Shape;
                }
                btn7.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
            
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            btn8.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn8.Content = plar.Shape;
                }
                else
                {
                    btn8.Content = opnt.Shape;
                }
                btn8.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
            
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            btn9.Content = plar.Shape;
                if (plar.IsPlay)
                {
                    btn9.Content = plar.Shape;
                }
                else
                {
                    btn9.Content = opnt.Shape;
                }
                btn9.IsEnabled = false;
                plar.IsPlay = !plar.IsPlay;
            
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }
        #endregion
        // buttons event

        // menu bar logic
        #region menu bar
        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            ClearBoard(); // clear the board
            NewGameStartingPlayerMessage(); // start new game
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // exit game
        }

        /// <summary>
        /// allowing p2 to play
        /// </summary>
        private void p2Play_Click(object sender, RoutedEventArgs e)
        {
            StopTimer(ref timer);
            ClearBoard();
            MessageBoxResult result = MessageBox.Show("Choose the starting player\nyes - player1 start\nno - player2 start", "CHOOSE WHO PLAY FIRST",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
                opnt.IsPlay = false;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
                plar.IsPlay = false;
            }
            ResetScore(out plar.Score, out opnt.Score, out tieGame);
            ResetAndStartTimer(ref counter, ref timer);
        }

        /// <summary>
        /// allowing cp to play
        /// </summary>
        private void cpPlay_Click(object sender, RoutedEventArgs e)
        {
            ClearBoard();
            NewGameStartingPlayerMessage(); // start new game
        }

        #region Choose Shape
        private void O_Shape_Click(object sender, RoutedEventArgs e)
        {
            plar.Shape = "O";
            opnt.Shape = "X";
            ClearBoard();
            NewGameStartingPlayerMessage();
        }

        private void X_Shape_Click(object sender, RoutedEventArgs e)
        {
            plar.Shape = "X";
            opnt.Shape = "O";
            ClearBoard();
            NewGameStartingPlayerMessage();
        }
        #endregion

        #region Change Shapes Text Color
        private void Blue_shapes_Click(object sender, RoutedEventArgs e)
        {
            btn1.Foreground = Brushes.Blue;
            btn2.Foreground = Brushes.Blue;
            btn3.Foreground = Brushes.Blue;
            btn4.Foreground = Brushes.Blue;
            btn5.Foreground = Brushes.Blue;
            btn6.Foreground = Brushes.Blue;
            btn7.Foreground = Brushes.Blue;
            btn8.Foreground = Brushes.Blue;
            btn9.Foreground = Brushes.Blue;
        }

        private void Green_shapes_Click(object sender, RoutedEventArgs e)
        {
            btn1.Foreground = Brushes.Green;
            btn2.Foreground = Brushes.Green;
            btn3.Foreground = Brushes.Green;
            btn4.Foreground = Brushes.Green;
            btn5.Foreground = Brushes.Green;
            btn6.Foreground = Brushes.Green;
            btn7.Foreground = Brushes.Green;
            btn8.Foreground = Brushes.Green;
            btn9.Foreground = Brushes.Green;
        }

        private void Yellow_shapes_Click(object sender, RoutedEventArgs e)
        {
            btn1.Foreground = Brushes.Yellow;
            btn2.Foreground = Brushes.Yellow;
            btn3.Foreground = Brushes.Yellow;
            btn4.Foreground = Brushes.Yellow;
            btn5.Foreground = Brushes.Yellow;
            btn6.Foreground = Brushes.Yellow;
            btn7.Foreground = Brushes.Yellow;
            btn8.Foreground = Brushes.Yellow;
            btn9.Foreground = Brushes.Yellow;
        }

        private void Red_shapes_Click(object sender, RoutedEventArgs e)
        {
            btn1.Foreground = Brushes.Red;
            btn2.Foreground = Brushes.Red;
            btn3.Foreground = Brushes.Red;
            btn4.Foreground = Brushes.Red;
            btn5.Foreground = Brushes.Red;
            btn6.Foreground = Brushes.Red;
            btn7.Foreground = Brushes.Red;
            btn8.Foreground = Brushes.Red;
            btn9.Foreground = Brushes.Red;
        }

        private void Black_shapes_Click(object sender, RoutedEventArgs e)
        {
            btn1.Foreground = Brushes.Black;
            btn2.Foreground = Brushes.Black;
            btn3.Foreground = Brushes.Black;
            btn4.Foreground = Brushes.Black;
            btn5.Foreground = Brushes.Black;
            btn6.Foreground = Brushes.Black;
            btn7.Foreground = Brushes.Black;
            btn8.Foreground = Brushes.Black;
            btn9.Foreground = Brushes.Black;
        }
        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new();
            mn.Show();
            Close();
        }
        #endregion
        //menu bar logic

        // timer
        #region Timer
        private void StartTimer(ref System.Timers.Timer t)
        {
            t.Interval = 1000;
            t.Elapsed += OnTimerElapsed;
            t.Start();
        }
        private static void StopTimer(ref System.Timers.Timer t)
        {
            t.Stop();
        }
        private void ResetAndStartTimer(ref int count, ref System.Timers.Timer t)
        {
            count = 0; // reset counter to 0

            t.Stop(); // stop timer
            t.Interval = 1000; // reset timer interval to 1 second at a time
            t.Start(); // start timer

            gameTimer.Text = " Seconds passed - " + count; // update the UI
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            counter++;
            gameTimer.Dispatcher.Invoke(() =>
            {
                gameTimer.Text = "Seconds passed - " + counter;
            });
        }
        #endregion
        // timer
    }
}