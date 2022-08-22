using UnityEngine;
using UniRx;
using System;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoPiece
    {
        private readonly ReactiveProperty<MinoPiecePosition> _position = new();
        private readonly MinoPieceColor _color;

        public MinoPiece(int x, int y, MinoPieceColor color)
        {
            _position.Value = new MinoPiecePosition(x,y);
            _color = color;
        }

        public IObservable<MinoPiecePosition> WhenPositionChange => _position;

        public void Move(MinoPiecePosition position) => _position.Value = position;

        public void MoveDown()
        {
            _position.Value.Change(_position.Value.X, _position.Value.Y-1);
        }

        public Vector2Int GetPosition()
        {
            return new Vector2Int(_position.Value.X, _position.Value.Y);
        }
    }
}
