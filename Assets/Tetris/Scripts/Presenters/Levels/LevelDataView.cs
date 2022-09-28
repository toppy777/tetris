using UnityEngine;
using TMPro;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelDataView : MonoBehaviour, ILevelDataView
    {
        public void SetText(int level)
        {
            GetComponent<TextMeshProUGUI>().text = level.ToString();
        }
    }
}
