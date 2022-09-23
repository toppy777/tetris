using UnityEngine;

namespace Tetris.Scripts.Infrastructures.Sounds
{
    public class SoundRepository
    {
        public static AudioClip GetSoundPlaceMino()
        {
            return Resources.Load<AudioClip>("Sounds/PlaceMino_short");
        }
    }
}
