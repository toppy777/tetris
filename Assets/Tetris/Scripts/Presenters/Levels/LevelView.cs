using UnityEngine;
using Tetris.Scripts.Domains.Levels;

namespace Tetris.Scripts.Presenters.Levels
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        public ILevelDataView GetScoreDataView()
        {
            return transform.GetChild(1).gameObject.GetComponent<LevelDataView>() as ILevelDataView;
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
