using UnityEngine.Events;
using UnityEngine.SceneManagement;
using VContainer.Unity;
using Tetris.Scripts.Domains.Titles;
using Tetris.Scripts.Infrastructures.BetweenScenes;
using Tetris.Scripts.Domains.Audios;
using Tetris.Scripts.Infrastructures.Sounds;
using UnityEngine;

namespace Tetris.Scripts.Application.Titles
{
    public class TitleInitializer : IInitializable
    {
        IPlayButton _playButton;
        IPracticeButton _practiceButton;
        ModeRepository _modeRepository;
        IAudio _audio;
        AudioClip _audioClip;

        public TitleInitializer(
            IPlayButton playButton,
            IPracticeButton practiceButton,
            IAudio audio
        ) {
            _playButton = playButton;
            _practiceButton = practiceButton;
            _modeRepository = new ModeRepository();
            _audio = audio;
            _audioClip = SoundRepository.GetSoundPlaceMino();
        }

        public void Initialize()
        {
            _playButton.SetAction(() => {
                _modeRepository.SetMode(ModeType.Play);
                _audio.PlaySound(_audioClip);
                SceneManager.LoadScene("SampleScene");
            });
            _practiceButton.SetAction(() => {
                _modeRepository.SetMode(ModeType.Practice);
                _audio.PlaySound(_audioClip);
                SceneManager.LoadScene("SampleScene");
            });
        }
    }
}
