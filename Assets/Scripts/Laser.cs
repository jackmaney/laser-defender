using UnityEngine;
using System.Collections;

public class Laser : Projectile {

    private AudioSource padoo;

    private Vector3 size;

    private GameObject hullHitPrefab;
    private Quaternion hullHitRotation;

    void Awake() {

        hullHitPrefab = Resources.Load<GameObject>(
            "Prefabs/HullHitParticleEmitter");
        hullHitRotation = hullHitPrefab.transform.rotation;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        size = sr.sprite.bounds.size;
        Speed = 10f;
        padoo = GetComponent<AudioSource>();
    }

    public override void Initialize() {
        base.Initialize();
        AudioSource.PlayClipAtPoint(padoo.clip,
            transform.position, 0.25f);
    }

    public Vector3 HeadPosition() {
        return transform.position + new Vector3(0f, 0.5f * size.y, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        HitShip(collision.gameObject.transform);
    }

    public override void HitShip(Transform t) {

        Vector3 position = HeadPosition();
        GameObject hullHit = Instantiate(hullHitPrefab,
                    Vector3.zero, hullHitRotation) as GameObject;
        hullHit.transform.SetParent(t, false);

        // Find out how much to rotate the particle effects and lighting
        // Since our effect starts out pointing straight down, we
        // need to find the angle between this displacement vector
        // and (0, -1)
        Vector2 displacement = position - hullHit.transform.position;

        // The angle (between 0 and 180 degrees) between
        // vectors v1 and v2 is: 
        //      Vector2.Dot(v1, v2) / (v1.magnitude * v2.magnitude)
        // However, since we're dotting with (0, -1), we really
        // only need to divide the y-coordinate of the displacement
        // by its magnitude.

        float angle = 0f;
        if(displacement.magnitude != 0) {
            angle = displacement.y / displacement.magnitude;
        }

        // However, we need to adjust per quadrant, by making
        // the angle negative if it's in the second or
        // third quadrants (remember, an angle of zero
        // corresponds to (0, -1).

        if (displacement.x < 0 && displacement.y != 0) {
            angle *= -1;
        }

        // Convert to degrees...
        angle *= 180f / Mathf.PI;

        hullHit.transform.RotateAround(hullHit.transform.position,
            Vector3.back, angle);

        //base.HitShip();
        Destroy(gameObject);
    }

}
