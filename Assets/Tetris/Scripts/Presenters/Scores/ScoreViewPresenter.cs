using System;
using UniRx;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreViewPresenter
    {
        public IDisposable Disposable;
        
        public ScoreViewPresenter(Game game, ScoreView scoreView)
        {
            Disposable = game.Point.WhenPointAdd.Subscribe(score => {
                scoreView.SetText(score);
            });
        }
    }
}
