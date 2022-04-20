using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats
{
    public float maxHealth {get; set;}
    public float currentHealth {get; set;}
    public float movementSpeed {get; set;}

    void Start()
    {
        this.maxHealth = 150;
        this.currentHealth = maxHealth;
        this.movementSpeed = 1f;
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
