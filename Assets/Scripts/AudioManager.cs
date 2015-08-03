using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public AudioClip PlayerDeathClip, EnemyDeathClip;
    
    public void Play(AudioClip clip, Vector3 position) {
        AudioSource.PlayClipAtPoint(clip, position, 1f);
    }

    public void Play(AudioClip clip, Vector3 position, float volume) {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

}
