using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWDistrictLeaderController : GWEnemyController {
    public GWEnemyAttackor[] attackors;

    public float switchInterval;
    public float remainingSwitchTime;

    void Start() {
        this.remainingSwitchTime = this.switchInterval;
    }

    // Update is called once per frame
    void Update() {
        this.remainingSwitchTime -= Time.deltaTime;

        if (this.remainingSwitchTime <= 0) {
            this.remainingSwitchTime = this.switchInterval;
            this.SwitchAttackor();
        }
    }

    void SwitchAttackor() {

        this.attackor.attackState = GWAttackState.Roaming;
        this.attackor.weapon.gameObject.SetActive(false);
        this.agent.isStopped = false;

        this.attackor.gameObject.SetActive(false);
        this.attackor = this.attackors[(int)Random.Range(0, this.attackors.Length)];
        this.attackor.gameObject.SetActive(true);

        
    }
}
