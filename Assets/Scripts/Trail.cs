using UnityEngine;

public class Trail : MonoBehaviour {
    
    private bool startedBecomingVisible = false;
    private LineRenderer lr;
    
	void Start () {
	    lr = GetComponent<LineRenderer>();
	}
	
    void Update () {
	    
        if(!startedBecomingVisible && lr.isVisible){
            startedBecomingVisible = true;
        }
        else if(startedBecomingVisible && !lr.isVisible){
            Destroy(
                gameObject.GetComponentInParent<Weapon>().gameObject);
            Destroy(gameObject);
        }
        
	}
}
