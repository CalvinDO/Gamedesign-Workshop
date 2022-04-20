using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffect
{

    public override void Init() {
        base.Init();

        StartCoroutine(ApplySlow(3f));
    }

    IEnumerator ApplySlow(float time){
        float previousMovementSpeed = stats.movementSpeed;
        stats.movementSpeed = previousMovementSpeed/3;
        yield return new WaitForSeconds(time);
        stats.movementSpeed = previousMovementSpeed;
        Destroy(gameObject.GetComponent<Slow>());
    }
}
