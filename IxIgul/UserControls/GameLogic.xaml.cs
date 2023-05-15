using IxIgul.BasicVaribles;
using IxIgul.Players;
using System;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace IxIgul.UserControls
{
    /// <summary>
    /// Interaction logic for GameLogic.xaml
    /// </summary>
    public partial class GameLogic : UserControl
    {
        #region Varibles
        Player plar = new();
        Opponent opnt = new();
        data dat = new();
        System.Timers.Timer timer = new System.Timers.Timer();
        bool isTie = false;
        int counter = 0;
        bool oponent = true;
        int player1Score = 0;
        int player2Score = 0;
        int tieGame = 0;
        bool gameIsStart = false;
        bool gameIsEnd = false;
        #endregion

        #region Constructor
        public GameLogic()
        {
            InitializeComponent();

            Levels.Visibility = Visibility.Hidden;
            txtLevels.Text = "Level 1";
            txtLevels.Visibility = Visibility.Hidden;

            StartTimer(ref timer);

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
                ClearBoard(ref plar.Shape, ref opnt.Shape);

                // if it is cp turn generate cp shape in random place
                if (opnt.IsPlay && oponent == false)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }

                ResetAndStartTimer(ref counter, ref timer); // id the game is tied reset the timer
            }
            tie = false;
        }
        #endregion

        #region Clear Board
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
                if (oponent == false)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }
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
                if (oponent == false)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }
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
                if (oponent == false)
                {
                    CpPlaceLevel1(ref opnt.Shape);
                }
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
                ClearBoard(ref plar.Shape, ref opnt.Shape); // reset score
                NewGameStartingPlayerMessage(); // reset board and score
            }
            else
            {
                t.Start();
            }
        }
        #endregion

        #region Who Is Won
        private void isWin(ref bool gameEnd, ref bool gameStart)
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
            ClearBoard(ref plar.Shape, ref opnt.Shape);

            // if cp start nex game generate cp shape in random place
            if (opnt.IsPlay && oponent == false && plar.IsWinner ||
                opnt.IsPlay && oponent == false && opnt.IsWinner)
            {
                CpPlaceLevel1(ref opnt.Shape);
            }
        }
        #endregion

        #region Check Who Won
        private bool CheckWinner(ref string pl, ref string co)
        {
            // ------- player win -------
            if ((string)btn1.Content == pl && (string)btn2.Content == pl && (string)btn3.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn4.Content == pl && (string)btn5.Content == pl && (string)btn6.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn7.Content == pl && (string)btn8.Content == pl && (string)btn9.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn1.Content == pl && (string)btn4.Content == pl && (string)btn7.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn2.Content == pl && (string)btn5.Content == pl && (string)btn8.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == pl && (string)btn6.Content == pl && (string)btn9.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);

            }
            else if ((string)btn1.Content == pl && (string)btn5.Content == pl && (string)btn9.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == pl && (string)btn5.Content == pl && (string)btn7.Content == pl)
            {
                StopTimer(ref timer);
                plar.IsWinner = true;
                opnt.IsWinner = false;
                plar.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }

            // -------- cp win --------
            if ((string)btn1.Content == co && (string)btn2.Content == co && (string)btn3.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);

            }
            else if ((string)btn4.Content == co && (string)btn5.Content == co && (string)btn6.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn7.Content == co && (string)btn8.Content == co && (string)btn9.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn1.Content == co && (string)btn4.Content == co && (string)btn7.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn2.Content == co && (string)btn5.Content == co && (string)btn8.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == co && (string)btn6.Content == co && (string)btn9.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn1.Content == co && (string)btn5.Content == co && (string)btn9.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            else if ((string)btn3.Content == co && (string)btn5.Content == co && (string)btn7.Content == co)
            {
                StopTimer(ref timer);
                opnt.IsWinner = true;
                plar.IsWinner = false;
                opnt.Score++;
                isWin(ref gameIsEnd, ref gameIsStart);
                return true;
            }
            GameTied(ref isTie, ref plar.IsWinner, ref opnt.IsWinner);

            return false;
        }
        #endregion

        #region empty borad method
        private bool BoardIsEmpty()
        {
            if ((string)btn1.Content == "" && (string)btn2.Content == "" && (string)btn3.Content == "" &&
            (string)btn4.Content == "" && (string)btn5.Content == "" && (string)btn6.Content == "" &&
            (string)btn7.Content == "" && (string)btn8.Content == "" && (string)btn9.Content == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region buttons events
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            btn1.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in boolean varible if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn1.IsEnabled = false;
                    }
                    break;
                }
            }

            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            btn2.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn2.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            btn3.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn3.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            btn4.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn4.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            btn5.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn5.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            btn6.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn6.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            btn7.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn7.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            btn8.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn8.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            btn9.Content = plar.Shape;
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
                bool somePlayerWon = CheckWinner(ref plar.Shape, ref opnt.Shape); // store in the boolean if there are game winner
                while (true)
                {
                    if (somePlayerWon) // check if there are game winner
                    {
                        break; // go out of the loop for not allowing for cp to put shape if there are winner
                    }
                    if (dat.level1)
                    {
                        CpPlaceLevel1(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level2)
                    {
                        CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level3)
                    {
                        CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level4)
                    {
                        CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level5)
                    {
                        CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level6)
                    {
                        CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level7)
                    {
                        CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level8)
                    {
                        CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level9)
                    {
                        CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level10)
                    {
                        CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level11)
                    {
                        CpPlaceLevel11(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level12)
                    {
                        CpPlaceLevel12(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level13)
                    {
                        CpPlaceLevel13(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level14)
                    {
                        CpPlaceLevel14(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level15)
                    {
                        CpPlaceLevel15(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level16)
                    {
                        CpPlaceLevel16(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level17)
                    {
                        CpPlaceLevel17(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level18)
                    {
                        CpPlaceLevel18(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    else if (dat.level19)
                    {
                        CpPlaceLevel19(ref opnt.Shape);
                        btn9.IsEnabled = false;
                    }
                    break;
                }
            }
            CheckWinner(ref plar.Shape, ref opnt.Shape);
        }
        #endregion

        #endregion
        // pleyer2 logic

        // cp logic
        #region Cp Random Place levels

        /// <summary>
        /// cp displayed the shape randomaly on the board
        /// </summary>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel1(ref string opn)
        {
            Random rnd = new Random();
            int randomButtonIndex;
            int exitLoop = 0;
            while (true)
            {
                if ((string)btn1.Content != "" && (string)btn2.Content != "" && (string)btn3.Content != "" &&
                    (string)btn4.Content != "" && (string)btn5.Content != "" && (string)btn6.Content != "" &&
                    (string)btn7.Content != "" && (string)btn8.Content != "" && (string)btn9.Content != "" ||
                    btn1.Content != null && btn2.Content != null && btn3.Content != null &&
                    btn4.Content != null && btn5.Content != null && btn6.Content != null &&
                    btn7.ContentTemplate != null && btn8.Content != null && btn9.Content != null)
                {
                    break;
                }
                randomButtonIndex = rnd.Next(1, 10);
                if (randomButtonIndex == 1)
                {
                    if ((string)btn1.Content == "" || btn1.Content == null)
                    {
                        btn1.Content = opn;
                        btn1.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 2)
                {
                    if ((string)btn2.Content == "" || btn2.Content == null)
                    {
                        btn2.Content = opn;
                        btn2.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 3)
                {
                    if ((string)btn3.Content == "" || btn3.Content == null)
                    {
                        btn3.Content = opn;
                        btn3.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 4)
                {
                    if ((string)btn4.Content == "" || btn4.Content == null)
                    {
                        btn4.Content = opn;
                        btn4.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 5)
                {
                    if ((string)btn5.Content == "" || btn5.Content == null)
                    {
                        btn5.Content = opn;
                        btn5.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 6)
                {
                    if ((string)btn6.Content == "" || btn7.Content == null)
                    {
                        btn6.Content = opn;
                        btn6.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 7)
                {
                    if ((string)btn7.Content == "" || btn7.Content == null)
                    {
                        btn7.Content = opn;
                        btn7.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 8)
                {
                    if ((string)btn8.Content == "" || btn8.Content == null)
                    {
                        btn8.Content = opn;
                        btn8.IsEnabled = false;
                        break;
                    }
                }
                else if (randomButtonIndex == 9)
                {
                    if ((string)btn9.Content == "" || btn9.Content == null)
                    {
                        btn9.Content = opn;
                        btn9.IsEnabled = false;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block vertical and horizontal, excep in the middle
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel2(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == plr && (string)btn2.Content == plr &&
                (string)btn3.Content == "" || (string)btn3.Content == null)
            {
                btn3.Content = opn;
                btn3.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn4.Content == plr && (string)btn5.Content == plr &&
                (string)btn6.Content == "" || (string)btn6.Content == null)
            {
                btn6.Content = opn;
                btn6.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn7.Content == plr && (string)btn8.Content == plr &&
                (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn1.Content == plr && (string)btn4.Content == plr &&
                (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn2.Content == plr && (string)btn5.Content == plr &&
                (string)btn8.Content == "" || (string)btn8.Content == null)
            {
                btn8.Content = opn;
                btn8.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == plr && (string)btn6.Content == plr &&
                (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn1.Content == plr && (string)btn5.Content == plr &&
                (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == plr && (string)btn5.Content == plr &&
                (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel1(ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block diagonally, excep in the moddle
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel3(ref string plr, ref string opn)
        {

            bool conditionsSucceeded = false;

            if ((string)btn9.Content == plr && (string)btn5.Content == plr &&
                (string)btn1.Content == "" || (string)btn1.Content == null)
            {
                btn1.Content = opn;
                btn1.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn7.Content == plr && (string)btn5.Content == plr &&
                (string)btn3.Content == "" || (string)btn3.Content == null)
            {
                btn3.Content = opn;
                btn3.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel2(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp clock in the middle, excep in the middle diagonally
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel4(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == plr && (string)btn7.Content == plr &&
                (string)btn4.Content == "" || (string)btn4.Content == null)
            {
                btn4.Content = opn;
                btn4.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == plr && (string)btn9.Content == plr &&
                (string)btn6.Content == "" || (string)btn6.Content == null)
            {
                btn6.Content = opn;
                btn6.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn1.Content == plr && (string)btn3.Content == plr &&
                (string)btn2.Content == "" || (string)btn2.Content == null)
            {
                btn2.Content = opn;
                btn2.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn7.Content == plr && (string)btn9.Content == plr &&
                (string)btn8.Content == "" || (string)btn8.Content == null)
            {
                btn8.Content = opn;
                btn8.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel3(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp blockin the middle diagonally
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel5(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == plr && (string)btn9.Content == plr &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == plr && (string)btn7.Content == plr &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel4(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block in diagonally from above to down
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel6(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == plr && (string)btn5.Content == plr &&
               (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == plr && (string)btn5.Content == plr &&
               (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel5(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block in the middle always
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel7(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn2.Content == plr && (string)btn8.Content == plr &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn4.Content == plr && (string)btn6.Content == plr &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel6(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block from down ti up excep in the middle
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel8(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn7.Content == plr && (string)btn4.Content == plr &&
               (string)btn1.Content == "" || (string)btn1.Content == null)
            {
                btn1.Content = opn;
                btn1.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn9.Content == plr && (string)btn6.Content == plr &&
               (string)btn3.Content == "" || (string)btn3.Content == null)
            {
                btn3.Content = opn;
                btn3.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel7(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block from rigt to lefet excep in the middle
        /// </summary>
        /// <param name="plr">player shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel9(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn3.Content == plr && (string)btn2.Content == plr &&
               (string)btn1.Content == "" || (string)btn1.Content == null)
            {
                btn1.Content = opn;
                btn1.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn9.Content == plr && (string)btn8.Content == plr &&
               (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel8(ref plar.Shape, ref opnt.Shape);
            }
        }

        /// <summary>
        /// In addition to the previous method, cp block from right to left and from down to up in the middle
        /// </summary>
        /// <param name="plr">platyer shape</param>
        /// <param name="opn">cp shape</param>
        private void CpPlaceLevel10(ref string plr, ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn6.Content == plr && (string)btn5.Content == plr &&
              (string)btn4.Content == "" || (string)btn4.Content == null)
            {
                btn4.Content = opn;
                btn4.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn8.Content == plr && (string)btn5.Content == plr &&
              (string)btn2.Content == "" || (string)btn2.Content == null)
            {
                btn2.Content = opn;
                btn2.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel9(ref plar.Shape, ref opnt.Shape);
            }
        }

        private void CpPlaceLevel11(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == opn && (string)btn2.Content == opn &&
                (string)btn3.Content == "" || (string)btn3.Content == null)
            {
                btn3.Content = opn;
                btn3.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn4.Content == opn && (string)btn5.Content == opn &&
                (string)btn6.Content == "" || (string)btn6.Content == null)
            {
                btn6.Content = opn;
                btn6.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn7.Content == opn && (string)btn8.Content == opn &&
                (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn1.Content == opn && (string)btn4.Content == opn &&
                (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn2.Content == opn && (string)btn5.Content == opn &&
                (string)btn8.Content == "" || (string)btn8.Content == null)
            {
                btn8.Content = opn;
                btn8.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == opn && (string)btn6.Content == opn &&
                (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn1.Content == opn && (string)btn5.Content == opn &&
                (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == opn && (string)btn5.Content == opn &&
                (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel10(ref plar.Shape, ref opnt.Shape);
            }
        }

        private void CpPlaceLevel12(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn9.Content == opn && (string)btn5.Content == opn &&
                (string)btn1.Content == "" || (string)btn1.Content == null)
            {
                btn1.Content = opn;
                btn1.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn7.Content == opn && (string)btn5.Content == opn &&
                (string)btn3.Content == "" || (string)btn3.Content == null)
            {
                btn3.Content = opn;
                btn3.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel11(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel13(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == opn && (string)btn7.Content == opn &&
                (string)btn4.Content == "" || (string)btn4.Content == null)
            {
                btn4.Content = opn;
                btn4.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == opn && (string)btn9.Content == opn &&
                (string)btn6.Content == "" || (string)btn6.Content == null)
            {
                btn6.Content = opn;
                btn6.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn1.Content == opn && (string)btn3.Content == opn &&
                (string)btn2.Content == "" || (string)btn2.Content == null)
            {
                btn2.Content = opn;
                btn2.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn7.Content == opn && (string)btn9.Content == opn &&
                (string)btn8.Content == "" || (string)btn8.Content == null)
            {
                btn8.Content = opn;
                btn8.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel12(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel14(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == opn && (string)btn9.Content == opn &&
                 (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == opn && (string)btn7.Content == opn &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel13(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel15(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn1.Content == opn && (string)btn5.Content == opn &&
              (string)btn9.Content == "" || (string)btn9.Content == null)
            {
                btn9.Content = opn;
                btn9.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn3.Content == opn && (string)btn5.Content == opn &&
               (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel14(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel16(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn2.Content == opn && (string)btn8.Content == opn &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn4.Content == opn && (string)btn6.Content == opn &&
                (string)btn5.Content == "" || (string)btn5.Content == null)
            {
                btn5.Content = opn;
                btn5.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel15(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel17(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn7.Content == opn && (string)btn4.Content == opn &&
               (string)btn1.Content == "" || (string)btn1.Content == null)
            {
                btn1.Content = opn;
                btn1.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn9.Content == opn && (string)btn6.Content == opn &&
               (string)btn3.Content == "" || (string)btn3.Content == null)
            {
                btn3.Content = opn;
                btn3.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel16(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel18(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn3.Content == opn && (string)btn2.Content == opn &&
               (string)btn1.Content == "" || (string)btn1.Content == null)
            {
                btn1.Content = opn;
                btn1.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn9.Content == opn && (string)btn8.Content == opn &&
               (string)btn7.Content == "" || (string)btn7.Content == null)
            {
                btn7.Content = opn;
                btn7.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel17(ref opnt.Shape);
            }
        }

        private void CpPlaceLevel19(ref string opn)
        {
            bool conditionsSucceeded = false;

            if ((string)btn6.Content == opn && (string)btn5.Content == opn &&
             (string)btn4.Content == "" || (string)btn4.Content == null)
            {
                btn4.Content = opn;
                btn4.IsEnabled = false;
                conditionsSucceeded = true;
            }
            else if ((string)btn8.Content == opn && (string)btn5.Content == opn &&
              (string)btn2.Content == "" || (string)btn2.Content == null)
            {
                btn2.Content = opn;
                btn2.IsEnabled = false;
                conditionsSucceeded = true;
            }

            if (conditionsSucceeded == false)
            {
                CpPlaceLevel18(ref opnt.Shape);
            }
        }

        #endregion
        // cp logic

        // menu bar logic
        #region menu bar
        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            ClearBoard(ref plar.Shape, ref opnt.Shape); // clear the board
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
            oponent = true; // allow p2 logic to happen
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
            ResetScore(out plar.Score, out opnt.Score, out tieGame);
            Levels.Visibility = Visibility.Hidden; // hide levels option when cp not play
            txtLevels.Visibility = Visibility.Hidden; // hide levels text when cp not play
            ResetAndStartTimer(ref counter, ref timer);
        }

        /// <summary>
        /// allowing cp to play
        /// </summary>
        private void cpPlay_Click(object sender, RoutedEventArgs e)
        {
            oponent = false; // allow cp logic to happen
            ClearBoard(ref plar.Shape, ref opnt.Shape);
            NewGameStartingPlayerMessage(); // start new game
            Levels.Visibility = Visibility.Visible; // show levels option when cp play
            txtLevels.Visibility = Visibility.Visible; // show levels text when cp play
            BegginerBackgroundColor();
        }

        #region Difficulty Level
        /// <summary>
        /// allowing level 1 logic after clicked
        /// </summary>
        private void Level1_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 1"; // display the cp level in the text block
            dat.level1 = true; // allow level 1 logic to happen
            BegginerBackgroundColor(); // change screen background color
        }

        /// <summary>
        /// allowing level 2 logic after clicked
        /// </summary>
        private void Level2_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 2"; // display the cp level in the text block
            dat.level2 = true; // allow level 2 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            BegginerBackgroundColor(); // change screen background color
        }

        /// <summary>
        /// allowing level 3 logic after clicked
        /// </summary>
        private void Level3_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer); // reset score
            txtLevels.Text = "Level 3"; // display the cp level in the text block
            dat.level3 = true; // allow level 3 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            BegginerBackgroundColor(); // change screen background color
        }

        /// <summary>
        /// allowing level 4 logic after clicked
        /// </summary>
        private void Level4_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer); // reset score
            txtLevels.Text = "Level 4"; // display the cp level in the text block
            dat.level4 = true; // allow level 4 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            BegginerBackgroundColor(); // change screen background color
        }

        /// <summary>
        /// allowing level 5 logic after clicked
        /// </summary>
        private void Level5_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer); // reset score
            txtLevels.Text = "Level 5"; // display the cp level in the text block
            dat.level5 = true; // allow level 5 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            BegginerBackgroundColor(); // change screen background color
        }

        /// <summary>
        /// allowing level 6 logic after clicked
        /// </summary>
        private void Level6_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer); // reset score
            txtLevels.Text = "Level 6"; // display the cp level in the text block
            dat.level6 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            AdvancedBackgroundColor(); // change screen background color
        }

        private void Level7_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer); // reset score
            txtLevels.Text = "Level 7"; // display the cp level in the text block
            dat.level7 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            AdvancedBackgroundColor(); // change screen background color
        }
        private void Level8_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer); // reset score
            txtLevels.Text = "Level 8"; // display the cp level in the text block
            dat.level8 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            AdvancedBackgroundColor(); // change screen background color
        }
        private void Level9_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 9"; // display the cp level in the text block
            dat.level9 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            AdvancedBackgroundColor(); // change screen background color
        }
        private void Level10_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 10"; // display the cp level in the text block
            dat.level10 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            AdvancedBackgroundColor(); // change screen background color
        }
        private void Level11_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 11"; // display the cp level in the text block
            dat.level11 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            ExpertBackgroundColor(); // change screen background color
        }
        private void Level12_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 12"; // display the cp level in the text block
            dat.level12 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            ExpertBackgroundColor(); // change screen background color
        }
        private void Level13_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 13"; // display the cp level in the text block
            dat.level13 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            ExpertBackgroundColor(); // change screen background color
        }
        private void Level14_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 14"; // display the cp level in the text block
            dat.level14 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            ExpertBackgroundColor(); // change screen background color
        }
        private void Level15_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 15"; // display the cp level in the text block
            dat.level15 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            ExpertBackgroundColor(); // change screen background color
        }
        private void Level16_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 16"; // display the cp level in the text block
            dat.level16 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            LegendaryBackgroundColor(); // change screen background color
        }
        private void Level17_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 17"; // display the cp level in the text block
            dat.level17 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            LegendaryBackgroundColor(); // change screen background color
        }
        private void Level18_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 18"; // display the cp level in the text block
            dat.level18 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            LegendaryBackgroundColor(); // change screen background color
        }
        private void Level19_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame(ref timer);
            txtLevels.Text = "Level 19"; // display the cp level in the text block
            dat.level19 = true; // allow level 6 logic to happen
            dat.level1 = false; // not allow to level 1 logic to happen
            LegendaryBackgroundColor(); // change screen background color
        }
        #endregion


        #region change board background color
        private void BegginerBackgroundColor()
        {
            Screen.Background = Brushes.LightBlue; // change borad background color to light blue

            // change rectangles color to blue
            rec1.Fill = Brushes.Blue;
            rec2.Fill = Brushes.Blue;
            rec3.Fill = Brushes.Blue;
            rec4.Fill = Brushes.Blue;

            // change buttons background color to the screen background color 
            btn1.Background = Brushes.LightBlue;
            btn2.Background = Brushes.LightBlue;
            btn3.Background = Brushes.LightBlue;
            btn4.Background = Brushes.LightBlue;
            btn5.Background = Brushes.LightBlue;
            btn6.Background = Brushes.LightBlue;
            btn7.Background = Brushes.LightBlue;
            btn8.Background = Brushes.LightBlue;
            btn9.Background = Brushes.LightBlue;

            txtLevels.Foreground = Brushes.Blue; // change the text to blue
        }

        private void AdvancedBackgroundColor()
        {
            Screen.Background = Brushes.LightGreen; // change borad background color to light green

            // change rectangles color to green
            rec1.Fill = Brushes.Green;
            rec2.Fill = Brushes.Green;
            rec3.Fill = Brushes.Green;
            rec4.Fill = Brushes.Green;

            // change buttons background color to the screen background color 
            btn1.Background = Brushes.LightGreen;
            btn2.Background = Brushes.LightGreen;
            btn3.Background = Brushes.LightGreen;
            btn4.Background = Brushes.LightGreen;
            btn5.Background = Brushes.LightGreen;
            btn6.Background = Brushes.LightGreen;
            btn7.Background = Brushes.LightGreen;
            btn8.Background = Brushes.LightGreen;
            btn9.Background = Brushes.LightGreen;

            txtLevels.Foreground = Brushes.Green; // change the text to green
        }

        private void ExpertBackgroundColor()
        {
            Screen.Background = Brushes.LightYellow; // change borad background color to light yellow

            // change rectangles color to yellow
            rec1.Fill = Brushes.Yellow;
            rec2.Fill = Brushes.Yellow;
            rec3.Fill = Brushes.Yellow;
            rec4.Fill = Brushes.Yellow;

            // change buttons background color to the screen background color 
            btn1.Background = Brushes.LightYellow;
            btn2.Background = Brushes.LightYellow;
            btn3.Background = Brushes.LightYellow;
            btn4.Background = Brushes.LightYellow;
            btn5.Background = Brushes.LightYellow;
            btn6.Background = Brushes.LightYellow;
            btn7.Background = Brushes.LightYellow;
            btn8.Background = Brushes.LightYellow;
            btn9.Background = Brushes.LightYellow;

            txtLevels.Foreground = Brushes.Yellow; // change the text to yellow
        }

        private void LegendaryBackgroundColor()
        {
            Screen.Background = Brushes.LightCoral; // change borad background color to light coral

            // change rectangles color to red
            rec1.Fill = Brushes.Red;
            rec2.Fill = Brushes.Red;
            rec3.Fill = Brushes.Red;
            rec4.Fill = Brushes.Red;

            // change buttons background color to the screen background color 
            btn1.Background = Brushes.LightCoral;
            btn2.Background = Brushes.LightCoral;
            btn3.Background = Brushes.LightCoral;
            btn4.Background = Brushes.LightCoral;
            btn5.Background = Brushes.LightCoral;
            btn6.Background = Brushes.LightCoral;
            btn7.Background = Brushes.LightCoral;
            btn8.Background = Brushes.LightCoral;
            btn9.Background = Brushes.LightCoral;

            txtLevels.Foreground = Brushes.Red; // change the text to red
        }
        #endregion

        #region Choose Shape
        private void O_Shape_Click(object sender, RoutedEventArgs e)
        {
            plar.Shape = "O";
            opnt.Shape = "X";
            ClearBoard(ref plar.Shape, ref opnt.Shape);
            NewGameStartingPlayerMessage();
        }

        private void X_Shape_Click(object sender, RoutedEventArgs e)
        {
            plar.Shape = "X";
            opnt.Shape = "O";
            ClearBoard(ref plar.Shape, ref opnt.Shape);
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
        private void StopTimer(ref System.Timers.Timer t)
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