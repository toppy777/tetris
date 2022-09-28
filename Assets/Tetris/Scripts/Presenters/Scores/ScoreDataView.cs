using UnityEngine;
using TMPro;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreDataView : MonoBehaviour, IScoreDataView
    {
        public void SetText(int score)
        {
            GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
    }
}
