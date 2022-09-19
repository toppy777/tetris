using UnityEngine;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Domains.MinoMoveSpeeds
{
    public class MinoMoveSpeed
    {
        float _speed;
        float _elapsedTime;

        public MinoMoveSpeed()
        {
            _speed = 0.5f;
            _elapsedTime = 0f;
        }

        public void SetSpeed(Level level)
        {
            float speed = 0;
            switch(level.Value) {
                default:
                case 1:
                    speed = 0.5f;
                    break;
                case 2:
                    speed = 0.4f;
                    break;
                case 3:
                    speed = 0.3f;
                    break;
                case 4:
                    speed = 0.2f;
                    break;
                case 5:
                    speed = 0.1f;
                    break;
            }
            _speed = speed;
        }

        public bool IsElapsed()
        {
            bool ret = _elapsedTime >= _speed;
            if (ret) {
                _elapsedTime = 0;
            }
            return ret;
        }

        public void AddElapsedTime(float elapsed)
        {
            _elapsedTime += elapsed;
        }
    }
}
