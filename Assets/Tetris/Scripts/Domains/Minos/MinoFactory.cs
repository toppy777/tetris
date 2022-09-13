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

        // public List<Mino> CreateMinos()
        // {
        //     return new List<Mino>(){
        //         CreateIMino(),
        //         CreateJMino(),
        //         CreateLMino(),
        //         CreateOMino(),
        //         CreateSMino(),
        //         CreateTMino(),
        //         CreateZMino()
        //     };
        // }

        public Mino CreateMino(MinoType minoType)
        {
            MinoShape minoShape = new MinoShape(minoType);
            MinoColor minoColor = new MinoColor(minoType);
            return new Mino(minoShape, minoColor, defaultPos, minoType);
        }

        // public Mino CreateIMino()
        // {
        //     MinoType minoType = MinoType.I;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.I;
        //     var color = MinoPieceColor.Color.LightBlue;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }

        // public Mino CreateOMino()
        // {
        //     MinoType minoType = MinoType.O;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.O;
        //     var color = MinoPieceColor.Color.Yellow;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }

        // public Mino CreateSMino()
        // {
        //     MinoType minoType = MinoType.S;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.S;
        //     var color = MinoPieceColor.Color.Green;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }
    
        // public Mino CreateZMino()
        // {
        //     MinoType minoType = MinoType.Z;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.Z;
        //     var color = MinoPieceColor.Color.Red;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }

        // public Mino CreateJMino()
        // {
        //     MinoType minoType = MinoType.J;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.J;
        //     var color = MinoPieceColor.Color.Blue;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }

        // public Mino CreateLMino()
        // {
        //     MinoType minoType = MinoType.L;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.L;
        //     var color = MinoPieceColor.Color.Orange;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }

        // public Mino CreateTMino()
        // {
        //     MinoType minoType = MinoType.T;
        //     List<List<Vector2Int>> shapePatten = MinoShapeType.T;
        //     var color = MinoPieceColor.Color.Purple;
        //     return new Mino(shapePatten, color, defaultPos, minoType);
        // }
    }
}
