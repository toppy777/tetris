using UnityEngine;
using VContainer;
using VContainer.Unity;

using Tetris.Scripts.Application.Games;
using Tetris.Scripts.Application.Minos;
using Tetris.Scripts.Application.HoldMinos;

using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Domains.PlacePredictions;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.Boards;
using Tetris.Scripts.Domains.MinoReserves;
using Tetris.Scripts.Domains.Others;
using Tetris.Scripts.Domains.Levels;
using Tetris.Scripts.Domains.Scores;

using Tetris.Scripts.Presenters.FinishCanvas;
using Tetris.Scripts.Presenters.Minos;
using Tetris.Scripts.Presenters.NextMinos;
using Tetris.Scripts.Presenters.HoldMinos;
using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Presenters.MinoPieces;
using Tetris.Scripts.Presenters.MinoShadows;
using Tetris.Scripts.Presenters.Levels;
using Tetris.Scripts.Presenters.Scores;

using Tetris.Scripts.Domains.Inputs;
using Tetris.Scripts.Presenters.Inputs;

using Tetris.Scripts.Infrastructures.BetweenScenes;

namespace Tetris.Scripts.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Prefabs _prefabs;

        protected override void Configure(IContainerBuilder builder)
        {
            // Prefabs
            builder.RegisterInstance(_prefabs);

            // Infrastructures
            builder.Register<ModeRepository>(Lifetime.Singleton);

            // View Factory
            builder.Register<LevelViewFactory>(Lifetime.Singleton).As<ILevelViewFactory>();
            builder.Register<ScoreViewFactory>(Lifetime.Singleton).As<IScoreViewFactory>();

            // View Binds
            builder.Register<MinoBindFactory>(Lifetime.Singleton).As<IMinoBindFactory>();
            builder.Register<NextMinoBindFactory>(Lifetime.Singleton).As<INextMinoBindFactory>();
            builder.Register<HoldMinoBindFactory>(Lifetime.Singleton).As<IHoldMinoBindFactory>();
            builder.Register<MinoShadowBindFactory>(Lifetime.Singleton).As<IMinoShadowBindFactory>();
            builder.Register<FinishCanvasViewFactory>(Lifetime.Singleton).As<IFinishCanvasViewFactory>();

            // Domains
            builder.Register<GameRegistry>(Lifetime.Singleton);
            builder.Register<MinoFactory>(Lifetime.Singleton);

            // Domain Services
            builder.Register<BoardService>(Lifetime.Singleton);
            builder.Register<PlacePrediction>(Lifetime.Singleton);

            // Use Cases
            builder.Register<CreateGameUseCase>(Lifetime.Singleton);
            builder.Register<MinoMoveDownUseCase>(Lifetime.Singleton);
            builder.Register<MinoMoveHorizontalUseCase>(Lifetime.Singleton);
            builder.Register<MinoRotateRightUseCase>(Lifetime.Singleton);
            builder.Register<MinoRotateLeftUseCase>(Lifetime.Singleton);
            builder.Register<CreateMinoUseCase>(Lifetime.Singleton);
            builder.Register<CreateNextMinoUseCase>(Lifetime.Singleton);
            builder.Register<FastPlaceMinoUseCase>(Lifetime.Singleton);
            builder.Register<PlaceMinoUseCase>(Lifetime.Singleton);
            builder.Register<SetHoldMinoUseCase>(Lifetime.Singleton);
            builder.Register<SwapHoldMinoUseCase>(Lifetime.Singleton);

            // Input Presenters
            builder.Register<LeftMouseClickPresenterFactory>(Lifetime.Singleton).As<ILeftMouseClickPresenterFactory>();
            builder.Register<RightMouseClickPresenterFactory>(Lifetime.Singleton).As<IRightMouseClickPresenterFactory>();
            builder.Register<ScrollUpPresenterFactory>(Lifetime.Singleton).As<IScrollUpPresenterFactory>();
            builder.Register<ScrollDownPresenterFactory>(Lifetime.Singleton).As<IScrollDownPresenterFactory>();
            builder.Register<MouseMovePresenterFactory>(Lifetime.Singleton).As<IMouseMovePresenterFactory>();
            builder.Register<IntervalPresenterFactory>(Lifetime.Singleton).As<IIntervalPresenterFactory>();

            // ViewPresenters
            builder.Register<LevelDataPresenterFactory>(Lifetime.Singleton).As<ILevelDataPresenterFactory>();
            builder.Register<ScoreDataViewPresenterFactory>(Lifetime.Singleton).As<IScoreDataViewPresenterFactory>();

            // Entry Point
            builder.RegisterEntryPoint<GameInitializer>();
        }
    }
}