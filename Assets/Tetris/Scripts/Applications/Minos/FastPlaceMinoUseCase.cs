using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.PlacePredictions;
using Tetris.Scripts.Domains.Audios;
using Tetris.Scripts.Infrastructures.Sounds;

namespace Tetris.Scripts.Application.Minos
{
    public class FastPlaceMinoUseCase
    {
        GameRegistry _gameRegistry;
        PlacePrediction _minoShadowService;
        CreateNextMinoUseCase _createNextMinoUseCase;
        IAudio _audio;
        AudioClip _audioClip;

        public FastPlaceMinoUseCase(
            GameRegistry gameRegistry,
            PlacePrediction minoShadowService,
            CreateNextMinoUseCase createNextMinoUseCase,
            IAudio audio
        )
        {
            _gameRegistry = gameRegistry;
            _minoShadowService = minoShadowService;
            _createNextMinoUseCase = createNextMinoUseCase;
            _audio = audio;
            _audioClip = SoundRepository.GetSoundPlaceMino();
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            List<Vector2Int> positionPredicted = _minoShadowService.GetPlacePrediction(game.Board, game.Mino);
            if (positionPredicted == null) {
                return;
            }
            game.Mino.MoveTo(positionPredicted);
            game.Board.Add(game.Mino);
            _audio.PlaySound(_audioClip);
            game.Mino.Release();
            game.HorizontalPosition.Reset();
            _createNextMinoUseCase.Execute();
        }
    }
}
