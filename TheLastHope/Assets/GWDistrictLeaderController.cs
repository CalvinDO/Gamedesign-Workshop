using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWDistrictLeaderController : GWEnemyController {
    public GWEnemyAttackor[] attackors;

    public float switchInterval;
    public float remainingSwitchTime;
    public GWSpell[] dropSpells;

    public override void Start() {

        base.Start();

        this.remainingSwitchTime = this.switchInterval;
        this.agent.isStopped = false;
    }

    // Update is called once per frame
    public override void Update() {

        /*
        if (!this.GetComponent<GWSlow>() && !this.GetComponent<GWStun>()) {

            this.currentMovementSpeed = 2;
        }
        */
        base.Update();

        this.remainingSwitchTime -= Time.deltaTime;

        if (this.remainingSwitchTime <= 0) {
            this.remainingSwitchTime = this.switchInterval;
            this.SwitchAttackor();
        }

        //Debug.Log(this.agent.isStopped);
    }

    void SwitchAttackor() {

        this.attackor.attackState = GWAttackState.Roaming;
        this.attackor.weapon.gameObject.SetActive(false);
        this.agent.isStopped = false;

        this.attackor.gameObject.SetActive(false);
        this.attackor = this.attackors[(int)Random.Range(0, this.attackors.Length)];
        this.attackor.gameObject.SetActive(true);

        //Debug.Log(this.agent.isStopped + " should be false");
    }


    override public void Die() {

        GWCollectableSpell collectible = null;
        collectible.spell = this.dropSpells[(int)this.element];
        Instantiate(collectible, this.transform.position, Quaternion.identity);
       

        GameObject.Destroy(this.gameObject);
    }
}
