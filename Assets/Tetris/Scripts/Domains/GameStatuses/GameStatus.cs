using UnityEngine;

namespace Tetris.Scripts.Domains.GameStatuses
{
    public class GameStatus
    {
        GameStatusType _value;
        public GameStatusType Value => _value;

        public GameStatus()
        {
            _value = GameStatusType.Play;
        }

        public void Play()
        {
            _value = GameStatusType.Play;
        }

        public void GameOver()
        {
            _value = GameStatusType.GameOver;
        }
    }
}
