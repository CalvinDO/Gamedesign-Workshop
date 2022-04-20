using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : StatusEffect
{
    private int maxTicks = 20;
    public int currentTicks;
    private float healAmount = 1;

    

    public override void Init() {
        base.Init();

        currentTicks = 0;
        InvokeRepeating("ApplyHealing", 1f, 0.5f);
    }

    void ApplyHealing(){
        if(stats.currentHealth<stats.maxHealth){
            stats.currentHealth += healAmount;
        }
        currentTicks++;
        if(this.currentTicks>=maxTicks) Destroy(gameObject.GetComponent<Heal>());
    }
}
