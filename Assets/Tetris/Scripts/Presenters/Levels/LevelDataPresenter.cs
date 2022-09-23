using System;
using UniRx;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelDataPresenter
    {
        public IDisposable Disposable;
        
        public LevelDataPresenter(Game game, LevelDataView levelView)
        {
            Disposable = game.Level.WhenLevelSet.Subscribe(level => {
                levelView.SetText(level);
            });
        }
    }
}
