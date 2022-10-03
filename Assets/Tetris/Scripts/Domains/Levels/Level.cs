using UniRx;
using System;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Domains.Levels
{
    public class Level
    {
        int _value;
        public int Value => _value;

        Subject<int> _whenLevelSet;
        public IObservable<int> WhenLevelSet => _whenLevelSet;

        public Level()
        {
            _value = 1;
            _whenLevelSet = new Subject<int>();
        }

        public void Set(Score point)
        {
            if (point.Value >= 10 && _value == 1) {
                Up();
            } else if (point.Value >= 30 && _value == 2) {
                Up();
            } else if (point.Value >= 60 && _value == 3) {
                Up();
            } else if (point.Value >= 100 && _value == 4) {
                Up();
            } else if (point.Value >= 150 && _value == 5) {
                Up();
            } else if (point.Value >= 210 && _value == 6) {
                Up();
            } else if (point.Value >= 280 && _value == 7) {
                Up();
            } else if (point.Value >= 360 && _value == 8) {
                Up();
            } else if (point.Value >= 450 && _value == 9) {
                Up();
            } else {
                // 何もしない
            }

            _whenLevelSet.OnNext(_value);
        }

        public void Up()
        {
            _value++;
            if (_value > 10) {
                _value = 10;
            }
        }

        public void Dispose()
        {
            _whenLevelSet.Dispose();
        }
    }
}
