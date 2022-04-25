using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSlow : GWStatusEffect
{
    public override void Init() {
        base.Init();

        this.StartCoroutine(ApplySlow(3f));
    }

    IEnumerator ApplySlow(float time){
        float previousMovementSpeed = this.stats.movementSpeed;
        this.stats.movementSpeed = previousMovementSpeed/3;
        yield return new WaitForSeconds(time);
        this.stats.movementSpeed = previousMovementSpeed;
        Destroy(this.gameObject.GetComponent<GWSlow>());
    }
}
