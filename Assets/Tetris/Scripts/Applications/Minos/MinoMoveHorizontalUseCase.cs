using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.PlacePredictions;
using Tetris.Scripts.Domains.HorizontalPositions;

namespace Tetris.Scripts.Application.Minos
{
    public class MinoMoveHorizontalUseCase
    {
        GameRegistry _gameRegistry;
        PlacePrediction _minoShadowService;
        BoardService _boardService;

        public MinoMoveHorizontalUseCase(
            GameRegistry gameRegistry,
            PlacePrediction minoShadowService,
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
                List<Vector2Int> positionPredicted = _minoShadowService.GetPlacePrediction(game.Board, game.Mino);
                if (positionPredicted == null) {
                    return;
                }
                game.MinoShadow.Set(positionPredicted);
            }
        }
    }
}
