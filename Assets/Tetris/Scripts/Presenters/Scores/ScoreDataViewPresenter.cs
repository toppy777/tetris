using System;
using UniRx;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreDataViewPresenter
    {
        public IDisposable Disposable;
        
        public ScoreDataViewPresenter(Game game, ScoreDataView scoreView)
        {
            Disposable = game.Point.WhenScoreAdd.Subscribe(score => {
                scoreView.SetText(score);
            });
        }
    }
}
