using UnityEngine;
using Tetris.Scripts.Domains.Scores;

namespace Tetris.Scripts.Presenters.Scores
{
    public class ScoreView : MonoBehaviour, IScoreView
    {
        public IScoreDataView GetScoreDataView()
        {
            return transform.GetChild(1).gameObject.GetComponent<ScoreDataView>() as IScoreDataView;
        }

        public void Destroy()
        {
            foreach(Transform child in gameObject.transform){
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
