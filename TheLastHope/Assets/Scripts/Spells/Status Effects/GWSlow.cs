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
        this.stats.isSlowed = true;
        this.enemyController.currentMovementSpeed = this.stats.movementSpeed / 3;
        yield return new WaitForSeconds(time);
        this.enemyController.currentMovementSpeed = this.stats.movementSpeed;
        this.stats.isSlowed = false;

        Destroy(this.gameObject.GetComponent<GWSlow>());
    }
}
