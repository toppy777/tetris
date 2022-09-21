using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Tetris.Scripts.Domains.Titles;

namespace Tetris.Scripts.Presenters.Titles
{
    public class PracticeButton : MonoBehaviour, IPracticeButton
    {
        public void SetAction(UnityAction action)
        {
            GetComponent<Button>().onClick.AddListener(action);
        }
    }
}

