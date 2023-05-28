using IxIgul.BasicVaribles;
using System.ComponentModel.DataAnnotations;

namespace IxIgul.Players
{
    public class Player : data
    {
        #region varibles
        public bool IsPlay = false;
        public bool IsWinner = false;
        public int Score = 0;
        public string Shape = "O";
        #endregion

        #region constructors
        public Player(bool isWinner, int score, bool isPlay, string shape) : base(isWinner, score, isPlay, shape) { }

        public Player() { }
        #endregion
    }
}