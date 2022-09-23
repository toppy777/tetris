using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.PlacePredictions;

namespace Tetris.Scripts.Application.Minos
{
    public class MinoRotateRightUseCase
    {
        GameRegistry _gameRegistry;
        PlacePrediction _minoShadowService;

        public MinoRotateRightUseCase(
            GameRegistry gameRegistry,
            PlacePrediction minoShadowService
        )
        {
            _gameRegistry = gameRegistry;
            _minoShadowService = minoShadowService;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.Mino.RotateRight();
            List<Vector2Int> positionPredicted = _minoShadowService.GetPlacePrediction(game.Board, game.Mino);
            if (positionPredicted == null) {
                return;
            }
            game.MinoShadow.Set(positionPredicted);
        }
    }
}
