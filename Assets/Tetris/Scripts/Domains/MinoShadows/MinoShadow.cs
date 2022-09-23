using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;
using Tetris.Scripts.Domains.Positions;

namespace Tetris.Scripts.Domains.PlacePredictions
{
    public class MinoShadow
    {
        List<Position> _piecePositions = new();
        Subject<List<Vector2Int>> _whenPositionsChange = new();
        public IObservable<List<Vector2Int>> WhenPositionsChange => _whenPositionsChange;

        public void Set(List<Vector2Int> piecePositions)
        {
            _piecePositions.Clear();
            foreach (Vector2Int pos in piecePositions) {
                _piecePositions.Add(new Position(pos.x, pos.y));
            }
            _whenPositionsChange.OnNext(GetPositions());
        }

        public List<Vector2Int> GetPositions()
        {
            var list = new List<Vector2Int>();
            foreach (Position pos in _piecePositions) {
                list.Add(new Vector2Int(pos.X, pos.Y));
            }
            return list;
        }
    }
}
