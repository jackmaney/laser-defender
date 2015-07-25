using UnityEngine;
using System.Collections;

public class CameraDimensions : MonoBehaviour {
    
    public Vector3 LowerLeft;
    public Vector3 UpperRight;
    
    public float XMin;
    public float XMax;
    
    public float YMin;
    public float YMax;
    
	// Using Awake instead of Start to ensure that 
    // these variables are instantiated before
    // any other MonoBehavior calls its Start method.
	void Awake () {
	    
        LowerLeft = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, Camera.main.nearClipPlane));
        UpperRight = Camera.main.ViewportToWorldPoint(
            new Vector3(1, 1, Camera.main.nearClipPlane));
        
        XMin = LowerLeft.x;
        XMax = UpperRight.x;
        
        YMin = LowerLeft.y;
        YMax = UpperRight.y;
	}
}
