using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : GWStatusEffect {
    private int maxTicks = 7;
    public int currentTicks;
    private float percentDmg = 7;


    public override void Init() {
        base.Init();

        this.currentTicks = 0;
        this.InvokeRepeating("DealPoisonDmg", 1f, 1f);
    }

    void DealPoisonDmg() {
        this.stats.currentHealth -= this.stats.maxHealth * (0.01f * this.percentDmg);
        this.currentTicks++;
        if (this.currentTicks >= this.maxTicks) {
            Destroy(this.gameObject.GetComponent<Poison>());
        }
    }
}
