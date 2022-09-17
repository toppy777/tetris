using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Application.Minos
{
    public class CreateNextMinoUseCase
    {
        GameRegistry _gameRegistry;
        CreateMinoUseCase _createMinoUseCase;

        public CreateNextMinoUseCase(
            GameRegistry gameRegistry,
            CreateMinoUseCase createMinoUseCase
        )
        {
            _gameRegistry = gameRegistry;
            _createMinoUseCase = createMinoUseCase;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            _createMinoUseCase.Execute(game.MinoReserveList.PopMinoType());
            if (game.HoldMino.Exists) {
                if (!game.HoldMino.IsFirst) game.HoldMino.SetAvailable();
                game.HoldMino.SetNotFirst();
            }
        }
    }
}
