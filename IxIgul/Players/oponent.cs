using IxIgul.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IxIgul.Players
{
    public class oponent : data
    {
        public bool IsPlay = false;
        public bool IsWinner;
        public int Score;
        public oponent(bool isWinner, int score, bool isPlay) : base(isWinner, score, isPlay) { }

        public oponent() { }
    }
}