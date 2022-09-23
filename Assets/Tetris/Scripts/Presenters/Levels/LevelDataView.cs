using UnityEngine;
using TMPro;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelDataView : MonoBehaviour
    {
        public void SetText(int level)
        {
            GetComponent<TextMeshProUGUI>().text = level.ToString();
        }
    }
}
