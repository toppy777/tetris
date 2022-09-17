using Tetris.Scripts.Domains.Points;

namespace Tetris.Scripts.Domains.Levels
{
    public class Level
    {
        int _value;
        public int Value => _value;

        public Level()
        {
            _value = 1;
        }

        public void Calc(Point point)
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
        }

        public void Up()
        {
            _value++;
            if (_value > 10) {
                _value = 10;
            }
        }
    }
}
