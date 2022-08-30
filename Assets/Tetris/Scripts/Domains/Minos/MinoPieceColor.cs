using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Scripts.Domains.Minos
{
    public static class MinoPieceColor
    {
        public enum Color
        {
            LightBlue,
            Yellow,
            Green,
            Red,
            Blue,
            Orange,
            Purple,
        }

        public static Color32 GetColor(Color color)
        {
            switch(color)
            {
                default:
                case Color.LightBlue:
                    return new Color32(65,215,255,255);
                case Color.Yellow:
                    return new Color32(255,255,55,255);
                case Color.Green:
                    return new Color32(70,255,115,255);
                case Color.Red:
                    return new Color32(255,30,70,255);
                case Color.Blue:
                    return new Color32(0,125,255,255);
                case Color.Orange:
                    return new Color32(255,145,0,255);
                case Color.Purple:
                    return new Color32(200,90,255,255);
            }
        }
    }
}
