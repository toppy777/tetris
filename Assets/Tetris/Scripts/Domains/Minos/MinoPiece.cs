using UnityEngine;
using UniRx;
using System;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoPiece
    {
        private readonly MinoPiecePosition _position;
        public int X {
            get { return _position.X; }
        }
        public int Y {
            get { return _position.Y; }
        }

        Subject<Vector2Int> _whenChangePosition;
        public IObservable<Vector2Int> WhenChangePosition => _whenChangePosition;

        Subject<Unit> _whenDelete;
        public IObservable<Unit> WhenDelete => _whenDelete;

        public MinoPiece(int x, int y)
        {
            _position = new MinoPiecePosition(x,y);
            _whenChangePosition = new Subject<Vector2Int>();
            _whenDelete = new Subject<Unit>();
        }

        public Vector2Int GetPosition()
        {
            return new Vector2Int(_position.X, _position.Y);
        }

        public void ChangePosition(int x, int y)
        {
            _position.Change(x,y);
            _whenChangePosition.OnNext(new Vector2Int(x,y));
        }

        public void Delete()
        {
            _whenDelete.OnNext(Unit.Default);
        }
    }
}
