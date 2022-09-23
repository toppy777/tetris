using System;
using UnityEngine;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreDataViewPresenterFactory : IScoreDataViewPresenterFactory
    {
        public IDisposable Create(Game game)
        {
            ScoreDataView scoreDataView = GameObject.Find("ScoreData").GetComponent<ScoreDataView>();
            var presenter = new ScoreDataViewPresenter(game, scoreDataView);
            return presenter.Disposable;
        }
    }
}
