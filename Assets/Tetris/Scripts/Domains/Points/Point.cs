using Tetris.Scripts.Domains.Levels;
using UniRx;
using System;

namespace Tetris.Scripts.Domains.Points
{
    public class Point
    {
        int _value = 0;
        public int Value => _value;

        Subject<int> _whenPointAdd = new();
        public IObservable<int> WhenPointAdd => _whenPointAdd;

        public void Add(Level level, int rowCount)
        {
            _value += level.Value * rowCount;
            if (_value > 99999) {
                _value = 99999;
            }
            _whenPointAdd.OnNext(_value);
        }
    }
}
