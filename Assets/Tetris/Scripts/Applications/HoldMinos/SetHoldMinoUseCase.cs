using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Application.HoldMinos
{
    public class SetHoldMinoUseCase
    {
        GameRegistry _gameRegistry;
        CreateMinoUseCase _createMinoUseCase;
        IHoldMinoBindFactory _holdMinoBindFactory;

        public SetHoldMinoUseCase(
            GameRegistry gameRegistry,
            CreateMinoUseCase createMinoUseCase,
            IHoldMinoBindFactory holdMinoBindFactory
        )
        {
            _gameRegistry = gameRegistry;
            _createMinoUseCase = createMinoUseCase;
            _holdMinoBindFactory = holdMinoBindFactory;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.HoldMino.Set(game.Mino.MinoType);
            game.Mino.Delete();
            game.Mino.Release();
            _holdMinoBindFactory.CreateHoldMinoBind(game.HoldMino);
        }
    }
}
