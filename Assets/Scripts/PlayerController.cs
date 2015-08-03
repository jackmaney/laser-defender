using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float Speed = 20f;
    public float MaxSpeed = 40f;

    public int MaxHealth = 150;
    private int CurrentHealth;


    private Shield shield;

    private ParticleSystem engineExhaust;

    private Rigidbody2D rb;

    private bool movementKeyPressed;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Percentage of the vertical space in which the ship can move
    private float verticalPct = 0.3f;

    private GameObject weaponPrefab;
    private GameObject laserProjectile;

    private Bar bar;

    private AudioSource engineNoise;

    private AudioManager audioManager;

    void Start() {

        CurrentHealth = MaxHealth;

        audioManager = FindObjectOfType<AudioManager>();

        laserProjectile =
            Resources.Load<GameObject>(
                "Prefabs/Weapons/Projectiles/Red Laser");

        weaponPrefab = Resources.Load<GameObject>(
            "Prefabs/Weapons/Weapon");

        shield = GetComponentInChildren<Shield>();

        bar = GameObject.Find("HullBar").
                GetComponent<Bar>();

        GameObject weapon = Instantiate(weaponPrefab, 
            Vector3.zero, Quaternion.identity)
            as GameObject;

        engineNoise = GetComponent<AudioSource>();
        engineNoise.Stop();

        weapon.transform.SetParent(transform, false);

        Weapon weaponScript = weapon.GetComponent<Weapon>();
        weaponScript.RateOfFire = 0.2f; //0.5f;
        weaponScript.Key = KeyCode.Mouse0;
        weaponScript.Projectile = laserProjectile;

        

        rb = GetComponent<Rigidbody2D>();

        if (Speed > MaxSpeed) {
            throw new ArgumentException(
                "Speed > MaxSpeed!");
        }

        engineExhaust = GetComponentInChildren<ParticleSystem>();
        engineExhaust.Stop();

        movementKeyPressed = false;

        var camDim =
                Camera.main.GetComponent<CameraDimensions>();

        Sprite ship = GetComponent<SpriteRenderer>().sprite;
        xMin = camDim.XMin;

        xMin += ship.bounds.extents.x;

        xMax = camDim.XMax;

        xMax -= ship.bounds.extents.x;

        yMin = Camera.main.GetComponent<CameraDimensions>().YMin;
        yMin += ship.bounds.extents.y;

        yMax = Camera.main.GetComponent<CameraDimensions>().YMax;

        yMax = yMin + verticalPct * (yMax - yMin);
    }

    void UpdateEngine() {

        if (movementKeyPressed && !engineExhaust.isPlaying) {
            engineExhaust.Play();
            engineNoise.Play();
        }
        else if (!movementKeyPressed && engineExhaust.isPlaying) {
            engineExhaust.Stop();
        }

        if (engineExhaust.isStopped) {
            engineNoise.Stop();
        }
        
    }

    void ClampMovement() {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);
        transform.position = position;
    }

    public void TakeDamage(int damage) {
        int damageToShield = damage;
        if(damageToShield > shield.CurrentHealth) {
            damageToShield = shield.CurrentHealth;
        }

        shield.TakeDamage(damageToShield);

        int hullDamage = damage - damageToShield;
        if(hullDamage > 0) {
            CurrentHealth -= hullDamage;
            if(CurrentHealth <= 0) {
                CurrentHealth = 0;
                bar.Display(CurrentHealth, MaxHealth);
                Die();
                return;
            }
        }

        bar.Display(CurrentHealth, MaxHealth);

    }

    void Die() {
        audioManager.Play(audioManager.PlayerDeathClip, 
            transform.position, 0.25f);
        Destroy(gameObject);
        MusicPlayer mp = FindObjectOfType<MusicPlayer>();
        if(mp != null) {
            mp.Play(mp.GameOverMusic);
        }
        Application.LoadLevel("Game Over");
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Projectile projectile =
            collision.gameObject.GetComponent<Projectile>();

        if(projectile != null) {
            TakeDamage(projectile.Damage);
            projectile.HitShip();
        }

    }

        // Update is called once per frame
    void Update() {

        if (Input.anyKey) {
            Vector2 vel = new Vector2();

            if (
                Input.GetKey(KeyCode.A)
                ||
                Input.GetKey(KeyCode.LeftArrow)
            ) {
                vel += Vector2.left;
            }
            else if (
                Input.GetKey(KeyCode.D)
                ||
                Input.GetKey(KeyCode.RightArrow)
            ) {
                vel += Vector2.right;
            }

            if (
                Input.GetKey(KeyCode.W)
                ||
                Input.GetKey(KeyCode.UpArrow)
                ) {
                vel += Vector2.up;
            }
            else if (
                Input.GetKey(KeyCode.S)
                ||
                Input.GetKey(KeyCode.DownArrow)
                ) {
                vel += Vector2.down;
            }

            if (vel != Vector2.zero) {

                movementKeyPressed = true;

                vel.Normalize();
                rb.velocity =
                    Vector2.ClampMagnitude(
                        rb.velocity + Speed * Time.deltaTime * vel, MaxSpeed);
            }
            else {
                movementKeyPressed = false;
            }

        }
        else {
            movementKeyPressed = false;
        }

        UpdateEngine();
        ClampMovement();

    }

    

}
