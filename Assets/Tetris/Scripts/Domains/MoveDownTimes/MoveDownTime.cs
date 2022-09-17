using UnityEngine;

namespace Tetris.Scripts.Domains.MoveDownTimes
{
    public class MoveDownTime
    {
        public static float GetSeconds(int level)
        {
            switch(level) {
                default:
                case 1:
                    return 0.5f;
                case 2:
                    return 0.4f;
                case 3:
                    return 0.3f;
                case 4:
                    return 0.2f;
                case 5:
                    return 0.1f;
            }
        }
    }
}
