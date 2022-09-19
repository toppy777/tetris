using System;
using UniRx;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelPresenter
    {
        public IDisposable Disposable;
        
        public LevelPresenter(Game game, LevelView levelView)
        {
            Disposable = game.Level.WhenLevelSet.Subscribe(level => {
                levelView.SetText(level);
            });
        }
    }
}
