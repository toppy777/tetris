using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;
using Tetris.Scripts.Application.HoldMinos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class RightMouseClickPresenterFactory : IRightMouseClickPresenterFactory
    {
        SwapHoldMinoUseCase _swapHoldMinoUseCase;
        SetHoldMinoUseCase _setHoldMinoUseCase;
        CreateNextMinoUseCase _createNextMinoUseCase;

        public RightMouseClickPresenterFactory(
            SwapHoldMinoUseCase swapHoldMinoUseCase,
            SetHoldMinoUseCase setHoldMinoUseCase,
            CreateNextMinoUseCase createNextMinoUseCase
        )
        {
            _swapHoldMinoUseCase = swapHoldMinoUseCase;
            _setHoldMinoUseCase = setHoldMinoUseCase;
            _createNextMinoUseCase = createNextMinoUseCase;
        }
        
        public IDisposable Create(Game game)
        {
            var input = new RightMouseClickPresenter(game, _swapHoldMinoUseCase, _setHoldMinoUseCase, _createNextMinoUseCase);
            return input.Disposable;
        }
    }
}
