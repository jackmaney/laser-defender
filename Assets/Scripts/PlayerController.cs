using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
	
	public float Speed = 20f;
    public float MaxSpeed = 40f;
    
    private ParticleSystem engineExhaust;
    
    private Rigidbody2D rb;
    
    private bool movementKeyPressed;
    
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    
    // Percentage of the vertical space in which the ship can move
    private float verticalPct = 0.3f;
	
    
	void Start () {
	    rb = GetComponent<Rigidbody2D>();
        if(Speed > MaxSpeed){
            throw new ArgumentException(
                "Speed > MaxSpeed!");
        }
        
        engineExhaust = GetComponentInChildren<ParticleSystem>();
        engineExhaust.Stop();
        
        movementKeyPressed = false;
        
        Sprite ship = GetComponent<SpriteRenderer>().sprite;

        var camDim = 
            Camera.main.GetComponent<CameraDimensions>();
        xMin = camDim.XMin;
        
        xMin += ship.bounds.extents.x;
        
        xMax = camDim.XMax;
        
        xMax -= ship.bounds.extents.x;
        
        yMin = Camera.main.GetComponent<CameraDimensions>().YMin;
        yMin += ship.bounds.extents.y;
        
        yMax = Camera.main.GetComponent<CameraDimensions>().YMax;
        
        yMax = yMin + verticalPct*(yMax - yMin);
        
	}
    
    void UpdateEngineExhaust(){
        
        
        if(movementKeyPressed && !engineExhaust.isPlaying){
            engineExhaust.Play();
        }
        else if(!movementKeyPressed && engineExhaust.isPlaying){
            engineExhaust.Play();
        }
            
    }
    
    void ClampMovement(){
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);
        transform.position = position;
    }
	
	// Update is called once per frame
	void Update () {
		
		if(Input.anyKey){
            Vector2 vel = new Vector2();
            
			if(
                Input.GetKey(KeyCode.A)
                ||
                Input.GetKey(KeyCode.LeftArrow)    
            ){
                vel += Vector2.left;
            }
            else if(
                Input.GetKey(KeyCode.D)
                ||
                Input.GetKey(KeyCode.RightArrow)
            ){
                vel += Vector2.right;
            }
            
            if(
                Input.GetKey(KeyCode.W)
                ||
                Input.GetKey(KeyCode.UpArrow)
                ){
                vel += Vector2.up;
            }
            else if(
                Input.GetKey(KeyCode.S)
                ||
                Input.GetKey(KeyCode.DownArrow)
                ){
                vel += Vector2.down;
            }
            
            if(vel != Vector2.zero){
                
                movementKeyPressed = true;
                
                vel.Normalize();
                rb.velocity = 
                    Vector2.ClampMagnitude(
                        rb.velocity + Speed * Time.deltaTime * vel, MaxSpeed);
            }
            else{
                movementKeyPressed = false;
            }
            
        }
        else{
            movementKeyPressed = false;
        }
        
        UpdateEngineExhaust();
        ClampMovement();
        
	}
}
