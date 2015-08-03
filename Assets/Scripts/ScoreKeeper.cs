using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public static int Score {
        get {
            return getScore();
        }
    }

    private static int numShipsDestroyed;
    private Text scoreText;

    private ScoreKeeper instance;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

	void Start () {
        scoreText = GameObject.Find("ScoreText").
                        GetComponent<Text>();
        DisplayScore();
	}

    private static int getScore() {
        return 10 * numShipsDestroyed;
    }

    public void Reset() {
        numShipsDestroyed = 0;
    }

    public void EnemyDestroyed() {
        numShipsDestroyed++;
        DisplayScore();
    }

    private void DisplayScore() {
        scoreText.text = Score.ToString();
    }
}
