using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class IntervalPresenterFactory : IIntervalPresenterFactory
    {
        BoardService _boardService;
        PlaceMinoUseCase _placeMinoUseCase;
        MinoMoveDownUseCase _minoMoveDownUseCase;
        CreateNextMinoUseCase _createNextMinoUseCase;

        public IntervalPresenterFactory(
            BoardService boardService,
            PlaceMinoUseCase placeMinoUseCase,
            MinoMoveDownUseCase minoMoveDownUseCase,
            CreateNextMinoUseCase createNextMinoUseCase
        )
        {
            _boardService = boardService;
            _placeMinoUseCase = placeMinoUseCase;
            _minoMoveDownUseCase = minoMoveDownUseCase;
            _createNextMinoUseCase = createNextMinoUseCase;
        }

        public IDisposable Create(Game game)
        {
            var presenter = new IntervalPresenter(game, _boardService, _placeMinoUseCase, _minoMoveDownUseCase, _createNextMinoUseCase);
            return presenter.Disposable;
        }
    }
}
