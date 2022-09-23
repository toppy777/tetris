using UnityEngine;
using VContainer.Unity;
using UnityEngine.SceneManagement;
using Tetris.Scripts.Domains.Audios;

namespace Tetris.Scripts.Application.Games
{
    public class GameInitializer : IInitializable
    {
        private readonly CreateGameUseCase _createGameUseCase;
        IAudio _audio;

        public GameInitializer(
            CreateGameUseCase createGameUseCase,
            IAudio audio
        )
        {
            _createGameUseCase = createGameUseCase;
        }

        public void Initialize()
        {
            Debug.Log("Initialize!");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "SampleScene") {
                _createGameUseCase.Execute();
            }
        }
    }
}
