using UnityEngine;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.Minos;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.PlacePredictions
{
    public class PlacePrediction
    {
        BoardService _boardService;

        public PlacePrediction(
            BoardService boardService
        ) {
            _boardService = boardService;
        }

        /// <summary>
        /// Minoの配置予測位置を返す
        /// </summary>
        public List<Vector2Int> GetPlacePrediction(Board board, Mino mino)
        {
            for (int y = 0; y < mino.Position.Y; y++) {
                if (_boardService.HasSpaceForMino(board, mino, new Vector2Int(mino.Position.X, y))) {
                    return mino.GetPiecePositionsAt(mino.Position.X, y);
                }
            }
            return null;
        }
    }
}
