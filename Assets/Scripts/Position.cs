using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
    
    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    public void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    /// <summary>
    /// An accessor for the Enemy GameObject at this Position.
    /// </summary>
    /// <returns>The Enemy (as a GameObject) at this Position.</returns>
    public GameObject Enemy() {
        Enemy enemyComponent = transform.GetComponentInChildren<Enemy>();

        if(enemyComponent == null) {
            return null;
        }

        return enemyComponent.gameObject;
    }

    public bool IsEmpty() {

        return Enemy() == null;
    }

}
