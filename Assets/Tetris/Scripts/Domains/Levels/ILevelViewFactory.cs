using UnityEngine;

namespace Tetris.Scripts.Domains.Levels
{
    public interface ILevelViewFactory
    {
        ILevelView Create();
    }
}
