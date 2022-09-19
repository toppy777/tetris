using UnityEngine;
using TMPro;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreView : MonoBehaviour
    {
        public void SetText(int level)
        {
            GetComponent<TextMeshProUGUI>().text = level.ToString();
        }
    }
}
