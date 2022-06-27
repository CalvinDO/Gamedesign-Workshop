using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWDisarm : GWStatusEffect {
    public override void Init() {
        base.Init();

        
        this.StartCoroutine(ApplyDisarm(10f));
    }

    IEnumerator ApplyDisarm(float time) {


        this.stats.isDisarmed = true;
        yield return new WaitForSeconds(time);
        this.stats.isDisarmed = false;
        Destroy(this.gameObject.GetComponent<GWDisarm>());
    }
}
