using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public bool ReCountEnemies;

    private float spawnDelay = 0.5f;
    private float delayBetweenWaves = 2f;

    private GameObject enemyPrefab;

    // For gizmo drawing
    public float GizmoWidth;
    public float GizmoHeight;
    public float GizmoVerticalOffset;

    private GameObject[] positions = new GameObject[] { };

    private Vector3 direction = Vector3.left;
    public float speed;

    private CameraDimensions camDim;

    private List<Enemy> enemies = new List<Enemy>();

    private Enemy leftMostEnemy;
    private Enemy rightMostEnemy;

    private GameObject weaponPrefab;
    private GameObject laserProjectile;

    void Start() {

        ReCountEnemies = false;

        weaponPrefab = Resources.Load<GameObject>(
            "Prefabs/Weapons/ProbabilisticWeapon");

        laserProjectile =
            Resources.Load<GameObject>(
                "Prefabs/Weapons/Projectiles/Green Laser");

        string path = "Prefabs/Ships/Enemies/Enemy";
        enemyPrefab = Resources.Load<GameObject>(path);
        camDim = Camera.main.GetComponent<CameraDimensions>();

        Position[] pos = transform.GetComponentsInChildren<Position>();
        positions = new GameObject[pos.Length];

        for(int i=0; i<pos.Length; i++) {
            positions[i] = pos[i].gameObject;
        }

        SpawnEnemies();
        
    }

    void SpawnEnemies() {

        Transform freePosition = NextFreePosition();

        if(freePosition != null) {
            InitializeEnemy(freePosition);
        }

        if (NextFreePosition()) {
            Invoke("SpawnEnemies", spawnDelay);
        }

    }

    public void FindEnemies() {

        enemies.Clear();

        float xMin = 0;
        float xMax = 0;
        bool first = true;

        foreach(GameObject p in positions) {
            GameObject enemyObj = p.GetComponent<Position>().Enemy();

            if(enemyObj != null) {
                Enemy e = enemyObj.GetComponent<Enemy>();

                Bounds b = e.EnemySprite.bounds;

                float left = e.transform.position.x - b.extents.x;
                float right = e.transform.position.x + b.extents.x;
                if (first) {
                    xMin = left;
                    xMax = right;
                    leftMostEnemy = e;
                    rightMostEnemy = e;
                    first = false;
                }
                else {
                    if(xMin > left) {
                        xMin = left;
                        leftMostEnemy = e;
                    }

                    if(xMax < right) {
                        xMax = right;
                        rightMostEnemy = e;
                    }
                }

                enemies.Add(e);
            }
        }

        if(enemies.Count == 0) {
            Invoke("SpawnEnemies", delayBetweenWaves);
        }

    }


    void Update() {

        if (ReCountEnemies) {
            FindEnemies();
            ReCountEnemies = false;
        }

        Vector3 position = transform.position;
        position += speed * direction * Time.deltaTime;

        float left = 0;
        float right = 0;

        if(enemies.Count == 0) {
            left = transform.position.x - GizmoWidth / 2f;
            right = transform.position.x + GizmoWidth / 2f;
        }
        else {
            left = leftMostEnemy.transform.position.x -
                        leftMostEnemy.EnemySprite.bounds.extents.x;

            right = rightMostEnemy.transform.position.x +
                            rightMostEnemy.EnemySprite.bounds.extents.x;
        }

        if (direction == Vector3.left && left < camDim.XMin) {
            direction = Vector3.right;
        }
        else if(direction == Vector3.right && right > camDim.XMax) {
            direction = Vector3.left;
        }

        transform.position = position;
    }

    void InitializeWeapon(Transform enemyTransform) {
        GameObject weapon = Instantiate(weaponPrefab,
                Vector3.zero, Quaternion.identity)
                as GameObject;

        weapon.transform.SetParent(enemyTransform, false);

        weapon.GetComponent<ProbabilisticWeapon>().Probability = 0.01f;
        weapon.GetComponent<ProbabilisticWeapon>().Projectile =
            laserProjectile;

    }

    void InitializeEnemy(Transform positionTransform) {
        GameObject enemy = Instantiate(enemyPrefab,
                Vector3.zero, Quaternion.identity) as GameObject;

        enemy.transform.SetParent(positionTransform, false);

        InitializeWeapon(enemy.transform);

        ReCountEnemies = true;
    }

    Transform NextFreePosition() {
        foreach(GameObject p in positions) {
            if(p.transform.childCount == 0) {
                return p.transform;
            }
        }

        return null;
    }

    bool AllEnemiesDead() {
        int count = 0;

        foreach(GameObject p in positions) {
            if (!p.GetComponent<Position>().IsEmpty()) {
                count++;
            }
        }

        return count == 0;
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + GizmoVerticalOffset * Vector3.up, 
            new Vector3(GizmoWidth, GizmoHeight));
    }
}
