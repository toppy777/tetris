using Tetris.Scripts.Domains.Levels;
using UniRx;
using System;

namespace Tetris.Scripts.Domains.Scores
{
    public class Score
    {
        int _value = 0;
        public int Value => _value;

        Subject<int> _whenScoreAdd = new();
        public IObservable<int> WhenScoreAdd => _whenScoreAdd;

        public void Add(Level level, int rowCount)
        {
            _value += level.Value * rowCount;
            if (_value > 99999) {
                _value = 99999;
            }
            _whenScoreAdd.OnNext(_value);
        }
    }
}
