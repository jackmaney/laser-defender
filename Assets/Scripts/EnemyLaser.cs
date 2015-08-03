using UnityEngine;
using System.Collections;

public class EnemyLaser : Projectile {

    public float RotationRate;
    private Rigidbody2D rb;

    void Awake() {
        Speed = 5f;
        direction = Vector3.down;
        RotationRate = 90f;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Initialize() {
        base.Initialize();
        GetComponent<Rigidbody2D>().rotation = RotationRate;
    }

    void FixedUpdate() {
        rb.MoveRotation(rb.rotation + 
            RotationRate * Time.fixedDeltaTime);
    }

    public override void Move() {
        base.Move();
        transform.RotateAround(transform.position,
            Vector3.forward, RotationRate * Time.deltaTime);
    }

}
