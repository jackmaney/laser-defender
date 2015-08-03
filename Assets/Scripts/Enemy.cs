using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int Health = 150;

    public EnemyColor EColor;

    public int index = -1; // 0 to 4 to pick a particular ship

    public Sprite EnemySprite;

    private EnemySpawner spawner;
    private ScoreKeeper scoreKeeper;
    private AudioManager audioManager;

    void Awake() {

        audioManager = FindObjectOfType<AudioManager>();
        spawner = FindObjectOfType<EnemySpawner>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        if (index >= 0 && index <= 4) {
            EnemySprite =
                FindObjectOfType<EnemySpriteLoader>().
                    GetSprite(EColor, index);

            GetComponent<SpriteRenderer>().sprite = EnemySprite;
        }
        else {
            EnemySprite =
                FindObjectOfType<EnemySpriteLoader>().Random(EColor);
            GetComponent<SpriteRenderer>().sprite = EnemySprite;
        }

        if (GetComponent<PolygonCollider2D>() != null) {
            Destroy(GetComponent<PolygonCollider2D>());
        }

        PolygonCollider2D poly =
            gameObject.AddComponent<PolygonCollider2D>();

        poly.isTrigger = true;
    }

    void Die() {
        spawner.ReCountEnemies = true;
        scoreKeeper.EnemyDestroyed();
        audioManager.Play(audioManager.EnemyDeathClip,
                transform.position, 0.25f);
        Destroy(gameObject);
    }

    void TakeDamage(int damage) {
        Health -= damage;

        if(Health <= 0) {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Projectile projectile =
            collision.gameObject.GetComponent<Projectile>();

        if (projectile != null) {
            TakeDamage(projectile.Damage);
            projectile.HitShip(transform);
        }
    }

}
