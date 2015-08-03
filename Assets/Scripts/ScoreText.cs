using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	void Start () {
        GetComponent<Text>().text = ScoreKeeper.Score.ToString();
	}
	
}
