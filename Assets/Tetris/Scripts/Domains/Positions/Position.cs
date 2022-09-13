using System;

namespace Tetris.Scripts.Domains.Positions
{
    public class Position
    {
        private int _x;
        public int X {
            get { return _x; }
        }
        private int _y;
        public int Y {
            get { return _y; }
        }
        private const int MinValue = 0;
        private const int XMaxValue = 10 - 1;
        private const int YMaxValue = 24;

        public Position(int x, int y)
        {
            if (x < MinValue || XMaxValue < x)
                throw new ArgumentOutOfRangeException($"X({x}) is out of range.");
            if (y < MinValue || YMaxValue < y)
                throw new ArgumentOutOfRangeException($"Y({y}) is out of range");

            _x = x;
            _y = y;
        }

        public void Change(int x, int y)
        {
            if (x < MinValue || XMaxValue < x)
                throw new ArgumentOutOfRangeException($"X({x}) is out of range.");
            if (y < MinValue || YMaxValue < y)
                throw new ArgumentOutOfRangeException($"Y({y}) is out of range");

            _x = x;
            _y = y;
        }
    }
}
