using UnityEngine;
using TMPro;
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

        public void SetScore(int score)
        {
            var scoreDataView = transform.Find("ScoreView/ScoreDataView").gameObject;
            scoreDataView.GetComponent<TextMeshProUGUI>().text = score.ToString();
        }

        public void DisplayScore()
        {
            var scoreView = GetScoreView();
            scoreView.SetActive(true);
            var finishText = GetFinishText();
            finishText.SetActive(false);
        }

        public void DisplayFinishText()
        {
            var scoreView = GetScoreView();
            scoreView.SetActive(false);
            var finishText = GetFinishText();
            finishText.SetActive(true);
        }

        private GameObject GetScoreView()
        {
            return transform.Find("ScoreView").gameObject;
        }

        private GameObject GetFinishText()
        {
            return transform.Find("FinishText").gameObject;
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

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
