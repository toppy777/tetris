using UnityEngine;

namespace Tetris.Scripts.Infrastructures.BetweenScenes
{
    [CreateAssetMenu(fileName = "ModeObject", menuName = "Mode Object/Mode Object", order = 1)]
    public class ModeObject : ScriptableObject
    {
        [SerializeField] ModeType ModeType;

        public void SetMode(ModeType modeType)
        {
            ModeType = modeType;
        }

        public ModeType GetMode()
        {
            return ModeType;
        }
    }
}
