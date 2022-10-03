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
                    speed = 0.45f;
                    break;
                case 3:
                    speed = 0.40f;
                    break;
                case 4:
                    speed = 0.35f;
                    break;
                case 5:
                    speed = 0.30f;
                    break;
                case 6:
                    speed = 0.20f;
                    break;
                case 7:
                    speed = 0.15f;
                    break;
                case 8:
                    speed = 0.10f;
                    break;
                case 9:
                    speed = 0.05f;
                    break;
                case 10:
                    speed = 0.02f;
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
