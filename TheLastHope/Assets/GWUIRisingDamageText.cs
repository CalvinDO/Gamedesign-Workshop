using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GWUIRisingDamageText : MonoBehaviour {
    [Range(0, 1)]
    public float lifeTime;
    [Range(1, 10)]
    public float speed;
    private float livedTime = 0;

    public Text text;


    void Start() {

    }

    // Update is called once per frame
    void Update() {

        this.livedTime += Time.deltaTime;

        if (this.livedTime > this.lifeTime) {
            GameObject.Destroy(this.gameObject);
            return;
        }

        this.transform.Translate(Vector3.up * this.speed * Time.deltaTime);
    }

    public void SetHurt(float damage) {
        this.text.text = "-" + damage;
    }
}
