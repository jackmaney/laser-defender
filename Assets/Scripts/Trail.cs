using UnityEngine;

public class Trail : MonoBehaviour {
    
    private bool startedBecomingVisible = false;
    private TrailRenderer tr;
    
	void Start () {
	    tr = GetComponent<TrailRenderer>();
	}
	
    void Update () {
	    
        if(!startedBecomingVisible && tr.isVisible){
            startedBecomingVisible = true;
        }
        else if(startedBecomingVisible && !tr.isVisible){
            Destroy(gameObject.GetComponentInParent<Star>().gameObject);
        }
        
	}
}
