using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWStun : GWStatusEffect {
    public override void Init() {
        base.Init();

        this.StartCoroutine(ApplyStun(2f));
    }

    IEnumerator ApplyStun(float time) {

        this.enemyController.agent.isStopped = true;
        this.enemyController.rb.isKinematic = true;
        this.stats.isStunned = true;

        yield return new WaitForSeconds(time);
        this.enemyController.agent.isStopped = false;
        this.enemyController.rb.isKinematic = false;
        this.stats.isStunned = false;

        Destroy(this.gameObject.GetComponent<GWStun>());
    }
}
