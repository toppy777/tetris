using UnityEngine;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.Minos;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.MinoShadows
{
    public class MinoShadowService
    {
        BoardService _boardService;

        public MinoShadowService(
            BoardService boardService
        ) {
            _boardService = boardService;
        }

        /// <summary>
        /// MinoShadowの配置予測位置を返す
        /// </summary>
        public List<Vector2Int> GetMinoShadowPositions(Board board, Mino mino)
        {
            // TODO: もっと汎用的なクラスに移動した方が良い
            for (int y = 0; y < mino.Position.Y; y++) {
                if (_boardService.HasSpaceForMino(board, mino, new Vector2Int(mino.Position.X, y))) {
                    return mino.GetPiecePositionsAt(mino.Position.X, y);
                }
            }
            return null;
        }
    }
}
