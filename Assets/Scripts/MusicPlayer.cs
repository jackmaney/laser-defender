using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
    private AudioSource _audio;

    public AudioClip DefaultMusic, GameOverMusic;
	
	void Awake(){
		if(instance != null && instance != this) {
			Destroy(gameObject);
			print("Duplicate music player self-destructing!");
		} else {
			instance = this;
            DontDestroyOnLoad(gameObject);
		}

        _audio = GetComponent<AudioSource>();
        _audio.loop = true;
		
	}

    public void Play(AudioClip clip) {
        if (_audio.isPlaying) {
            _audio.Stop();
        }
        _audio.clip = clip;
        _audio.Play();
    }

    public AudioClip CurrentClip() {
        return _audio.clip;
    }
}
