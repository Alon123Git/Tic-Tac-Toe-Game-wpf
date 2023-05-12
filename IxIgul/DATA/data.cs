using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IxIgul.DATA
{
    public class data
    {
        public bool IsPlay = false;
        public bool IsWinner;
        public int Score;
        public string Shape;
        public data(bool isWinner, int score, bool isPlay, string shape)
        {
            IsPlay = isPlay;
            IsWinner = isWinner;
            Score = score;
            Shape = shape;
        }
        public data() { }
    }
}
