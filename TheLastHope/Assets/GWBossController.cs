using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWBossController : GWEnemyController
{

    [Range(0, 180)]
    public float maxRotation;

    public Animator animator;
    public GameObject indicators;

    public static GWBossController instance;



    void Awake() {
        GWBossController.instance = this;
    }
    public override void Start()
    {
        this.stats = this.gameObject.GetComponent<GWEnemyStats>();

        InvokeRepeating("RotateNHit", 3f, 3f);
    }

    // Update is called once per frame
    public override void Update()
    {
        //this.animator.ResetTrigger("attackSlam");
    }

    void RotateNHit() {

        float rand = Random.Range(-this.maxRotation, this.maxRotation);
        this.transform.rotation = Quaternion.Euler(0, 180 - rand, 0);
        this.animator.SetTrigger("attackSlam");

        GWBossController.instance.indicators.gameObject.SetActive(true);
    }
}
