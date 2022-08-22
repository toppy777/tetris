using UnityEngine;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoFactory
    {
        Vector2Int defaultPos = new Vector2Int(3, 18);

        public Mino CreateIMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.I;
            var color = MinoPieceColor.LightBlue;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateOMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.O;
            var color = MinoPieceColor.Yellow;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateSMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.S;
            var color = MinoPieceColor.Green;
            return new Mino(shapePatten, color, defaultPos);
        }
    
        public Mino CreateZMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.Z;
            var color = MinoPieceColor.Red;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateJMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.J;
            var color = MinoPieceColor.Blue;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateLMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.L;
            var color = MinoPieceColor.Orange;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateTMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.T;
            var color = MinoPieceColor.Purple;
            return new Mino(shapePatten, color, defaultPos);
        }
    }
}
