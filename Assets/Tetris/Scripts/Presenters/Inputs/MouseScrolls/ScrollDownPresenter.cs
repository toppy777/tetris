using UnityEngine;
using System;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.GameStatuses;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class ScrollDownPresenter
    {
        public IDisposable Disposable;

        public ScrollDownPresenter(Game game, BoardService boardService, MinoRotateRightUseCase minoRotateRightUseCase)
        {
            Disposable = Observable.EveryUpdate()
                .Where(_ => game.GameStatus.Value == GameStatusType.Play)
                .Where(_ => Input.GetAxis("Mouse ScrollWheel") < 0)
                .Where(_ => game.Mino.Exists())
                .Where(_ => boardService.HasSpaceForMino(game.Board, game.Mino, game.Mino.GetNextShape()))
                .Subscribe(_ => {
                    // ■ 回転
                    minoRotateRightUseCase.Execute();
                });
        }
    }
}
