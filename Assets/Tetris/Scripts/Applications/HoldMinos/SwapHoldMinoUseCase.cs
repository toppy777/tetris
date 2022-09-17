using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Application.HoldMinos
{
    public class SwapHoldMinoUseCase
    {
        GameRegistry _gameRegistry;
        CreateMinoUseCase _createMinoUseCase;
        IHoldMinoBindFactory _holdMinoBindFactory;

        public SwapHoldMinoUseCase(
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
            MinoType minoType = game.Mino.MinoType;
            game.Mino.Delete();
            game.Mino.Release();
            _createMinoUseCase.Execute(game.HoldMino.GetMinoType());
            game.HoldMino.Set(minoType);
            _holdMinoBindFactory.CreateHoldMinoBind(game.HoldMino);
        }
    }
}
