using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;
using Tetris.Scripts.Domains.Minos;

namespace Tetris.Scripts.Domains.MinoShadows
{
    public class MinoShadow
    {
        List<MinoPiecePosition> _piecePositions;
        Subject<List<Vector2Int>> _whenPositionsChange;
        public IObservable<List<Vector2Int>> WhenPositionsChange => _whenPositionsChange;
        ReactiveProperty<bool> _displayFlg;
        public IObservable<bool> WhenDisplayFlgChange => _displayFlg;

        public MinoShadow()
        {
            _piecePositions = new List<MinoPiecePosition>();
            _whenPositionsChange = new Subject<List<Vector2Int>>();
            _displayFlg = new ReactiveProperty<bool>();
        }

        public void Set(List<Vector2Int> piecePositions)
        {
            _piecePositions.Clear();
            foreach (Vector2Int pos in piecePositions) {
                _piecePositions.Add(new MinoPiecePosition(pos.x, pos.y));
            }
            _whenPositionsChange.OnNext(GetPositions());
        }

        public List<Vector2Int> GetPositions()
        {
            var list = new List<Vector2Int>();
            foreach (MinoPiecePosition pos in _piecePositions) {
                list.Add(new Vector2Int(pos.X, pos.Y));
            }
            return list;
        }

        public void Display()
        {
            _displayFlg.Value = true;
        }

        public void UnDisplay()
        {
            _displayFlg.Value = false;
        }
    }
}
