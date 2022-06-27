using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWStatusEffect : MonoBehaviour
{

    public GWIStats stats;

    public GWEnemyController enemyController;

    void Awake() {
        this.enemyController = this.gameObject.GetComponent<GWEnemyController>();
    }
    void Start()
    {
        this.Init();
    }


    public virtual void Init() {

        if (this.gameObject.TryGetComponent<GWPawnStats>(out GWPawnStats pStats)) {
            this.stats = pStats;
        }
        if (this.gameObject.TryGetComponent<GWEnemyStats>(out GWEnemyStats eStats)) {
            this.stats = eStats;
        }
        if (this.stats == null) {
            Debug.Log("stats was null");
        }
    }
}
