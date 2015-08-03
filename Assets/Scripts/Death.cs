using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    private AudioSource audioSource;

    public AudioClip Clip;
    public float Volume;

    void Start() {
        
    }
	
    void OnDestroy() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Clip;
        audioSource.Play();
    }
}
