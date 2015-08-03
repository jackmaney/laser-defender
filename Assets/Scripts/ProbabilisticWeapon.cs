using UnityEngine;
using System.Collections;

public class ProbabilisticWeapon : Weapon {

    private float timeSinceLastFire;
    public float Probability = 0.1f;

	void Awake() {
        AutoFire = true;
    }

    public override void Initialize() {
        Damage = 100;
        canFire = true;
    }

    public override void Fire() {
        base.Fire();
        timeSinceLastFire = 0f;
    }

    protected override void FireHandler() {

        timeSinceLastFire += Time.deltaTime;

        float diceRoll = UnityEngine.Random.value;

        if(diceRoll <= Probability * timeSinceLastFire) {
            Fire();
        }
    }
}
