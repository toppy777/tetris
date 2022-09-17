using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Application.Minos
{
    public class PlaceMinoUseCase
    {
        GameRegistry _gameRegistry;

        public PlaceMinoUseCase(
            GameRegistry gameRegistry
        )
        {
            _gameRegistry = gameRegistry;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.Board.Add(game.Mino);
            game.Mino.Release();
            game.HorizontalPosition.Reset();
        }
    }
}
