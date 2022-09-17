using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;

namespace Tetris.Scripts.Application.Minos
{
    public class MinoRotateLeftUseCase
    {
        GameRegistry _gameRegistry;
        MinoShadowService _minoShadowService;

        public MinoRotateLeftUseCase(
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
            game.Mino.RotateLeft();
            game.MinoShadow.Set(_minoShadowService.GetMinoShadowPositions(game.Board, game.Mino));
        }
    }
}
