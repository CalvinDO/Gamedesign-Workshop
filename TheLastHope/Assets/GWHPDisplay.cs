using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GWHPDisplay : MonoBehaviour
{
    public Text text;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.text.text =  "" + GWPawnController.instance.stats.currentHealth;
    }
}
