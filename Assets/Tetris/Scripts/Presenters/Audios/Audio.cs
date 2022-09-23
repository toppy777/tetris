using UnityEngine;
using Tetris.Scripts.Domains.Audios;

namespace Tetris.Scripts.Presenters.Audios
{
    [RequireComponent(typeof (AudioSource))]
    public class Audio : MonoBehaviour, IAudio
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(AudioClip clip)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}
