using UnityEngine;
using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreViewFactory : IScoreViewFactory
    {
        ScoreView _scoreView;

        public ScoreViewFactory(
            Prefabs prefabs
        )
        {
            _scoreView = prefabs.ScoreView;
        }

        public IScoreView Create()
        {
            ScoreView obj = GameObject.Instantiate(_scoreView, GameObject.Find("Canvas").transform);
            return obj as IScoreView;
        }
    }
}
