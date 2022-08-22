using UnityEngine;
using System;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoPiecePosition
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
        private const int YMaxValue = 20 - 1;

        public MinoPiecePosition(int x, int y)
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
