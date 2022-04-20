using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{

    public IStats stats;

    void Start()
    {
        this.Init();

     
    }


    public virtual void Init() {
        if (gameObject.TryGetComponent<PlayerStats>(out PlayerStats pStats)) {
            stats = pStats;
        }
        if (gameObject.TryGetComponent<EnemyStats>(out EnemyStats eStats)) {
            stats = eStats;
        }
        if (stats == null) {
            Debug.Log("stats was null");
        }
    }
}
