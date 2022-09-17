using UnityEngine;

namespace Tetris.Scripts.Domains.HorizontalPositions
{
    public class HorizontalPosition
    {
        int _value = -1;
        public int Value => _value;

        public void Set(int posX)
        {
            _value = posX;
        }

        public void Reset()
        {
            _value = -1;
        }

        public static int GetHorizontalPos()
        {
            Vector3 cameraPosition = Input.mousePosition;
            cameraPosition.z = 10.0f;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(cameraPosition);
            return GetPositionX(mousePos.x);
        }

        static int GetPositionX(float posX)
        {
            int ret = Mathf.FloorToInt(posX / 0.16f) - 1;
            if (ret < 0) {
                ret = 0;
            }
            if (ret > 9) {
                ret = 9;
            }
            return ret;
        }
    }
}
