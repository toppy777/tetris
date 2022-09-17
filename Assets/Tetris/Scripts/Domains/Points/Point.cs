using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Domains.Points
{
    public class Point
    {
        int _value = 0;
        public int Value => _value;

        public void Add(Level level, int rowCount)
        {
            _value += level.Value * rowCount;
            if (_value > 99999) {
                _value = 99999;
            }
        }
    }
}
