using System;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Domains.Levels
{
    public interface ILevelPresenterFactory
    {
        IDisposable Create(Game game);
    }
}
