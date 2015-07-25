using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    private GameObject enemyPrefab;

    void Start() {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        GameObject enemy = Instantiate(enemyPrefab, Vector3.zero,       
            Quaternion.identity) as GameObject;

        enemy.transform.SetParent(transform, false);
    }
}
