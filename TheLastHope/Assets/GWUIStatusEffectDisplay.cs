using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GWUIStatusEffectDisplay : MonoBehaviour {

    public Image disarmedImage;
    public Image burningImage;
    public Image slowedImage;
    public Image stunnedImage;



    public GWEnemyStats ememyStats;

    void Start() {
        this.ememyStats = this.transform.root.GetComponent<GWEnemyController>().stats;
    }

    // Update is called once per frame
    void Update() {

        try {
            this.disarmedImage.gameObject.SetActive(this.ememyStats.isDisarmed);
            this.burningImage.gameObject.SetActive(this.ememyStats.isBurning);
            this.slowedImage.gameObject.SetActive(this.ememyStats.isSlowed);
            this.stunnedImage.gameObject.SetActive(this.ememyStats.isStunned);

        }
        catch (Exception e) {

            Debug.LogWarning("weird exception, unusual: " + e.Message);

            this.disarmedImage.gameObject.SetActive(false);
            this.burningImage.gameObject.SetActive(false);
            this.slowedImage.gameObject.SetActive(false);
            this.stunnedImage.gameObject.SetActive(false);
        }
    }
}
