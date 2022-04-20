using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    private int maxTicks = 5;
    private int currentTicks;
    private float burnDmg = 10;

    private IStats stats;
    void Start()
    {
        if(gameObject.TryGetComponent<PlayerStats>(out PlayerStats pStats)){
            stats = pStats;
        }
        if(gameObject.TryGetComponent<EnemyStats>(out EnemyStats eStats)){
            stats = eStats;
        }
        if(stats==null){
            Debug.Log("stats was null");
        }
        currentTicks = 0;
        InvokeRepeating("DealBurnDmg", 0.8f, 0.8f);
    }

    void DealBurnDmg(){
        stats.currentHealth -= burnDmg;
        currentTicks++;
        if(this.currentTicks>=maxTicks) Destroy(gameObject.GetComponent<Burn>());
    }

}
