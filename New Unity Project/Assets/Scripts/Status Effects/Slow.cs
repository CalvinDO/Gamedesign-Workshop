using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
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
        StartCoroutine(ApplySlow(3f));
    }

    IEnumerator ApplySlow(float time){
        float previousMovementSpeed = stats.movementSpeed;
        stats.movementSpeed = previousMovementSpeed/3;
        yield return new WaitForSeconds(time);
        stats.movementSpeed = previousMovementSpeed;
        Destroy(gameObject.GetComponent<Slow>());
    }
}
