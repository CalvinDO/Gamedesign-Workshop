using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private int maxTicks = 7;
    private int currentTicks;
    private float percentDmg = 7;

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
        InvokeRepeating("DealPoisonDmg", 1f, 1f);
    }

    void DealPoisonDmg(){
        stats.currentHealth -= stats.maxHealth * (0.01f*percentDmg);
        currentTicks++;
        if(this.currentTicks>=maxTicks) Destroy(gameObject.GetComponent<Poison>());
    }
}
