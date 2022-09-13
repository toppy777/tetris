using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tetris.Scripts.Application.Games;

namespace Tetris.Scripts.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private MinoPieceView _minoPieceViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_minoPieceViewPrefab);

            builder.Register<CreateGameUseCase>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameInitializer>();
        }
    }
}