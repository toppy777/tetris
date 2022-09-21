using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Tetris.Scripts.Domains.Titles;

namespace Tetris.Scripts.Presenters.Titles
{
    public class PlayButton : MonoBehaviour, IPlayButton
    {
        public void SetAction(UnityAction action)
        {
            GetComponent<Button>().onClick.AddListener(action);
        }
    }
}

