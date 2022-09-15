using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Domains.MinoShapes;
using Tetris.Scripts.Domains.MinoColors;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoFactory
    {
        Vector2Int defaultPos = new Vector2Int(3, 21);

        public Mino CreateMino(MinoType minoType)
        {
            MinoShape minoShape = new MinoShape(minoType);
            MinoColor minoColor = new MinoColor(minoType);
            return new Mino(minoShape, minoColor, defaultPos, minoType);
        }
    }
}
