using System;
using UniRx;
using UnityEngine;
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
            Disposable = Observable.EveryUpdate()
                // .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    game.MinoMoveSpeed.AddElapsedTime(Time.deltaTime);
                    if (!game.MinoMoveSpeed.IsElapsed()) {
                        return;
                    }

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
