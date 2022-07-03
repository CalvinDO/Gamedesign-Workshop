using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWBurn : MonoBehaviour
{
    private int maxTicks = 5;
    public int currentTicks;
    private float burnDmg = 1;

    private GWIStats stats;


    void Start()
    {
        if(gameObject.TryGetComponent<GWPawnStats>(out GWPawnStats pStats)){
            stats = pStats;
        }
        if(gameObject.TryGetComponent<GWEnemyStats>(out GWEnemyStats eStats)){
            stats = eStats;
        }
        if(stats==null){
            Debug.Log("stats was null");
        }
        currentTicks = 0;
        InvokeRepeating("DealBurnDmg", 0.8f, 0.8f);
    }

    void DealBurnDmg(){

        this.stats.isBurning = true;
        stats.currentHealth -= burnDmg;
        currentTicks++;
        if (this.currentTicks >= maxTicks) {
            this.stats.isBurning = false;
            Destroy(gameObject.GetComponent<GWBurn>());
        }
    }

}
