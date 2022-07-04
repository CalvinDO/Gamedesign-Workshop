using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWBossEventHandler : MonoBehaviour {
  

    public void AttackSlamImpact() {
        Debug.Log("impact");
        GWBossController.instance.indicators.gameObject.SetActive(false);
    }
}
