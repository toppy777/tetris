using UnityEngine;
using Tetris.Scripts.Presenters.FinishCanvas;
using Tetris.Scripts.Presenters.MinoPieces;

namespace Tetris.Scripts.Presenters.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Prefabs", menuName = "Tetris/Prefabs", order = 0)]
    public class Prefabs : ScriptableObject
    {
        [SerializeField] private MinoPieceView _minoPieceViewPrefab;
        [SerializeField] private FinishCanvasView _finishCanvasViewPrefab;

        public MinoPieceView MinoPieceViewPrefab => _minoPieceViewPrefab;
        public FinishCanvasView FinishCanvasViewPrefab => _finishCanvasViewPrefab;
    }
}
