using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    public float RechargeRate; // Amount of health per second
    public float RechargeDelay; // Seconds before recharge begins

    private bool canRecharge;

    // Keeps track of fractional part of recharged health.
    private float rechargingHealth;

    private SpriteRenderer sr;
    private Bar bar;

    private AudioSource shieldUpSound, shieldDownSound;

    void Awake() {
        CurrentHealth = MaxHealth;
    }
    
	// Use this for initialization
	void Start () {
        rechargingHealth = 0;
        canRecharge = false;
        sr = GetComponent<SpriteRenderer>();
        bar = GameObject.Find("ShieldBar").
                GetComponent<Bar>();

        AudioSource[] tracks = GetComponents<AudioSource>();

        foreach(AudioSource track in tracks) {
            if(track.clip.name == "sfx_shieldDown") {
                shieldDownSound = track;
            }
            else if(track.clip.name == "sfx_shieldUp") {
                shieldUpSound = track;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        Recharge();
	}

    void SetAlpha() {
        float alpha = (float)CurrentHealth / MaxHealth;
        SetAlpha(alpha);
    }

    void SetAlpha(float alpha) {
        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }

    IEnumerator WaitForRecharge() {
        canRecharge = false;
        yield return new WaitForSeconds(RechargeDelay);
        canRecharge = true;
    }


    public void TakeDamage(int damage) {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) {
            CurrentHealth = 0;
            AudioSource.PlayClipAtPoint(shieldDownSound.clip,
                transform.position);
        }

        SetAlpha();
        bar.Display(CurrentHealth, MaxHealth);

        StartCoroutine(WaitForRecharge());
    }

    void Recharge() {
        if (canRecharge && CurrentHealth < MaxHealth) {

            rechargingHealth +=  RechargeRate * Time.deltaTime;
            int integerPart = (int)rechargingHealth;

            if (integerPart > 0) {
                rechargingHealth -= integerPart;
                CurrentHealth += integerPart;

                if(CurrentHealth >= MaxHealth) {
                    CurrentHealth = MaxHealth;
                    AudioSource.PlayClipAtPoint(
                        shieldUpSound.clip, transform.position);
                }

                SetAlpha();
                bar.Display(CurrentHealth, MaxHealth);

                
            }
        }
    }
}
