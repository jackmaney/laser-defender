using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelManager : MonoBehaviour {

    private MusicPlayer _music;

    void Start() {
        _music = FindObjectOfType<MusicPlayer>();
    }

	public void LoadLevel(string levelName){
		Debug.Log("New Level load: " + levelName);
        if(_music != null &&
                _music.CurrentClip() != _music.DefaultMusic) {
            _music.Play(_music.DefaultMusic);
        }

		Application.LoadLevel(levelName);
	}

	public void QuitRequest(){
		Debug.Log("Quit requested");
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
		Application.Quit();
        #endif
	}

}
