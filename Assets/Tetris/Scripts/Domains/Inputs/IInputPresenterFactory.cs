using System;
using Tetris.Scripts.Domains.Games;

namespace Tetris.Scripts.Domains.Inputs
{
    public interface IInputPresenterFactory
    {
        IDisposable Create(Game game);
    }
}
