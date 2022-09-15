using UnityEngine;
using Tetris.Scripts.Application.Games;
using Tetris.Scripts.Presenters.ScriptableObjects;

namespace Tetris.Scripts.Presenters.FinishCanvas
{
    public class FinishCanvasViewFactory : IFinishCanvasViewFactory
    {
        FinishCanvasView _finishCanvasViewPrefab;

        public FinishCanvasViewFactory(
            Prefabs prefabs
        ) {
            _finishCanvasViewPrefab = prefabs.FinishCanvasViewPrefab;
        }

        public IFinishCanvasView CreateFinishCanvasView()
        {
            return GameObject.Instantiate(_finishCanvasViewPrefab);
        }
    }
}
