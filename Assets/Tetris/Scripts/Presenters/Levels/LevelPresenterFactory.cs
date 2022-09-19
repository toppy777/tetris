using UnityEngine;
using System;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelPresenterFactory : ILevelPresenterFactory
    {
        LevelView _levelView;

        public LevelPresenterFactory(LevelView levelView)
        {
            _levelView = levelView;
        }

        public IDisposable Create(Game game)
        {
            var presenter = new LevelPresenter(game, _levelView);
            return presenter.Disposable;
        }
    }
}
