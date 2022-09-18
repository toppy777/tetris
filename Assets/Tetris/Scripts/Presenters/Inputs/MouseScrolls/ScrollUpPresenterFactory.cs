using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class ScrollUpPresenterFactory : IScrollUpPresenterFactory
    {
        BoardService _boardService;
        MinoRotateLeftUseCase _minoRotateLeftUseCase;

        public ScrollUpPresenterFactory(
            BoardService boardService,
            MinoRotateLeftUseCase minoRotateLeftUseCase
        )
        {
            _boardService = boardService;
            _minoRotateLeftUseCase = minoRotateLeftUseCase;
        }

        public IDisposable Create(Game game)
        {
            var scrollPresenter = new ScrollUpPresenter(game, _boardService, _minoRotateLeftUseCase);
            return scrollPresenter.Disposable;
        }
    }
}
