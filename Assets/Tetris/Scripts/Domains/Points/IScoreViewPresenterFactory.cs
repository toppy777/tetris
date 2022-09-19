using System;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Domains.Points
{
    public interface IScoreViewPresenterFactory
    {
        IDisposable Create(Game game);
    }
}
