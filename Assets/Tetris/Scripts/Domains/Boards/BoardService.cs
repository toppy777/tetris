using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.Minos;

namespace Tetris.Scripts.Domains.Boards
{
    public class BoardService
    {
        /// <summary>
        /// x,yの移動距離を受け取り、その座標に空きがあるか確認する
        /// </summary>
        public bool CanMove(int distX, int distY, Board board, Mino mino)
        {
            List<Vector2Int> positions = mino.GetPiecePositions();
            foreach(Vector2Int indexPos in positions) {
                if (!board.IsAvailable(indexPos.x + distX, indexPos.y + distY)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 与えられたMinoと座標をもとにそのミノが利用可能なスペースがあるかチェックする
        /// </summary>
        public bool HasSpaceForMino(Board board, Mino mino, Vector2Int position) {
            foreach (Vector2Int shape in mino.GetShape()) {
                if (!board.IsAvailable(shape.x + position.x, shape.y + position.y)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 与えられたMinoとシェイプをもとにそのミノが利用可能なスペースがあるかチェックする
        /// </summary>
        public bool HasSpaceForMino(Board board, Mino mino, List<Vector2Int> shapes) {
            foreach (Vector2Int shape in shapes) {
                if (!board.IsAvailable(shape.x + mino.Position.X, shape.y + mino.Position.Y)) {
                    return false;
                }
            }
            return true;
        }
    }
}