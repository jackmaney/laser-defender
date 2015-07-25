using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private static Sprite[] sprites;
    private EnemySpriteLoader spriteLoader;

    public EnemyColor EColor;

    public int index = -1 ; // 0 to 4 to pick a particular ship


	void Start () {
        spriteLoader = FindObjectOfType<EnemySpriteLoader>();

        SetSprite();
    }


    public void SetSprite() {
        
        if(index >= 0 && index <= 4) {
            GetComponent<SpriteRenderer>().sprite = 
                spriteLoader.GetSprite(EColor, index);
        }
        else {
            GetComponent<SpriteRenderer>().sprite = 
                spriteLoader.Random(EColor);
        }

        if(GetComponent<PolygonCollider2D>() != null) {
            Destroy(GetComponent<PolygonCollider2D>());
        }

        PolygonCollider2D poly = 
            gameObject.AddComponent<PolygonCollider2D>();

        poly.isTrigger = true;

    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
