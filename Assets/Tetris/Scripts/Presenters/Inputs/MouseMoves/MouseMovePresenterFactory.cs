using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class MouseMovePresenterFactory : IMouseMovePresenterFactory
    {
        MinoMoveHorizontalUseCase _minoMoveHorizontalUseCase;

        public MouseMovePresenterFactory(
            MinoMoveHorizontalUseCase minoMoveHorizontalUseCase
        ) {
            _minoMoveHorizontalUseCase = minoMoveHorizontalUseCase;
        }

        public IDisposable Create(Game game)
        {
            MouseMovePresenter cursorMovePresenter = new MouseMovePresenter(game, _minoMoveHorizontalUseCase);
            return cursorMovePresenter.Disposable;
        }
    }
}
