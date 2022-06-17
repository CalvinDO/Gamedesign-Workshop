using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWProjectileAttackor : GWAttackor {

    public GWProjectile projectile;
    //public GWProjectile flyingProjectile;

    public override void Activate(GWInventorySlot inventorySlot) {

        GWProjectile newProjectile = GameObject.Instantiate(this.projectile, this.projectile.transform.position, this.projectile.transform.rotation, GWPoolManager.instance.projectilePool);
        newProjectile.gameObject.SetActive(true);
        newProjectile.Shoot();
    }
}
