using System;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Domains.Levels
{
    public interface ILevelDataPresenterFactory
    {
        IDisposable Create(Game game, ILevelDataView levelDataView);
    }
}
