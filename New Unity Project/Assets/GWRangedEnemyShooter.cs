using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWRangedEnemyShooter : GWEnemyAttackor {


    public GWProjectile projectile;


    /*
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
    */

    public override void Update() {

        switch (this.attackState) {

            case GWAttackState.Roaming:

                if (this.pawnController != null) {
                    this.attackState = GWAttackState.Loading;
                }

                this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                this.enemy.agent.isStopped = true;


                if (this.remainingLoadTime <= 0) {

                    this.attackState = GWAttackState.Attacking;

                    this.remainingLoadTime = this.loadTime;
                }


                this.transform.LookAt(GWPawnController.instance.transform.position);

                Vector3 d = GWPawnController.instance.transform.position - this.transform.position;
                Vector3 v = this.projectile.transform.forward * this.projectile.flySpeed * 50;
                float t = d.magnitude / v.magnitude;
                Vector3 posAfterT = GWPawnController.instance.transform.position +  GWPawnController.instance.velocity * t * 50;
                this.transform.LookAt(posAfterT);
                this.futureAttackPos = posAfterT;

                //this.futureAttackPos = GWPawnController.instance.transform.position + GWPawnController.instance.velocity * this.attackTime * 50;
                //this.transform.LookAt(this.futureAttackPos);
                //this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y,  0);

                this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Attacking:

                this.Attack();

                this.attackState = GWAttackState.Roaming;
                this.enemy.agent.isStopped = false;

                this.remainingAttackTime = this.attackTime;


                break;
            default:
                break;

        }
    }


    public override void Attack() {

        this.projectile.Shoot();

        this.remainingTime = this.cooldownTime;
    }
}
