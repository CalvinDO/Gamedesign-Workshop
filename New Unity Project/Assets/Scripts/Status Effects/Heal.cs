using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private int maxTicks = 20;
    private int currentTicks;
    private float healAmount = 1;

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
