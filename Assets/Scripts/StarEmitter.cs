using UnityEngine;
using System.Collections;

public class StarEmitter : MonoBehaviour {
    
    private float MinWaitTime;
    private float MaxWaitTime;
    
    private bool active;
    
    private GameObject starPrefab;
    
    private float xMin, xMax, yMax;
    
	// Use this for initialization
	void Start () {
	    active = false;
        
        starPrefab = Resources.Load("Prefabs/Star") as GameObject;
        
        MinWaitTime = 0.01f;
        MaxWaitTime = 0.25f;
        
        CameraDimensions camDim = Camera.main.GetComponent<CameraDimensions>();

        xMin = camDim.XMin;
        xMax = camDim.XMax;
        yMax = camDim.YMax;
        
        Begin();
        
	}
    
    
    public void Stop(){
        active = false;
    }
    
    public void Begin(){
        active = true;
        StartCoroutine(EmitStars());
    }
    
    float WaitTime(){
        return UnityEngine.Random.Range(MinWaitTime, MaxWaitTime);
    }
    
    float RandomXCoordinate(){
    
        return UnityEngine.Random.Range(xMin, xMax);
    }
    
    IEnumerator EmitStars(){
        
        while(active){
            Vector3 position = new Vector3(RandomXCoordinate(),
                    yMax + Star.MaxSize, 0);
            Instantiate(starPrefab, position, Quaternion.identity);
            
            yield return new WaitForSeconds(WaitTime());
        
        }

    }
	
}
