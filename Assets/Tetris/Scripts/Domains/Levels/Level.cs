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
            if (point.Value > 20) {
                Up();
            } else if (point.Value > 40) {
                Up();
            } else if (point.Value > 80) {
                Up();
            } else if (point.Value > 160) {
                Up();
            } else if (point.Value > 320) {
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
