using VContainer;
using VContainer.Unity;
using UnityEngine;
using Tetris.Scripts.Domains.Audios;
using Tetris.Scripts.Presenters.Audios;

namespace Tetris.Scripts.LifetimeScopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] Audio _audioPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentOnNewGameObject<Audio>(Lifetime.Singleton).As<IAudio>();
        }
    }
}
