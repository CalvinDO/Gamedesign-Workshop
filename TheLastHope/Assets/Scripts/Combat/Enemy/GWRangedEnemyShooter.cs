using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWRangedEnemyShooter : GWEnemyAttackor {


    public GWProjectile projectile;
    public GWProjectile flyingProjectile;

    public GameObject aimLaser;

    public override void Update() {

        switch (this.attackState) {

            case GWAttackState.Roaming:

                if (this.reachablePawnController != null) {
                    this.attackState = GWAttackState.Loading;

                }

                this.aimLaser.SetActive(false);

                break;
            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                this.enemy.agent.isStopped = true;


                if (this.remainingLoadTime <= 0) {
                    this.ActivateLaser();
                }


                this.transform.LookAt(GWPawnController.instance.transform.position);

                Vector3 d = GWPawnController.instance.transform.position - this.transform.position;
                Vector3 v = this.projectile.transform.forward * this.projectile.flySpeed * 50;
                float t = d.magnitude / v.magnitude;
                Vector3 posAfterT = GWPawnController.instance.transform.position + GWPawnController.instance.velocity * t * 50;

                this.transform.LookAt(posAfterT);
                this.futureAttackPos = posAfterT;


                break;
            case GWAttackState.Active:

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

    public void ActivateLaser() {

        this.projectile.transform.parent = this.transform;

        this.projectile.transform.localPosition = Vector3.zero + Vector3.up;
        this.projectile.transform.localRotation = Quaternion.identity;

        this.attackState = GWAttackState.Active;
        this.aimLaser.SetActive(true);

        this.remainingLoadTime = this.loadTime;
    }
    public override void Attack() {

        this.animator.SetTrigger("rangedAttack");

        this.flyingProjectile = GameObject.Instantiate(this.projectile, GWPoolManager.instance.projectilePool);
        this.flyingProjectile.transform.SetPositionAndRotation(this.projectile.transform.position, this.projectile.transform.rotation);
        this.flyingProjectile.Shoot();

        this.remainingTime = this.cooldownTime;
    }
}
