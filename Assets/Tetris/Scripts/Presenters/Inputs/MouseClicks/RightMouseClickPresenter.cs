using UnityEngine;
using UniRx;
using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.GameStatuses;
using Tetris.Scripts.Application.Minos;
using Tetris.Scripts.Application.HoldMinos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class RightMouseClickPresenter
    {
        public IDisposable Disposable;

        public RightMouseClickPresenter(
            Game game,
            SwapHoldMinoUseCase swapHoldMinoUseCase,
            SetHoldMinoUseCase setHoldMinoUseCase,
            CreateNextMinoUseCase createNextMinoUseCase
        ) {
            Disposable = Observable.EveryUpdate()
                .Where(_ => game.GameStatus.Value == GameStatusType.Play)
                .Where(_ => Input.GetMouseButtonDown(1))
                .Where(_ => game.Mino.Exists())
                .Where(_ => game.HoldMino.IsAvailable)
                .Subscribe(_ => {
                    if (game.HoldMino.Exists) {
                        // ■ HoldMinoに登録
                        swapHoldMinoUseCase.Execute();
                    } else {
                        // ■ HoldMinoに登録(初回)
                        setHoldMinoUseCase.Execute();
                        createNextMinoUseCase.Execute();
                    }
                });
        }
    }
}
