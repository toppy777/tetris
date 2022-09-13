using UnityEngine;
using Tetris.Scripts.Domains.MinoTypes;

namespace Tetris.Scripts.Domains.MinoColors
{
    public class MinoColor
    {
        Color32 _value;

        public Color32 Value {
            get { return _value; }
        }

        public MinoColor(MinoType minoType)
        {
            _value = GetColor(minoType);
        }

        public static Color32 GetColor(MinoType minoType)
        {
            switch(minoType)
            {
                default:
                case MinoType.I:
                    return new Color32(65,215,255,255);
                case MinoType.O:
                    return new Color32(255,255,55,255);
                case MinoType.S:
                    return new Color32(70,255,115,255);
                case MinoType.Z:
                    return new Color32(255,30,70,255);
                case MinoType.J:
                    return new Color32(0,125,255,255);
                case MinoType.L:
                    return new Color32(255,145,0,255);
                case MinoType.T:
                    return new Color32(200,90,255,255);
            }
        }
    }
}
