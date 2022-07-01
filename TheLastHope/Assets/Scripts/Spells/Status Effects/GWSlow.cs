using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSlow : GWStatusEffect {

    public override void Init() {
        base.Init();

        this.StartCoroutine(ApplySlow(3f));
    }

    IEnumerator ApplySlow(float time) {

        //Debug.Log("slowing down!");
        this.enemyController.currentMovementSpeed = this.stats.movementSpeed / 3;
        yield return new WaitForSeconds(time);
        this.enemyController.currentMovementSpeed = this.stats.movementSpeed;
        Destroy(this.gameObject.GetComponent<GWSlow>());
    }
}
