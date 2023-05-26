using IxIgul.BasicVaribles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IxIgul.Players
{
    public class Opponent : data
    {
        public  bool IsPlay = false;
        public bool IsWinner = false;
        public int Score = 0;
        public string Shape = "X";
        public Opponent(bool isWinner, int score, bool isPlay, string shape) : base(isWinner, score, isPlay, shape) { }

        public Opponent() { }
    }
}