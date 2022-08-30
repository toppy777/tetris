using UnityEngine;
using Tetris.Scripts.Domains.Minos;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.Boards
{
    public class Board
    {
        private readonly int _xMax;
        private readonly int _yMax;
        MinoPiece[,] _minoPieces;

        public Board()
        {
            _xMax = 10-1;
            _yMax = 20-1;
            _minoPieces = new MinoPiece[_xMax+1, _yMax+1];
        }

        public void Add(MinoPiece piece)
        {
            Vector2Int pos = piece.GetPosition();
            _minoPieces[pos.x, pos.y] = piece;

            // 横一行そろったか
            bool flg = true;
            for (int x = 0; x <= _xMax; x++) {
                if (IsEmpty(x, pos.y)) {
                    flg = false;
                }
            }
            if (flg) {
                Debug.Log($"{pos.y}行目 そろった");
                // その行全消し
                // for (int x = 0; x <= _xMax; x++) {
                //     _minoPieces[x, pos.y] = null;
                // }
            }
        }

        public bool IsEmpty(int x, int y)
        {
            return _minoPieces[x,y] == null;
        }

        public bool CheckBoundary(int x, int y)
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
            if (CheckBoundary(x, y)) {
                if (IsEmpty(x, y)) {
                    return true;
                }
            }
            return false;
        }
    }
}
