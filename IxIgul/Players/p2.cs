using IxIgul.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IxIgul.Players
{
    public class p2 : data
    {
        public bool IsPlay = false;
        public bool IsWinner;
        public int Score;
        public string Shape = "O";
        public p2(bool isWinner, int score, bool isPlay, string shape) : base(isWinner, score, isPlay, shape) { }

        public p2() { }
    }
}