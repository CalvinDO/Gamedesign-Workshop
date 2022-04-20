using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWStatusEffect : MonoBehaviour
{

    public IStats stats;

    void Start()
    {
        this.Init();
    }


    public virtual void Init() {
        if (this.gameObject.TryGetComponent<GWPawnStats>(out GWPawnStats pStats)) {
            this.stats = pStats;
        }
        if (this.gameObject.TryGetComponent<EnemyStats>(out EnemyStats eStats)) {
            this.stats = eStats;
        }
        if (this.stats == null) {
            Debug.Log("stats was null");
        }
    }
}
