using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWProjectile: MonoBehaviour {
    [Range(0, 1)]
    public float flySpeed;

    private bool isFlying;



    void FixedUpdate() {
        if (this.isFlying) {
            this.transform.Translate(Vector3.forward * this.flySpeed);
        }
    }

    public void Shoot() {
        this.isFlying = true;
        this.transform.parent = null;
    }
}