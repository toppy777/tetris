using UnityEngine;
using System;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.MinoPieces;
namespace Tetris.Scripts.Domains.Boards
{
    public class Board
    {
        private readonly int _xMax;
        public int XMax => _xMax;
        private readonly int _yMax;
        public int YMax => _yMax;
        MinoPiece[,] _piecesList;

        Subject<Unit> _whenPieceCrossOver = new();
        public IObservable<Unit> WhenPieceCrossOver => _whenPieceCrossOver;

        Subject<Unit> _whenRowRemove = new();
        public IObservable<Unit> WhenRowRemove => _whenRowRemove;

        public Board()
        {
            _xMax = 10-1;
            _yMax = 24;
            _piecesList = new MinoPiece[_xMax+1, _yMax+1];
        }

        public void Add(Mino mino)
        {
            // MinoをPlacedMinoに変換して追加
            foreach (MinoPiece piece in mino.GetPieces()){
                if (piece.Y > 19) {
                    _whenPieceCrossOver.OnNext(Unit.Default);
                }
                _piecesList[piece.X, piece.Y] = piece;
            }

            int y = 0;
            while(y <= _yMax) {
                // 横一行そろったか
                if (CheckCompleteRow(y)) {
                    RemoveRow(y);
                    _whenRowRemove.OnNext(Unit.Default);
                    // そろった行の一段上からすべてのオブジェクトを一段下げる
                    for (int yy = y + 1; yy <= _yMax - 4; yy++) {
                        MoveDownRow(yy);
                    }
                } else {
                    y++;
                }
            }
        }

        public bool CheckCompleteRow(int y)
        {
            for (int x = 0; x <= _xMax; x++) {
                if (IsEmptyAt(x,y)) {
                    return false;
                }
            }
            return true;
        }

        public void RemoveRow(int y)
        {
            for (int x = 0; x <= _xMax; x++) {
                RemoveAt(x,y);
            }
        }

        public void RemoveAt(int x, int y)
        {
            _piecesList[x,y].Delete();
            _piecesList[x,y].Dispose();
            _piecesList[x,y] = null;
        }

        public void MoveDownRow(int y)
        {
            for (int x = 0; x <= _xMax; x++) {
                if (IsEmptyAt(x,y)) {
                    continue;
                }
                _piecesList[x,y].ChangePosition(_piecesList[x,y].X, _piecesList[x,y].Y-1);
                _piecesList[x,y-1] = _piecesList[x,y];
                _piecesList[x,y] = null;
            }
        }

        public void DisplayConsole()
        {
            string data = "";
            for (int y = _yMax; y >= 0; y--) {
                for (int x = 0; x <= _xMax; x++) {
                    if (IsEmptyAt(x,y)) {
                        data += "○";
                    } else {
                        data += "●";
                    }
                }
                data += "\n";
            }
            Debug.Log(data);
        }

        public bool IsEmptyAt(int x, int y)
        {
            return _piecesList[x,y] == null;
        }

        public bool CheckBoundaryAt(int x, int y)
        {
            if (x < 0) {
                return false;
            }
            if (y < 0) {
                return false;
            }
            if (x > _xMax) {
                return false;
            }
            if (y > _yMax) {
                return false;
            }

            return true;
        }

        public bool IsAvailable(int x, int y)
        {
            if (CheckBoundaryAt(x, y)) {
                if (IsEmptyAt(x, y)) {
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            for (int y = 0; y < _yMax; y++) {
                for (int x = 0; x < _xMax; x++) {
                    if (!IsEmptyAt(x,y)) {
                        RemoveAt(x,y);
                    }
                }
            }
        }

        public void Dispose()
        {
            _whenPieceCrossOver.Dispose();
            _whenRowRemove.Dispose();
        }
    }
}
