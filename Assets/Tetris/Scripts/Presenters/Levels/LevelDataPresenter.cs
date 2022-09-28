using System;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelDataPresenter
    {
        public IDisposable Disposable;
        
        public LevelDataPresenter(Game game, ILevelDataView levelView)
        {
            Disposable = game.Level.WhenLevelSet.Subscribe(level => {
                levelView.SetText(level);
            });
        }
    }
}
