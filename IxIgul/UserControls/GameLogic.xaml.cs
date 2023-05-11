using IxIgul.Players;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IxIgul.UserControls
{
    /// <summary>
    /// Interaction logic for GameLogic.xaml
    /// </summary>
    public partial class GameLogic : UserControl
    {
        p2 plar = new();
        oponent opnt = new();
        string pl1 = "O";
        string pl2 = "X";
        bool oponent = true;

        public GameLogic()
        {
            InitializeComponent();

            CheckWinner(ref pl1, ref pl2);
            WhoWillPlayFirstMessage();
        }

        // pleyer2
        #region TieGame
        private void GameTied(ref string pl, ref string co)
        {
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
                MessageBox.Show("The screen is full, so the result is tie", "CLEAN THE SCREEN",
                    MessageBoxButton.OK, MessageBoxImage.None);
                score();

                MessageBox.Show("Let me clean the screen\n: - )", "CLEAN THE SCREEN",
                    MessageBoxButton.OK, MessageBoxImage.None);
                // clear board
                ClearBoard(ref pl1, ref pl2);
            }
        }
        #endregion

        #region ClearBoard
        private void ClearBoard(ref string pl, ref string p2)
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
        }
        #endregion

        #region Score
        private void score()
        {
            MessageBox.Show("The score is: \nplayer1 - " + plar.Score + "\nplayer2 - " + opnt.Score, "SCORE",
                 MessageBoxButton.OK, MessageBoxImage.Information);
            FollowingStartingPlayerMessage();
            ResetButtonsBackgroundColor();
        }
        #endregion

        #region WhosStartToPlayMessage
        /// <summary>
        /// Who Start To Play
        /// </summary>
        private void WhoWillPlayFirstMessage()
        {
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
        }

        /// <summary>
        /// Who Will Play First After 1 Game And Foeword
        /// </summary>
        private void FollowingStartingPlayerMessage()
        {
            MessageBoxResult result = MessageBox.Show("Choose the starting player\nyes - player1 start\nno - player2 start\nIf you want to exit - cancel", "CHOOSE WHO PLAY FIRST",
           MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
            }
            else if (result == MessageBoxResult.Cancel)
            {
                Application.Current.Shutdown();
            }
        }

        private void NewGameStartingPlayerMessage()
        {
            MessageBoxResult result = MessageBox.Show("For new game choose the starting player\nyes - player start\nno - cp start", "NEW GAME",
          MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
            }
        }
        #endregion

        #region buttons background colors reset
        private void ResetButtonsBackgroundColor()
        {
            btn1.Background = Brushes.White;
            btn2.Background = Brushes.White;
            btn3.Background = Brushes.White;
            btn4.Background = Brushes.White;
            btn5.Background = Brushes.White;
            btn6.Background = Brushes.White;
            btn7.Background = Brushes.White;
            btn8.Background = Brushes.White;
            btn9.Background = Brushes.White;
        }
        #endregion

        #region WhoIsWon
        private void isWin()
        {
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
            score();
            ClearBoard(ref pl1, ref pl2);
        }
        #endregion

        #region ChekWhoWon
        private bool CheckWinner(ref string pl, ref string co)
        {
            // ------- p win -------
            if ((string)btn1.Content == pl && (string)btn2.Content == pl && (string)btn3.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();
            }
            else if ((string)btn4.Content == pl && (string)btn5.Content == pl && (string)btn6.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }
            else if ((string)btn7.Content == pl && (string)btn8.Content == pl && (string)btn9.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }
            else if ((string)btn1.Content == pl && (string)btn4.Content == pl && (string)btn7.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }
            else if ((string)btn2.Content == pl && (string)btn5.Content == pl && (string)btn8.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }
            else if ((string)btn3.Content == pl && (string)btn6.Content == pl && (string)btn9.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }
            else if ((string)btn1.Content == pl && (string)btn5.Content == pl && (string)btn9.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }
            else if ((string)btn3.Content == pl && (string)btn5.Content == pl && (string)btn7.Content == pl)
            {
                plar.IsWinner = true;
                plar.Score++;
                isWin();

            }

            // -------- cp win --------
            if ((string)btn1.Content == co && (string)btn2.Content == co && (string)btn3.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn4.Content == co && (string)btn5.Content == co && (string)btn6.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn7.Content == co && (string)btn8.Content == co && (string)btn9.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn1.Content == co && (string)btn4.Content == co && (string)btn7.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn2.Content == co && (string)btn5.Content == co && (string)btn8.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn3.Content == co && (string)btn6.Content == co && (string)btn9.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn1.Content == co && (string)btn5.Content == co && (string)btn9.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn3.Content == co && (string)btn5.Content == co && (string)btn7.Content == co)
            {
                opnt.IsWinner = true;
                opnt.Score++;
                isWin();
            }
            GameTied(ref pl1, ref pl2);

            return false;
        }
        #endregion

        #region buttons events
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn1.Content = pl1;
                }
                else
                {
                    btn1.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay; // change turn
            }
            else if (oponent == false)
            {
                btn1.Content = pl1;
                CpPlace(ref pl2);
            }
            btn1.IsEnabled = false;
            //btn1.Background = Brushes.LightGreen;
            CheckWinner(ref pl1, ref pl2);
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn2.Content = pl1;
                }
                else
                {
                    btn2.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn2.Content = pl1;
                CpPlace(ref pl2);
            }
            btn2.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn3.Content = pl1;
                }
                else
                {
                    btn3.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn3.Content = pl1;
                CpPlace(ref pl2);
            }
            btn3.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn4.Content = pl1;
                }
                else
                {
                    btn4.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn4.Content = pl1;
                CpPlace(ref pl2);
            }
            btn4.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn5.Content = pl1;
                }
                else
                {
                    btn5.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn5.Content = pl1;
                CpPlace(ref pl2);
            }
            btn5.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn6.Content = pl1;
                }
                else
                {
                    btn6.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn6.Content = pl1;
                CpPlace(ref pl2);
            }
            btn6.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn7.Content = pl1;
                }
                else
                {
                    btn7.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn7.Content = pl1;
                CpPlace(ref pl2);
            }
            btn7.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn8.Content = pl1;
                }
                else
                {
                    btn8.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn8.Content = pl1;
                CpPlace(ref pl2);
            }
            btn8.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn9.Content = pl1;
                }
                else
                {
                    btn9.Content = pl2;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn9.Content = pl1;
                CpPlace(ref pl2);
            }
            btn9.IsEnabled = false;
            CheckWinner(ref pl1, ref pl2);
        }
        #endregion
        // pleyer2

        // cp
        #region Cp Random Place
        private void CpPlace(ref string opn)
        {
            Random rnd = new Random();
            int randomButtonIndex = rnd.Next(1, 10);

            switch (randomButtonIndex)
            {
                case 1:
                    btn1.Content = opn;
                    btn1.IsEnabled = false;
                    break;
                case 2:
                    btn2.Content = opn;
                    btn2.IsEnabled = false;
                    break;
                case 3:
                    btn3.Content = opn;
                    btn3.IsEnabled = false;
                    break;
                case 4:
                    btn4.Content = opn;
                    btn4.IsEnabled = false;
                    break;
                case 5:
                    btn5.Content = opn;
                    btn5.IsEnabled = false;
                    break;
                case 6:
                    btn6.Content = opn;
                    btn6.IsEnabled = false;
                    break;
                case 7:
                    btn7.Content = opn;
                    btn7.IsEnabled = false;
                    break;
                case 8:
                    btn8.Content = opn;
                    btn8.IsEnabled = false;
                    break;
                case 9:
                    btn9.Content = opn;
                    btn9.IsEnabled = false;
                    break;
            }
        }
        #endregion
        // cp

        // menu bar
        #region menu bar
        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            NewGameStartingPlayerMessage();
            ClearBoard(ref pl1, ref pl2);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void p2Play_Click(object sender, RoutedEventArgs e)
        {
            oponent = true;
            ClearBoard(ref pl1, ref pl2);
            MessageBoxResult result = MessageBox.Show("Choose the starting player\nyes - player1 start\nno - player2 start", "CHOOSE WHO PLAY FIRST",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
            }
        }

        private void cpPlay_Click(object sender, RoutedEventArgs e)
        {
            oponent = false;
            ClearBoard(ref pl1, ref pl2);
            MessageBoxResult result = MessageBox.Show("Choose the starting player\nyes - player1 start\nno - cp start", "CHOOSE WHO PLAY FIRST",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                plar.IsPlay = true;
            }
            else if (result == MessageBoxResult.No)
            {
                opnt.IsPlay = true;
                CpPlace(ref pl2);
            }
        }

        #region buttons backgroune colors
        private void green_Click(object sender, RoutedEventArgs e)
        {
            // change buttons color
            btn1.Background = Brushes.LightGreen;
            btn2.Background = Brushes.LightGreen;
            btn3.Background = Brushes.LightGreen;
            btn4.Background = Brushes.LightGreen;
            btn5.Background = Brushes.LightGreen;
            btn6.Background = Brushes.LightGreen;
            btn7.Background = Brushes.LightGreen;
            btn8.Background = Brushes.LightGreen;
            btn9.Background = Brushes.LightGreen;
            // change buttons color

            // change rectangles color
            rec1.Fill = Brushes.Green;
            rec2.Fill = Brushes.Green;
            rec3.Fill = Brushes.Green;
            rec4.Fill = Brushes.Green;
            // change rectangles color

            // change screen back ground color
            Screen.Background = Brushes.Yellow;
            // change screen back ground color
        }
        private void yellow_Click(object sender, RoutedEventArgs e)
        {
            // change buttons color
            btn1.Background = Brushes.LightYellow;
            btn2.Background = Brushes.LightYellow;
            btn3.Background = Brushes.LightYellow;
            btn4.Background = Brushes.LightYellow;
            btn5.Background = Brushes.LightYellow;
            btn6.Background = Brushes.LightYellow;
            btn7.Background = Brushes.LightYellow;
            btn8.Background = Brushes.LightYellow;
            btn9.Background = Brushes.LightYellow;
            // change buttons color

            // change rectangles color
            rec1.Fill = Brushes.Yellow;
            rec2.Fill = Brushes.Yellow;
            rec3.Fill = Brushes.Yellow;
            rec4.Fill = Brushes.Yellow;
            // change rectangles color

            // change screen back ground color
            Screen.Background = Brushes.Red;
            // change screen back ground color
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            // change buttons color
            btn1.Background = Brushes.LightCoral;
            btn2.Background = Brushes.LightCoral;
            btn3.Background = Brushes.LightCoral;
            btn4.Background = Brushes.LightCoral;
            btn5.Background = Brushes.LightCoral;
            btn6.Background = Brushes.LightCoral;
            btn7.Background = Brushes.LightCoral;
            btn8.Background = Brushes.LightCoral;
            btn9.Background = Brushes.LightCoral;
            // change buttons color

            // change rectangles color
            rec1.Fill = Brushes.Red;
            rec2.Fill = Brushes.Red;
            rec3.Fill = Brushes.Red;
            rec4.Fill = Brushes.Red;
            // change rectangles color

            // change screen back ground color
            Screen.Background = Brushes.LightBlue;
            // change screen back ground color
        }

        private void blue_Click(object sender, RoutedEventArgs e)
        {
            // change buttons color
            btn1.Background = Brushes.LightBlue;
            btn2.Background = Brushes.LightBlue;
            btn3.Background = Brushes.LightBlue;
            btn4.Background = Brushes.LightBlue;
            btn5.Background = Brushes.LightBlue;
            btn6.Background = Brushes.LightBlue;
            btn7.Background = Brushes.LightBlue;
            btn8.Background = Brushes.LightBlue;
            btn9.Background = Brushes.LightBlue;
            // change buttons color

            // change rectangles color
            rec1.Fill = Brushes.Blue;
            rec2.Fill = Brushes.Blue;
            rec3.Fill = Brushes.Blue;
            rec4.Fill = Brushes.Blue;
            // change rectangles color

            // change screen back ground color
            Screen.Background = Brushes.LightGreen;
            // change screen back ground color
        }

        private void orange_Click(object sender, RoutedEventArgs e)
        {
            // change buttons color
            btn1.Background = Brushes.LightSalmon;
            btn2.Background = Brushes.LightSalmon;
            btn3.Background = Brushes.LightSalmon;
            btn4.Background = Brushes.LightSalmon;
            btn5.Background = Brushes.LightSalmon;
            btn6.Background = Brushes.LightSalmon;
            btn7.Background = Brushes.LightSalmon;
            btn8.Background = Brushes.LightSalmon;
            btn9.Background = Brushes.LightSalmon;
            // change buttons color

            // change rectangles color
            rec1.Fill = Brushes.Orange;
            rec2.Fill = Brushes.Orange;
            rec3.Fill = Brushes.Orange;
            rec4.Fill = Brushes.Orange;
            // change rectangles color

            // change screen back ground color
            Screen.Background = Brushes.LightPink;
            // change screen back ground color
        }

        private void white_Click(object sender, RoutedEventArgs e)
        {
            // change buttons color
            btn1.Background = Brushes.LightGray;
            btn2.Background = Brushes.LightGray;
            btn3.Background = Brushes.LightGray;
            btn4.Background = Brushes.LightGray;
            btn5.Background = Brushes.LightGray;
            btn6.Background = Brushes.LightGray;
            btn7.Background = Brushes.LightGray;
            btn8.Background = Brushes.LightGray;
            btn9.Background = Brushes.LightGray;
            // change buttons color

            // change rectangles color
            rec1.Fill = Brushes.Gray;
            rec2.Fill = Brushes.Gray;
            rec3.Fill = Brushes.Gray;
            rec4.Fill = Brushes.Gray;
            // change rectangles color

            // change screen back ground color
            Screen.Background = Brushes.White;
            // change screen back ground color
        }
        #endregion

        #endregion

        #region Choose Shape
        /// <summary>
        /// Choose 'X' or 'O'
        /// </summary>
        private void ChooseShape_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("What shape do you want to play with?\nFor 'O' - yes\nFor 'X' - no", "CHOOSE SHAPE",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                pl1 = "O";
                pl2 = "X";
            }
            else if (result == MessageBoxResult.No)
            {
                pl1 = "X";
                pl2 = "O";
            }
            NewGameStartingPlayerMessage();
        }
        #endregion
        //menu bar
    }
}