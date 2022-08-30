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
            _whenPositionChange = new Subject<Vector2Int>();
        }

        private readonly Subject<Vector2Int> _whenPositionChange;
        public IObservable<Vector2Int> WhenPositionChange => _whenPositionChange;

        public void Move(MinoPiecePosition position) => _position.Value = position;

        public void MoveDown()
        {
            _position.Value.Change(_position.Value.X, _position.Value.Y-1);
            _whenPositionChange.OnNext(new Vector2Int(_position.Value.X, _position.Value.Y));
        }

        public void MoveRight()
        {
            _position.Value.Change(_position.Value.X+1, _position.Value.Y);
            _whenPositionChange.OnNext(new Vector2Int(_position.Value.X, _position.Value.Y));
        }

        public void MoveLeft()
        {
            _position.Value.Change(_position.Value.X-1, _position.Value.Y);
            _whenPositionChange.OnNext(new Vector2Int(_position.Value.X, _position.Value.Y));
        }

        public Vector2Int GetPosition()
        {
            return new Vector2Int(_position.Value.X, _position.Value.Y);
        }

        public void ChangePosition(int x, int y)
        {
            _position.Value.Change(x,y);
            _whenPositionChange.OnNext(new Vector2Int(_position.Value.X, _position.Value.Y));
        }
    }
}
