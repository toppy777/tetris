using UnityEngine;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        public void Display()
        {
            gameObject.SetActive(true);
        }

        public void UnDisplay()
        {
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
