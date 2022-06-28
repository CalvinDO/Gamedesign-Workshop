using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWStun : GWStatusEffect {
    public override void Init() {
        base.Init();

        this.StartCoroutine(ApplyStun(1.5f));
    }

    IEnumerator ApplyStun(float time) {

        this.enemyController.agent.isStopped = true;
        yield return new WaitForSeconds(time);
        this.enemyController.agent.isStopped = false;
        Destroy(this.gameObject.GetComponent<GWSlow>());
    }
}
