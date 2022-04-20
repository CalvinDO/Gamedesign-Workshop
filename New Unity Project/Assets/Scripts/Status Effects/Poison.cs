using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    private int maxTicks = 7;
    public int currentTicks;
    private float percentDmg = 7;


    public override void Init() {
        base.Init();

        currentTicks = 0;
        InvokeRepeating("DealPoisonDmg", 1f, 1f);
    }

    void DealPoisonDmg(){
        stats.currentHealth -= stats.maxHealth * (0.01f*percentDmg);
        currentTicks++;
        if(this.currentTicks>=maxTicks) Destroy(gameObject.GetComponent<Poison>());
    }
}
