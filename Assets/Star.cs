using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public static float MinSize = 0.1f;
    public static float MaxSize = 0.3f;
    private float size;
    
    public static float MinSpeed = 2f;
    public static float MaxSpeed = 20f;
    private float speed;

    private static Color yellowIsh = new Color(220f, 125f, 0f);
    private static Color blueIsh = new Color(150f, 169f, 245f);

    private Bounds bd;
    private float yMin;

    void Start () {

        speed = GetSpeed();
        size = GetSize();
        
	    transform.localScale = size * Vector3.one;
        
        GetComponent<Rigidbody>().velocity = speed * Vector3.down;
        
        SetColor();

        bd = GetComponent<SpriteRenderer>().bounds;

        yMin = Camera.main.GetComponent<CameraDimensions>().YMin;
	}
    
    void SetColor(){
        int value = (int)UnityEngine.Random.Range(1,4);
        if(value == 2){
            GetComponent<SpriteRenderer>().color = yellowIsh;
        }
        else if(value == 3){
            GetComponent<SpriteRenderer>().color = blueIsh;
        }
        
    }
    
    float GetSize(){
        return UnityEngine.Random.Range(MinSize, MaxSize);
    }
    
    float GetSpeed(){
        return UnityEngine.Random.Range(MinSpeed, MaxSpeed);
    }

    void Update() {
        // If the star drops out of the play window, destroy it.
        if(transform.position.y < yMin - bd.size.y) {
            Destroy(gameObject);
        }
    }
	
}
