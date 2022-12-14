using UnityEngine;
using UniRx;
using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.GameStatuses;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class LeftMouseClickPresenter
    {
        public IDisposable Disposable;

        public LeftMouseClickPresenter(Game game, FastPlaceMinoUseCase fastPlaceMinoUseCase)
        {
            Disposable = Observable.EveryUpdate()
                .Where(_ => game.GameStatus.Value == GameStatusType.Play)
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => game.Mino.Exists())
                .Subscribe(_ => {
                    fastPlaceMinoUseCase.Execute();
                });
        }
    }
}
