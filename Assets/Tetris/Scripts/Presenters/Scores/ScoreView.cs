using UnityEngine;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreView : MonoBehaviour, IScoreView
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
