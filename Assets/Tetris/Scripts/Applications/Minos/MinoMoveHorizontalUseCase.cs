using UnityEngine;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.HorizontalPositions;

namespace Tetris.Scripts.Application.Minos
{
    public class MinoMoveHorizontalUseCase
    {
        GameRegistry _gameRegistry;
        MinoShadowService _minoShadowService;
        BoardService _boardService;

        public MinoMoveHorizontalUseCase(
            GameRegistry gameRegistry,
            MinoShadowService minoShadowService,
            BoardService boardService
        )
        {
            _gameRegistry = gameRegistry;
            _minoShadowService = minoShadowService;
            _boardService = boardService;
        }
        
        public void TryExecute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.HorizontalPosition.Set(HorizontalPosition.GetHorizontalPos());
            if (_boardService.HasSpaceForMino(game.Board, game.Mino, new Vector2Int(game.HorizontalPosition.Value, game.Mino.Position.Y))) {
                game.Mino.MoveTo(game.HorizontalPosition.Value, game.Mino.Position.Y);
                Debug.Log(game.Mino);
                game.MinoShadow.Set(_minoShadowService.GetMinoShadowPositions(game.Board, game.Mino));
            }
        }
    }
}
