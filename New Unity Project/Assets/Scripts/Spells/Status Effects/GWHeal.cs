using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWHeal : GWStatusEffect {

    private int maxTicks = 20;
    public int currentTicks;
    private float healAmount = 1;

    public override void Init() {
        base.Init();

        this.currentTicks = 0;
        this.InvokeRepeating("ApplyHealing", 1f, 0.5f);
    }

    void ApplyHealing() {

        if (this.stats.currentHealth < this.stats.maxHealth) {
            this.stats.currentHealth += this.healAmount;
        }

        this.currentTicks++;

        if (this.currentTicks >= this.maxTicks) {
            Destroy(this.gameObject.GetComponent<GWHeal>());
        }
    }
}
