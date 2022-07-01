using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GWPawnStats : MonoBehaviour, GWIStats
{
    public float maxHealth {get; set;}
    public float currentHealth {get; set;}
    public float movementSpeed {get; set;}
    public bool isDisarmed { get; set; }

    void Start()
    {
        this.maxHealth = 150;
        this.currentHealth = maxHealth;
        this.movementSpeed = 1.33f;
    }

    void Awake() {
       // this.maxHealth = 150;
        //this.currentHealth = maxHealth;
        //this.movementSpeed = 1f;
    }

    void Update()
    {
        if(this.currentHealth <= 0){
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
