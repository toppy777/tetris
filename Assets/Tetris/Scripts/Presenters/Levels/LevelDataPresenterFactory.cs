using System;
using UnityEngine;
using Tetris.Scripts.Domains.Games;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelDataPresenterFactory : ILevelDataPresenterFactory
    {
        public IDisposable Create(Game game)
        {
            LevelDataView levelDataView = GameObject.Find("LevelData").GetComponent<LevelDataView>();
            var presenter = new LevelDataPresenter(game, levelDataView);
            return presenter.Disposable;
        }
    }
}
