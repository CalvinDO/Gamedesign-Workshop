using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WaterWave : GWSpell
{
    public float range = 5;
    
    public override void Activate(GameObject parent)
    {
        GWPawnStats stats = parent.GetComponent<GWPawnStats>();
        stats.movementSpeed = 5;
    }
    public override void BeginCooldown(GameObject parent)
    {
        GWPawnStats stats = parent.GetComponent<GWPawnStats>();
        stats.movementSpeed = 5;
    }
    
}
