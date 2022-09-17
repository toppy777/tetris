using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;

namespace Tetris.Scripts.Application.Minos
{
    public class FastPlaceMinoUseCase
    {
        GameRegistry _gameRegistry;
        MinoShadowService _minoShadowService;

        public FastPlaceMinoUseCase(
            GameRegistry gameRegistry,
            MinoShadowService minoShadowService
        )
        {
            _gameRegistry = gameRegistry;
            _minoShadowService = minoShadowService;
        }

        public void Execute()
        {
            Game game = _gameRegistry.CurrentGame;
            game.Mino.MoveTo(_minoShadowService.GetMinoShadowPositions(game.Board, game.Mino));
            game.Board.Add(game.Mino);
            game.Mino.Release();
            game.HorizontalPosition.Reset();
        }
    }
}
