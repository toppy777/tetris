using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tetris.Scripts.Application.Games;
using Tetris.Scripts.Presenters.FinishCanvas;
using Tetris.Scripts.Presenters.Minos;
using Tetris.Scripts.Presenters.NextMinos;
using Tetris.Scripts.Presenters.HoldMinos;
using Tetris.Scripts.Presenters.ScriptableObjects;

namespace Tetris.Scripts.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Prefabs _prefabs;
        [SerializeField] private MinoPieceView _minoPieceViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_prefabs);
            builder.RegisterInstance(_minoPieceViewPrefab);

            builder.Register<CreateGameUseCase>(Lifetime.Singleton);

            builder.Register<MinoBindFactory>(Lifetime.Singleton).As<IMinoBindFactory>();
            builder.Register<NextMinoBindFactory>(Lifetime.Singleton).As<INextMinoBindFactory>();
            builder.Register<HoldMinoBindFactory>(Lifetime.Singleton).As<IHoldMinoBindFactory>();
            builder.Register<FinishCanvasViewFactory>(Lifetime.Singleton).As<IFinishCanvasViewFactory>();

            builder.RegisterEntryPoint<GameInitializer>();
        }
    }
}