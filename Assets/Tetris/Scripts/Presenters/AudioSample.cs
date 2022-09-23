using UnityEngine;

public class AudioSample : MonoBehaviour
{
    AudioSource _audioSource;
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
