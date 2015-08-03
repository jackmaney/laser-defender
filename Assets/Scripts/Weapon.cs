using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float RateOfFire;
    public KeyCode Key;
    public int Damage;

    public GameObject Projectile;

    protected bool canFire;

    // Is this an autofire weapon?
    public bool AutoFire;

    // Time offset so that enemies aren't firing in unison
    public float fireTimeOffset;

    public virtual void Initialize() {
        Damage = 100;
        canFire = true;

        if(fireTimeOffset > 0) {
            StartCoroutine(InitialWait());
        }
    }

    IEnumerator InitialWait() {

        canFire = false;
        yield return new WaitForSeconds(fireTimeOffset);
        canFire = true;
    }


    void Start() {
        Initialize();
    }

    public virtual void Fire() {
        GameObject projectile = Instantiate(Projectile, 
            transform.position, Quaternion.identity) as GameObject;

        projectile.GetComponent<Projectile>().Damage = Damage;

    }

    /// <summary>
    /// Cycles canFire every RateOfFire seconds,
    /// ensuring that ROF is respected whether or not
    /// the specified button is held down (unless
    /// AutoFire is enabled).
    /// </summary>
    protected virtual IEnumerator EnforceFireRate() {

        canFire = false;
        yield return new WaitForSeconds(RateOfFire);
        canFire = true;
    }

    protected virtual void FireHandler() {
        if (AutoFire && canFire) {
            Fire();
            StartCoroutine(EnforceFireRate());
        }
        else {
            if (Input.GetKeyDown(Key) && canFire) {
                StartCoroutine(EnforceFireRate());
                InvokeRepeating("Fire",
                    1e-5f + fireTimeOffset, RateOfFire);
            }

            if (Input.GetKeyUp(Key)) {
                CancelInvoke("Fire");
            }
        }
    }

	
	// Update is called once per frame
	void Update () {
        FireHandler();
	}
}
