using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class LeftMouseClickPresenterFactory : ILeftMouseClickPresenterFactory
    {
        FastPlaceMinoUseCase _fastPlaceMinoUseCase;
        CreateNextMinoUseCase _createNextMinoUseCase;

        public LeftMouseClickPresenterFactory(
            FastPlaceMinoUseCase fastPlaceMinoUseCase,
            CreateNextMinoUseCase createNextMinoUseCase
        )
        {
            _fastPlaceMinoUseCase = fastPlaceMinoUseCase;
            _createNextMinoUseCase = createNextMinoUseCase;
        }

        public IDisposable Create(Game game)
        {
            var input = new LeftMouseClickPresenter(game, _fastPlaceMinoUseCase/*, _createNextMinoUseCase*/);
            return input.Disposable;
        }
    }
}
