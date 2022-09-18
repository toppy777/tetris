using System;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class IntervalPresenter
    {
        public IDisposable Disposable;

        public IntervalPresenter(
            Game game,
            BoardService boardService,
            PlaceMinoUseCase placeMinoUseCase,
            MinoMoveDownUseCase minoMoveDownUseCase,
            CreateNextMinoUseCase createNextMinoUseCase
        )
        {
            // Disposable = Observable.Interval(TimeSpan.FromSeconds(MoveDownTime.GetSeconds(game.Level.Value)))
            Disposable = Observable.Interval(TimeSpan.FromSeconds(0.5f))
                // .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 動けるか調べる
                    if (!boardService.CanMove(0, -1, game.Board, game.Mino)) {
                        // ■ 盤面に固定
                        placeMinoUseCase.Execute();
                        createNextMinoUseCase.Execute();
                        return;
                    }

                    // ■ 動く
                    minoMoveDownUseCase.Execute();
                });
        }
    }
}
