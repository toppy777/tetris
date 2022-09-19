using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Points;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreViewPresenterFactory : IScoreViewPresenterFactory
    {
        ScoreView _scoreView;

        public ScoreViewPresenterFactory(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        public IDisposable Create(Game game)
        {
            var presenter = new ScoreViewPresenter(game, _scoreView);
            return presenter.Disposable;
        }
    }
}
