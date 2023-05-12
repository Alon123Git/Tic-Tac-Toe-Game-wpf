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
        #region Varibles
        p2 plar = new();
        oponent opnt = new();
        bool oponent = true;
        int player1Score;
        int player2Score;
        bool level1 = false;
        bool level2 = false;
        bool level3 = false;
        #endregion

        #region Constructor
        public GameLogic()
        {
            InitializeComponent();
            Levels.Visibility = Visibility.Hidden;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
            WhoWillPlayFirstMessage();
        }
        #endregion


        // pleyer2 logic
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
                WindowScore();

                MessageBox.Show("Let me clean the screen\n: - )", "CLEAN THE SCREEN",
                    MessageBoxButton.OK, MessageBoxImage.None);
                // clear board
                ClearBoard(ref plar.Shape, ref opnt.Shape);
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

        #region Score In Window
        private void WindowScore()
        {
            MessageBox.Show("The score is: \nplayer1 - " + plar.Score + "\nplayer2 - " + opnt.Score, "SCORE",
                 MessageBoxButton.OK, MessageBoxImage.Information);
            FollowingStartingPlayerMessage();
        }
        #endregion

        #region Consistant Score
        private void ConsistantScore()
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
        }

        private void NewGameStartingPlayerMessage()
        {
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
            WindowScore();
            ConsistantScore();
            ClearBoard(ref plar.Shape, ref opnt.Shape);
        }
        #endregion

        #region ChekWhoWon
        private bool CheckWinner(ref string pl, ref string co)
        {
            // ------- p win -------
            if ((string)btn1.Content == pl && (string)btn2.Content == pl && (string)btn3.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();
            }
            else if ((string)btn4.Content == pl && (string)btn5.Content == pl && (string)btn6.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }
            else if ((string)btn7.Content == pl && (string)btn8.Content == pl && (string)btn9.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }
            else if ((string)btn1.Content == pl && (string)btn4.Content == pl && (string)btn7.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }
            else if ((string)btn2.Content == pl && (string)btn5.Content == pl && (string)btn8.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }
            else if ((string)btn3.Content == pl && (string)btn6.Content == pl && (string)btn9.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }
            else if ((string)btn1.Content == pl && (string)btn5.Content == pl && (string)btn9.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }
            else if ((string)btn3.Content == pl && (string)btn5.Content == pl && (string)btn7.Content == pl)
            {
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin();

            }

            // -------- cp win --------
            if ((string)btn1.Content == co && (string)btn2.Content == co && (string)btn3.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn4.Content == co && (string)btn5.Content == co && (string)btn6.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn7.Content == co && (string)btn8.Content == co && (string)btn9.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn1.Content == co && (string)btn4.Content == co && (string)btn7.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn2.Content == co && (string)btn5.Content == co && (string)btn8.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn3.Content == co && (string)btn6.Content == co && (string)btn9.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn1.Content == co && (string)btn5.Content == co && (string)btn9.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();

            }
            else if ((string)btn3.Content == co && (string)btn5.Content == co && (string)btn7.Content == co)
            {
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin();
            }
            GameTied(ref plar.Shape, ref opnt.Shape);

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
                    btn1.Content = plar.Shape;
                }
                else
                {
                    btn1.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay; // change turn
            }
            else if (oponent == false)
            {
                btn1.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn1.IsEnabled = false;

            //btn1.Background = Brushes.LightGreen;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn2.Content = plar.Shape;
                }
                else
                {
                    btn2.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn2.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn2.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn3.Content = plar.Shape;
                }
                else
                {
                    btn3.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn3.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn3.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn4.Content = plar.Shape;
                }
                else
                {
                    btn4.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn4.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn4.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn5.Content = plar.Shape;
                }
                else
                {
                    btn5.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn5.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn5.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn6.Content = plar.Shape;
                }
                else
                {
                    btn6.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn6.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn6.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn7.Content = plar.Shape;
                }
                else
                {
                    btn7.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn7.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn7.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn8.Content = plar.Shape;
                }
                else
                {
                    btn8.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn8.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn8.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (oponent)
            {
                if (plar.IsPlay)
                {
                    btn9.Content = plar.Shape;
                }
                else
                {
                    btn9.Content = opnt.Shape;
                }
                plar.IsPlay = !plar.IsPlay;
            }
            else if (oponent == false)
            {
                btn9.Content = plar.Shape;
                CheckWinner(ref plar.Shape, ref opnt.Shape);
                CpPlaceLevel1(ref opnt.Shape);
            }
            btn9.IsEnabled = false;
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }
        #endregion
        // pleyer2 logic

        // cp logic
        #region Cp Random Place levels
        private void CpPlaceLevel1(ref string opn)
        {
            Random rnd = new Random();
            int randomButtonIndex;
            while (true)
            {
                randomButtonIndex = rnd.Next(1, 10);
                if (randomButtonIndex == 1)
                {
                    if (btn1.Content == "" || btn1.Content == null)
                    {
                        btn1.Content = opn;
                        btn1.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 2)
                {
                    if (btn2.Content == "" || btn2.Content == null)
                    {
                        btn2.Content = opn;
                        btn2.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 3)
                {
                    if (btn3.Content == "" || btn3.Content == null)
                    {
                        btn3.Content = opn;
                        btn3.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 4)
                {
                    if (btn4.Content == "" || btn4.Content == null)
                    {
                        btn4.Content = opn;
                        btn4.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 5)
                {
                    if (btn5.Content == "" || btn5.Content == null)
                    {
                        btn5.Content = opn;
                        btn5.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 6)
                {
                    if (btn6.Content == "" || btn6.Content == null)
                    {
                        btn6.Content = opn;
                        btn6.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 7)
                {
                    if (btn7.Content == "" || btn7.Content == null)
                    {
                        btn7.Content = opn;
                        btn7.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 8)
                {
                    if (btn8.Content == "" || btn8.Content == null)
                    {
                        btn8.Content = opn;
                        btn8.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 9)
                {
                    if (btn9.Content == "" || btn9.Content == null)
                    {
                        btn9.Content = opn;
                        btn9.IsEnabled = false;
                        break;
                    }
                }
            }
        }
        
        private void CpPlaceLevel2()
        {
            
        }

        private void CpPlaceLevel3()
        {
            if (btn1.Content == plar.Shape && btn2.Content == plar.Shape)
            {
                btn3.Content = opnt.Shape;
            }
            else if (btn4.Content == plar.Shape && btn5.Content == plar.Shape)
            {
                btn5.Content = opnt.Shape;
            }
            else if (btn7.Content == plar.Shape && btn8.Content == plar.Shape)
            {
                btn9.Content = opnt.Shape;
            }
            else if (btn1.Content == plar.Shape && btn4.Content == plar.Shape)
            {
                btn7.Content = opnt.Shape;
            }
            else if (btn2.Content == plar.Shape && btn5.Content == plar.Shape)
            {
                btn8.Content = opnt.Shape;
            }
            else if (btn3.Content == plar.Shape && btn6.Content == plar.Shape)
            {
                btn9.Content = opnt.Shape;
            }
            else if (btn1.Content == plar.Shape && btn5.Content == plar.Shape)
            {
                btn9.Content= opnt.Shape;
            }
            else if (btn3.Content == plar.Shape && btn5.Content == plar.Shape)
            {
                btn7.Content = opnt.Shape;
            }
        }
        #endregion
        // cp logic

        // menu bar logic
        #region menu bar
        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            NewGameStartingPlayerMessage();
            ClearBoard(ref plar.Shape, ref opnt.Shape);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void p2Play_Click(object sender, RoutedEventArgs e)
        {
            oponent = true;
            ClearBoard(ref plar.Shape, ref opnt.Shape);
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
            Levels.Visibility = Visibility.Hidden;
        }

        private void cpPlay_Click(object sender, RoutedEventArgs e)
        {
            oponent = false;
            ClearBoard(ref plar.Shape, ref opnt.Shape);
            MessageBoxResult result = MessageBox.Show("Choose the starting player\nyes - player1 start\nno - cp start", "CHOOSE WHO PLAY FIRST",
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
                CpPlaceLevel1(ref opnt.Shape);
            }
            Levels.Visibility = Visibility.Visible;
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

            // change score rectengle color
            scoreRec1.Fill = Brushes.LightBlue;
            scoreRec2.Fill = Brushes.LightBlue;
            // change score rectengle color
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

            // change score rectengle color
            scoreRec1.Fill = Brushes.LightGray;
            scoreRec2.Fill = Brushes.LightGray;
            // change score rectengle color
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

            // change score rectengle color
            scoreRec1.Fill = Brushes.Yellow;
            scoreRec2.Fill = Brushes.Yellow;
            // change score rectengle color
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

            // change score rectengle color
            scoreRec1.Fill = Brushes.Red;
            scoreRec2.Fill = Brushes.Red;
            // change score rectengle color
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

            // change score rectengle color
            scoreRec1.Fill = Brushes.Blue;
            scoreRec2.Fill = Brushes.Blue;
            // change score rectengle color
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

            // change score rectengle color
            scoreRec1.Fill = Brushes.LightCoral;
            scoreRec2.Fill = Brushes.LightCoral;
            // change score rectengle color
        }
        #endregion

        #region Choose Shape
        private void O_Shape_Click(object sender, RoutedEventArgs e)
        {
            plar.Shape = "O";
            opnt.Shape = "X";
            NewGameStartingPlayerMessage();
        }

        private void X_Shape_Click(object sender, RoutedEventArgs e)
        {
            plar.Shape = "X";
            opnt.Shape = "O";
            NewGameStartingPlayerMessage();
        }
        #endregion

        #region Difficulty Level
        private void Level1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you wanna start a new game?", "NEW GAME?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearBoard(ref plar.Shape, ref opnt.Shape);
                NewGameStartingPlayerMessage();
                if (opnt.IsPlay == true)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }
            }
            level1 = true;
        }

        private void Level2_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you wanna start a new game?", "NEW GAME?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearBoard(ref plar.Shape, ref opnt.Shape);
                NewGameStartingPlayerMessage();
                if (opnt.IsPlay == true)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }
            }
            level2 = true;
        }

        private void Level3_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you wanna start a new game?", "NEW GAME?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearBoard(ref plar.Shape, ref opnt.Shape);
                NewGameStartingPlayerMessage();
                if (opnt.IsPlay == true)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }
            }
            level3 = true;
        }
        #endregion

        #endregion
        //menu bar logic
    }
}