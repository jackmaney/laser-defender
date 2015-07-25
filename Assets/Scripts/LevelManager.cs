using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string levelName){
		Debug.Log("New Level load: " + levelName);
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
