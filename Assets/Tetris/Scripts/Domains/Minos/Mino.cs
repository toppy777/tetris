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

        public Mino(List<List<Vector2Int>> shapePattens, MinoPieceColor color, Vector2Int position)
        {
            _pieces = new List<MinoPiece>();
            _position = new MinoPiecePosition(position.x, position.y);
            _shapePattens = new MinoShapePattens(shapePattens);
            List<Vector2Int> shape = _shapePattens.GetShape();
            foreach (Vector2Int pos in shape) {
                var piece = new MinoPiece(pos.x + position.x, pos.y + position.y, color);
                _pieces.Add(piece);
            }
        }

        public List<Vector2Int> GetPiecePositions()
        {
            List<Vector2Int> list = new List<Vector2Int>();
            foreach (MinoPiece piece in _pieces) {
                list.Add(piece.GetPosition());
            }
            return list;
        }

        public List<Vector2Int> GetShape()
        {
            return _shapePattens.GetShape();
        }

        public void MoveDown()
        {
            foreach (MinoPiece piece in _pieces) {
                piece.MoveDown();
            }
            _position.Change(_position.X, _position.Y-1);
        }

        public void MoveRight()
        {
            foreach (MinoPiece piece in _pieces) {
                piece.MoveRight();
            }
            _position.Change(_position.X+1, _position.Y);
        }

        public void MoveLeft()
        {
            foreach (MinoPiece piece in _pieces) {
                piece.MoveLeft();
            }
            _position.Change(_position.X-1, _position.Y);
        }

        public void MoveTo(List<Vector2Int> piecePositions)
        {
            for (int i = 0; i < piecePositions.Count; i++) {
                _pieces[i].ChangePosition(piecePositions[i].x, piecePositions[i].y);
            }
            int yMin = piecePositions.Select(pos => pos.y).ToList().Min();
            _position.Change(_position.X, yMin);
        }

        public void RotateRight()
        {
            _shapePattens.Next();
            List<Vector2Int> shape = _shapePattens.GetShape();
            for (int i = 0; i < _pieces.Count; i++) {
                _pieces[i].ChangePosition(shape[i].x + _position.X, shape[i].y + _position.Y);
            }
        }

        public void RotateLeft()
        {
            _shapePattens.Prev();
            List<Vector2Int> shape = _shapePattens.GetShape();
            for (int i = 0; i < _pieces.Count; i++) {
                _pieces[i].ChangePosition(shape[i].x + _position.X, shape[i].y + _position.Y);
            }
        }

        public bool ExitsMinoPiece(int x, int y)
        {
            foreach (MinoPiece piece in _pieces) {
                Vector2Int indexPos = piece.GetPosition();
                if (x == indexPos.x && y == indexPos.y) {
                    return true;
                }
            }
            return false;
        }

        public List<MinoPiece> GetMinoPieces()
        {
            return _pieces;
        }

        public void DeletePiece(MinoPiece piece)
        {
            _pieces.Remove(piece);
        }
    }
}
