using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.MinoTypes;
using Tetris.Scripts.Domains.MinoReserves;

namespace Tetris.Scripts.Application.Minos
{

    public class CreateMinoUseCase
    {
        GameRegistry _gameRegistry;
        MinoFactory _minoFactory;
        IMinoBindFactory _minoBindFactory;
        INextMinoBindFactory _nextMinoBindFactory;

        public CreateMinoUseCase(
            GameRegistry gameRegistry,
            MinoFactory minoFactory,
            IMinoBindFactory minoBindFactory,
            INextMinoBindFactory nextMinoBindFactory
        ) {
            _gameRegistry = gameRegistry;
            _minoFactory = minoFactory;
            _minoBindFactory = minoBindFactory;
            _nextMinoBindFactory = nextMinoBindFactory;
        }

        public void Execute(MinoType minoType)
        {
            Game game = _gameRegistry.CurrentGame;
            game.Mino = _minoFactory.CreateMino(minoType);
            game.MinoBind = _minoBindFactory.CreateMinoBind(game.Mino);
            game.NextMinoBind?.Dispose();
            game.NextMinoBind = _nextMinoBindFactory.CreateNextMinoBind(game.MinoReserveList);
        }
    }
}