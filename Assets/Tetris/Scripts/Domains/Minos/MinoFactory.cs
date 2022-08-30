using UnityEngine;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoFactory
    {
        Vector2Int defaultPos = new Vector2Int(3, 18);

        public List<Mino> CreateMinos()
        {
            return new List<Mino>(){
                CreateIMino(),
                CreateJMino(),
                CreateLMino(),
                CreateOMino(),
                CreateSMino(),
                CreateTMino(),
                CreateZMino()
            };
        }

        public Mino CreateIMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.I;
            var color = MinoPieceColor.Color.LightBlue;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateOMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.O;
            var color = MinoPieceColor.Color.Yellow;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateSMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.S;
            var color = MinoPieceColor.Color.Green;
            return new Mino(shapePatten, color, defaultPos);
        }
    
        public Mino CreateZMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.Z;
            var color = MinoPieceColor.Color.Red;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateJMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.J;
            var color = MinoPieceColor.Color.Blue;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateLMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.L;
            var color = MinoPieceColor.Color.Orange;
            return new Mino(shapePatten, color, defaultPos);
        }

        public Mino CreateTMino()
        {
            List<List<Vector2Int>> shapePatten = MinoShapeType.T;
            var color = MinoPieceColor.Color.Purple;
            return new Mino(shapePatten, color, defaultPos);
        }
    }
}
