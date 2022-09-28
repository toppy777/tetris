using System;
using UniRx;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreDataViewPresenter
    {
        public IDisposable Disposable;
        
        public ScoreDataViewPresenter(Game game, IScoreDataView scoreView)
        {
            Disposable = game.Score.WhenScoreAdd.Subscribe(score => {
                scoreView.SetText(score);
            });
        }
    }
}
