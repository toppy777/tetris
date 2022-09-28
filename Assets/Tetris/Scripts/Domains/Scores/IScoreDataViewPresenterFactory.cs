using System;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Domains.Scores
{
    public interface IScoreDataViewPresenterFactory
    {
        IDisposable Create(Game game, IScoreDataView scoreDataView);
    }
}
