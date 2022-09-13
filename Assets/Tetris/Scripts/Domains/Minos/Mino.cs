using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;
using System.Linq;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Domains.MinoShapes;
using Tetris.Scripts.Domains.MinoColors;
using Tetris.Scripts.Domains.Positions;
using Tetris.Scripts.Domains.MinoPieces;

namespace Tetris.Scripts.Domains.Minos
{
    public class Mino
    {
        private List<MinoPiece> _pieces;
        private Position _position;
        public Position Position {
            get { return _position; }
        }

        private MinoType _minoType;
        public MinoType MinoType {
            get { return _minoType; }
        }

        private MinoShape _shape;
        private MinoColor _color;
        public Color32 Color {
            get { return _color.Value; }
        }

        Subject<Color32> _whenMinoSet;
        public IObservable<Color32> WhenMinoSet => _whenMinoSet;

        public Mino(MinoShape minoShape, MinoColor minoColor, Vector2Int position, MinoType minoType)
        {
            _shape = minoShape;
            _pieces = new List<MinoPiece>();
            List<Vector2Int> s = _shape.GetShape();
            foreach (Vector2Int pos in s) {
                var piece = new MinoPiece(pos.x + position.x, pos.y + position.y);
                _pieces.Add(piece);
            }
            _color = minoColor;
            _position = new Position(position.x, position.y);
            _minoType = minoType;
            _whenMinoSet = new Subject<Color32>();
        }

        public void Set(Mino mino)
        {
            _pieces = mino._pieces;
            _position = mino._position;
            _minoType = mino._minoType;
            _shape = mino._shape;
            _color = mino._color;
            _whenMinoSet.OnNext(mino._color.Value);
        }

        public void Release()
        {
            _pieces = null;
            _position = null;
            _shape = null;
            _color = null;
        }
        
        public bool Exists()
        {
            return _pieces != null;
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
            return _shape.GetShape();
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
            _shape.Next();
            ChangeShape();
        }

        public void RotateLeft()
        {
            _shape.Prev();
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
            return _shape.GetNextShape();
        }

        public List<Vector2Int> GetPrevShape()
        {
            return _shape.GetPrevShape();
        }

        public void Delete()
        {
            for (int i = 0; i < 4; i++) {
                _pieces[i].Delete();
            }
            _pieces.Clear();
        }
    }
}
