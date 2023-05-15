using IxIgul.BasicVaribles;
using System.ComponentModel.DataAnnotations;

namespace IxIgul.Players
{
    public class Player : data
    {
        public bool IsPlay = false;
        public bool IsWinner = false;
        public int Score = 0;
        public string Shape = "O";
        public Player(bool isWinner, int score, bool isPlay, string shape) : base(isWinner, score, isPlay, shape) { }

        public Player() { }
    }
}