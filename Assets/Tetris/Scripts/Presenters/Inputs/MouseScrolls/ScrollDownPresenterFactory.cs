using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class ScrollDownPresenterFactory : IScrollDownPresenterFactory
    {
        BoardService _boardService;
        MinoRotateRightUseCase _minoRotateRightUseCase;

        public ScrollDownPresenterFactory(
            BoardService boardService,
            MinoRotateRightUseCase minoRotateLeftUseCase
        )
        {
            _boardService = boardService;
            _minoRotateRightUseCase = minoRotateLeftUseCase;
        }

        public IDisposable Create(Game game)
        {
            var scrollPresenter = new ScrollDownPresenter(game, _boardService, _minoRotateRightUseCase);
            return scrollPresenter.Disposable;
        }
    }
}
