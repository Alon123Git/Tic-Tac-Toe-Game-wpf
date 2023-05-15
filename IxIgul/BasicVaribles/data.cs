namespace IxIgul.BasicVaribles
{
    public class data
    {
        public bool IsPlay = false;
        public bool IsWinner = false;
        public int Score = 0;
        public string Shape;
        public bool level1 = true;
        public bool level2 = false;
        public bool level3 = false;
        public bool level4 = false;
        public bool level5 = false;
        public bool level6 = false;
        public bool level7 = false;
        public bool level8 = false;
        public bool level9 = false;
        public bool level10 = false;
        public bool level11 = false;
        public bool level12 = false;
        public bool level13 = false;
        public bool level14 = false;
        public bool level15 = false;
        public bool level16 = false;
        public bool level17 = false;
        public bool level18 = false;
        public bool level19 = false;
        public data(bool isWinner, int score, bool isPlay, string shape, bool lvl1, bool lvl2, bool lvl3, bool lvl4, bool lvl5,
            bool lvl6, bool lvl7, bool lvl8, bool lvl9, bool lvl10, bool lvl11, bool lvl12, bool lvl13, bool lvl14, bool lvl15,
            bool lvl16, bool lvl17, bool lvl18, bool lvl19)
        {
            IsPlay = isPlay;
            IsWinner = isWinner;
            Score = score;
            Shape = shape;
            level1 = lvl1;
            level2 = lvl2;
            level3 = lvl3;
            level4 = lvl4;
            level5 = lvl5;
            level6 = lvl6;
            level7 = lvl7;
            level8 = lvl8;
            level9 = lvl9;
            level10 = lvl10;
            level11 = lvl11;
            level12 = lvl12;
            level13 = lvl13;
            level14 = lvl14;
            level15 = lvl15;
            level16 = lvl16;
            level17 = lvl17;
            level18 = lvl18;
            level19 = lvl19;
        }

        public data(bool isWinner, int score, bool isPlay, string shape)
        {
            IsWinner = isWinner;
            Score = score;
            IsPlay = isPlay;
            Shape = shape;
        }

        public data() { }
    }
}
