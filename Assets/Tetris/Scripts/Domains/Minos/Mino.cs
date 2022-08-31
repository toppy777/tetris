using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;
using System.Linq;

namespace Tetris.Scripts.Domains.Minos
{
    public class Mino
    {
        private List<MinoPiece> _pieces;
        private MinoPiecePosition _position;
        public MinoPiecePosition Position {
            get { return _position; }
        }
        private readonly MinoShapePattens _shapePattens;

        MinoPieceColor.Color _color;
        public MinoPieceColor.Color Color {
            get { return _color; }
        }

        public Mino(List<List<Vector2Int>> shapePattens, MinoPieceColor.Color color, Vector2Int position)
        {
            _pieces = new List<MinoPiece>();
            _position = new MinoPiecePosition(position.x, position.y);
            _shapePattens = new MinoShapePattens(shapePattens);
            List<Vector2Int> shape = _shapePattens.GetShape();
            foreach (Vector2Int pos in shape) {
                var piece = new MinoPiece(pos.x + position.x, pos.y + position.y);
                _pieces.Add(piece);
            }
            _color = color;
        }

        public List<MinoPiece> GetPieces()
        {
            return _pieces;
        }

        public List<Vector2Int> GetPiecePositions()
        {
            List<Vector2Int> list = new List<Vector2Int>();
            foreach (MinoPiece piece in _pieces) {
                list.Add(piece.GetPosition());
            }
            return list;
        }

        public List<Vector2Int> GetPiecePositionsAt(int posX, int posY)
        {
            var list = new List<Vector2Int>();
            foreach (Vector2Int shape in GetShape()) {
                list.Add(new Vector2Int(shape.x + posX, shape.y + posY));
            }
            return list;
        }

        public List<Vector2Int> GetShape()
        {
            return _shapePattens.GetShape();
        }

        public void MoveTo(List<Vector2Int> piecePositions)
        {
            for (int i = 0; i < piecePositions.Count; i++) {
                _pieces[i].ChangePosition(piecePositions[i].x, piecePositions[i].y);
            }
            int xMin = piecePositions.Select(pos => pos.x).ToList().Min();
            int yMin = piecePositions.Select(pos => pos.y).ToList().Min();
            ChangePosition(xMin, yMin);
        }

        public void MoveTo(int x, int y)
        {
            ChangePosition(x,y);
        }

        public void RotateRight()
        {
            _shapePattens.Next();
            ChangeShape();
        }

        public void RotateLeft()
        {
            _shapePattens.Prev();
            ChangeShape();
        }

        private void ChangePosition(int x, int y)
        {
            _position.Change(x,y);
            List<Vector2Int> piecePositions = GetPiecePositionsAt(_position.X, _position.Y);
            for (int i = 0; i < _pieces.Count; i++) {
                _pieces[i].ChangePosition(piecePositions[i].x, piecePositions[i].y);
            }
        }

        private void ChangeShape()
        {
            List<Vector2Int> piecePositions = GetPiecePositionsAt(_position.X, _position.Y);
            for (int i = 0; i < _pieces.Count; i++) {
                _pieces[i].ChangePosition(piecePositions[i].x, piecePositions[i].y);
            }
        }

        public List<IObservable<Vector2Int>> GetWhenChangePositionObservables()
        {
            var list = new List<IObservable<Vector2Int>>();
            foreach (MinoPiece piece in _pieces) {
                list.Add(piece.WhenChangePosition);
            }
            return list;
        }

        public List<IObservable<Unit>> GetWhenDeleteObservables()
        {
            var list = new List<IObservable<Unit>>();
            foreach (MinoPiece piece in _pieces) {
                list.Add(piece.WhenDelete);
            }
            return list;
        }

        public void RemoveAt(int x, int y)
        {
            foreach (MinoPiece piece in _pieces) {
                if (piece.X == x && piece.Y == y) {
                    piece.Delete();
                    _pieces.Remove(piece);

                    if (_pieces.Count == 0) {
                        return;
                    }

                    // ポジジョン変更
                    int xMin = _pieces.Select(piece => piece.X).ToList().Min();
                    int yMin = _pieces.Select(piece => piece.Y).ToList().Min();
                    _position.Change(xMin, yMin);

                    return;
                }
            }
        }

        public List<Vector2Int> GetNextShape()
        {
            return _shapePattens.GetNextShape();
        }

        public List<Vector2Int> GetPrevShape()
        {
            return _shapePattens.GetPrevShape();
        }
    }
}
