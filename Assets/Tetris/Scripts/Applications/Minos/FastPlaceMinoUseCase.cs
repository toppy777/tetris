using UnityEngine;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Domains.Audios;
using Tetris.Scripts.Infrastructures.Sounds;

namespace Tetris.Scripts.Application.Minos
{
    public class FastPlaceMinoUseCase
    {
        GameRegistry _gameRegistry;
        MinoShadowService _minoShadowService;
        IAudio _audio;
        AudioClip _audioClip;

        public FastPlaceMinoUseCase(
            GameRegistry gameRegistry,
            MinoShadowService minoShadowService,
            IAudio audio
        )
        {
            _gameRegistry = gameRegistry;
            _minoShadowService = minoShadowService;
            _audio = audio;
            _audioClip = SoundRepository.GetSoundPlaceMino();
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.Mino.MoveTo(_minoShadowService.GetMinoShadowPositions(game.Board, game.Mino));
            game.Board.Add(game.Mino);
            _audio.PlaySound(_audioClip);
            game.Mino.Release();
            game.HorizontalPosition.Reset();
        }
    }
}
