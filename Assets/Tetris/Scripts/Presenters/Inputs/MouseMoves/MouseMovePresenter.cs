using System;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.HorizontalPositions;
using Tetris.Scripts.Domains.GameStatuses;
using Tetris.Scripts.Application.Minos;

namespace Tetris.Scripts.Presenters.Inputs
{
    public class MouseMovePresenter
    {
        public IDisposable Disposable;

        public MouseMovePresenter(Game game, MinoMoveHorizontalUseCase minoMoveHorizontalUseCase)
        {
            Disposable = Observable.EveryUpdate()
                .Where(_ => game.GameStatus.Value == GameStatusType.Play)
                .Where(_ => game.Mino.Exists())
                .Where(_ => game.HorizontalPosition.Value != HorizontalPosition.GetHorizontalPos())
                .Subscribe(_ => {
                    // ■ MinoShadowを移動(Presenterのどこかのクラスに移動)
                    minoMoveHorizontalUseCase.TryExecute();
                });
        }
    }
}
