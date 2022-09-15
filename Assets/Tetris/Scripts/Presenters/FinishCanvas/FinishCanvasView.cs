using UnityEngine;
using Tetris.Scripts.Application.Games;

namespace Tetris.Scripts.Presenters.FinishCanvas
{
    public class FinishCanvasView : MonoBehaviour, IFinishCanvasView
    {
        public void Display()
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }

        public void UnDisplay()
        {
            gameObject.GetComponent<Canvas>().enabled = false;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
