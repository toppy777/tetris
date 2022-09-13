using VContainer.Unity;

namespace Tetris.Scripts.Application.Games
{
    public class GameInitializer : IInitializable
    {
        private readonly CreateGameUseCase _createGameUseCase;

        public GameInitializer(
            CreateGameUseCase createGameUseCase
        )
        {
            _createGameUseCase = createGameUseCase;
        }

        public void Initialize()
        {
            _createGameUseCase.Execute();
        }
    }
}
