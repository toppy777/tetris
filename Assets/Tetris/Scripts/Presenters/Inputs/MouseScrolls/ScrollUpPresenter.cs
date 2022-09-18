using UnityEngine;
using System;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class ScrollUpPresenter
    {
        public IDisposable Disposable;

        public ScrollUpPresenter(Game game, BoardService boardService, MinoRotateLeftUseCase minoRotateLeftUseCase)
        {
            Disposable = Observable.EveryUpdate()
                .Where(_ => Input.GetAxis("Mouse ScrollWheel") > 0)
                .Where(_ => game.Mino.Exists())
                .Where(_ => boardService.HasSpaceForMino(game.Board, game.Mino, game.Mino.GetPrevShape()))
                // .TakeUntil(gameOverObservable)
                .Subscribe(_ => {
                    // ■ 回転
                    minoRotateLeftUseCase.Execute();
                });
        }
    }
}
