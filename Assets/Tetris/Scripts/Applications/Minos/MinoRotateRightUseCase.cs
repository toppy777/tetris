using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.MinoShadows;

namespace Tetris.Scripts.Application.Minos
{
    public class MinoRotateRightUseCase
    {
        GameRegistry _gameRegistry;
        MinoShadowService _minoShadowService;

        public MinoRotateRightUseCase(
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
            game.Mino.RotateRight();
            game.MinoShadow.Set(_minoShadowService.GetMinoShadowPositions(game.Board, game.Mino));
        }
    }
}
