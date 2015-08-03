using UnityEngine;
using System.Collections;

public class HullHitParticleEmitter : MonoBehaviour {

    private ParticleSystem ps;
    private float duration;


	// Use this for initialization
	void Start () {

        ps = GetComponent<ParticleSystem>();
        duration = ps.duration;
        StartCoroutine(timeBomb());
	}
	
    IEnumerator timeBomb() {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
