using UnityEngine;

namespace Tetris.Scripts.Infrastructures.BetweenScenes
{
    public class ModeRepository
    {
        ModeObject _modeObject;

        public ModeRepository()
        {
            _modeObject = Resources.Load<ModeObject>("Prefabs/ModeObject");
        }

        public void SetMode(ModeType modeType)
        {
            _modeObject.SetMode(modeType);
        }

        public ModeType GetMode()
        {
            return _modeObject.GetMode();
        }
    }
}
