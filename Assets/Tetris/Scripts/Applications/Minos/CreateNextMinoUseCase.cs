using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoReserves;

namespace Tetris.Scripts.Application.Minos
{
    public class CreateNextMinoUseCase
    {
        GameRegistry _gameRegistry;
        CreateMinoUseCase _createMinoUseCase;
        INextMinoBindFactory _nextMinoBindFactory;

        public CreateNextMinoUseCase(
            GameRegistry gameRegistry,
            CreateMinoUseCase createMinoUseCase,
            INextMinoBindFactory nextMinoBindFactory
        )
        {
            _gameRegistry = gameRegistry;
            _createMinoUseCase = createMinoUseCase;
            _nextMinoBindFactory = nextMinoBindFactory;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            _createMinoUseCase.Execute(game.MinoReserveList.PopMinoType());
            game.NextMinoBind?.Dispose();
            game.NextMinoBind = _nextMinoBindFactory.CreateNextMinoBind(game.MinoReserveList);
            if (game.HoldMino.Exists) {
                if (!game.HoldMino.IsFirst) game.HoldMino.SetAvailable();
                game.HoldMino.SetNotFirst();
            }
        }
    }
}
