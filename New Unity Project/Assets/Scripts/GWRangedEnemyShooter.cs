using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWRangedEnemyShooter : GWEnemyAttackor {


    public GWProjectile projectile;

    public GameObject aimLaser;

    public override void Update() {

        switch (this.attackState) {

            case GWAttackState.Roaming:

                if (this.pawnController != null) {
                    this.attackState = GWAttackState.Loading;

                }

                this.aimLaser.SetActive(false);

                break;
            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                this.enemy.agent.isStopped = true;


                if (this.remainingLoadTime <= 0) {

                    this.projectile.transform.parent = this.transform;

                    this.projectile.transform.localPosition = Vector3.zero + Vector3.up;
                    this.projectile.transform.localRotation = Quaternion.identity;

                    this.attackState = GWAttackState.Attacking;
                    this.aimLaser.SetActive(true);

                    this.remainingLoadTime = this.loadTime;
                }


                this.transform.LookAt(GWPawnController.instance.transform.position);

                Vector3 d = GWPawnController.instance.transform.position - this.transform.position;
                Vector3 v = this.projectile.transform.forward * this.projectile.flySpeed * 50;
                float t = d.magnitude / v.magnitude;
                Vector3 posAfterT = GWPawnController.instance.transform.position +  GWPawnController.instance.velocity * t * 50;
                
                this.transform.LookAt(posAfterT);
                this.futureAttackPos = posAfterT;

                
                break;
            case GWAttackState.Attacking:

                this.remainingAttackTime -= Time.deltaTime;

                

                if (this.remainingAttackTime <= 0) {

                    this.Attack();

                    this.attackState = GWAttackState.Roaming;
                    this.enemy.agent.isStopped = false;

                    this.remainingAttackTime = this.attackTime;

                    this.aimLaser.SetActive(false);
                }

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
