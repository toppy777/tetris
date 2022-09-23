using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Tetris.Scripts.Domains.Others;

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

        public void SetRestartButtonClick(UnityAction action)
        {
            GameObject restartButton = transform.Find("RestartButton").gameObject;
            restartButton.GetComponent<Button>().onClick.AddListener(action);
        }

        public void SetBackToTitleButton(UnityAction action)
        {
            GameObject backToTitleButton = transform.Find("BackToTitleButton").gameObject;
            backToTitleButton.GetComponent<Button>().onClick.AddListener(action);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
