using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    protected float Speed { get; set; }
    protected Vector3 direction = Vector3.up;

    public int Damage;

    void Start() {
        Initialize();
    }

    public virtual void Initialize() {

    }

    public virtual void Move() {
        transform.position += Speed * Time.deltaTime * direction;
    }

    public virtual void DestroyWhenOffScreen() {
        if(!GetComponent<Renderer>().isVisible) {
            Destroy(gameObject);
        }
    }

	void Update(){
        Move();
        DestroyWhenOffScreen();
	}

    public virtual void HitShip() {
        Destroy(gameObject);
    }

    public virtual void HitShip(Transform t) {
        
    }
}
