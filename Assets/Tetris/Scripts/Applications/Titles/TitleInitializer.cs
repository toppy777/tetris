using UnityEngine.Events;
using UnityEngine.SceneManagement;
using VContainer.Unity;
using Tetris.Scripts.Domains.Titles;
using Tetris.Scripts.Infrastructures.BetweenScenes;

namespace Tetris.Scripts.Application.Titles
{
    public class TitleInitializer : IInitializable
    {
        IPlayButton _playButton;
        IPracticeButton _practiceButton;
        ModeRepository _modeRepository;

        public TitleInitializer(
            IPlayButton playButton,
            IPracticeButton practiceButton
        ) {
            _playButton = playButton;
            _practiceButton = practiceButton;
            _modeRepository = new ModeRepository();
        }

        public void Initialize()
        {
            _playButton.SetAction(() => {
                _modeRepository.SetMode(ModeType.Play);
                SceneManager.LoadScene("SampleScene");
            });
            _practiceButton.SetAction(() => {
                _modeRepository.SetMode(ModeType.Practice);
                SceneManager.LoadScene("SampleScene");
            });
        }
    }
}
