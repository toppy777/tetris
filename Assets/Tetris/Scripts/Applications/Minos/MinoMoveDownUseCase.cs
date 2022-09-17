using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Application.Minos
{
    public class MinoMoveDownUseCase
    {
        GameRegistry _gameRegistry;
        public MinoMoveDownUseCase(
            GameRegistry gameRegistry
        )
        {
            _gameRegistry = gameRegistry;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.Mino.MoveTo(game.Mino.Position.X, game.Mino.Position.Y-1);
        }
    }
}
