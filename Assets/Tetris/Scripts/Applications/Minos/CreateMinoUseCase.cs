using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.MinoTypes;

namespace Tetris.Scripts.Application.Minos
{

    public class CreateMinoUseCase
    {
        GameRegistry _gameRegistry;
        MinoFactory _minoFactory;
        IMinoBindFactory _minoBindFactory;

        public CreateMinoUseCase(
            GameRegistry gameRegistry,
            MinoFactory minoFactory,
            IMinoBindFactory minoBindFactory
        ) {
            _gameRegistry = gameRegistry;
            _minoFactory = minoFactory;
            _minoBindFactory = minoBindFactory;
        }

        public void Execute(MinoType minoType)
        {
            Game game = _gameRegistry.CurrentGame;
            game.Mino = _minoFactory.CreateMino(minoType);
            game.MinoBind = _minoBindFactory.CreateMinoBind(game.Mino, game.Disposable);
        }
    }
}
