using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.HoldMinos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class RightMouseClickPresenterFactory : IRightMouseClickPresenterFactory
    {
        SwapHoldMinoUseCase _swapHoldMinoUseCase;
        SetHoldMinoUseCase _setHoldMinoUseCase;

        public RightMouseClickPresenterFactory(
            SwapHoldMinoUseCase swapHoldMinoUseCase,
            SetHoldMinoUseCase setHoldMinoUseCase
        )
        {
            _swapHoldMinoUseCase = swapHoldMinoUseCase;
            _setHoldMinoUseCase = setHoldMinoUseCase;
        }
        
        public IDisposable Create(Game game)
        {
            var input = new RightMouseClickPresenter(game, _swapHoldMinoUseCase, _setHoldMinoUseCase);
            return input.Disposable;
        }
    }
}
