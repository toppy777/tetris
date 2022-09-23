using UnityEngine;
using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelViewFactory : ILevelViewFactory
    {
        LevelView _levelView;

        public LevelViewFactory(
            Prefabs prefabs
        )
        {
            _levelView = prefabs.LevelView;
        }

        public ILevelView Create()
        {
            LevelView obj = GameObject.Instantiate(_levelView, GameObject.Find("Canvas").transform);
            return obj as ILevelView;
        }
    }
}
