using UnityEngine;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Audios;
using Tetris.Scripts.Infrastructures.Sounds;

namespace Tetris.Scripts.Application.Minos
{
    public class PlaceMinoUseCase
    {
        GameRegistry _gameRegistry;
        IAudio _audio;
        AudioClip _audioClip;

        public PlaceMinoUseCase(
            GameRegistry gameRegistry,
            IAudio audio
        )
        {
            _gameRegistry = gameRegistry;
            _audio = audio;
            _audioClip = SoundRepository.GetSoundPlaceMino();
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.Board.Add(game.Mino);
            _audio.PlaySound(_audioClip);
            game.Mino.Release();
            game.HorizontalPosition.Reset();
        }
    }
}
