using UnityEngine;
using Tetris.Scripts.Presenters.FinishCanvas;
using Tetris.Scripts.Presenters.MinoPieces;
using Tetris.Scripts.Presenters.Levels;
using Tetris.Scripts.Presenters.Scores;

namespace Tetris.Scripts.Presenters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Prefabs", menuName = "Tetris/Prefabs", order = 0)]
    public class Prefabs : ScriptableObject
    {
        [SerializeField] private MinoPieceView _minoPieceViewPrefab;
        [SerializeField] private FinishCanvasView _finishCanvasViewPrefab;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private ScoreView _scoreView;

        public MinoPieceView MinoPieceViewPrefab => _minoPieceViewPrefab;
        public FinishCanvasView FinishCanvasViewPrefab => _finishCanvasViewPrefab;
        public LevelView LevelView => _levelView;
        public ScoreView ScoreView => _scoreView;
    }
}
