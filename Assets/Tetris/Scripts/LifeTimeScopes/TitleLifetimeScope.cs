using VContainer;
using VContainer.Unity;
using Tetris.Scripts.Domains.Titles;
using Tetris.Scripts.Domains.Audios;
using Tetris.Scripts.Application.Titles;
using Tetris.Scripts.Presenters.Titles;
using Tetris.Scripts.Presenters.Audios;

namespace Tetris.Scripts.LifetimeScopes
{
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Buttons
            builder.RegisterComponentInHierarchy<PlayButton>().As<IPlayButton>();
            builder.RegisterComponentInHierarchy<PracticeButton>().As<IPracticeButton>();

            // Entry Point
            builder.RegisterEntryPoint<TitleInitializer>();
        }
    }
}
