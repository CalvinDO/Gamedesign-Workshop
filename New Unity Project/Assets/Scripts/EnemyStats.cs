using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats
{
    public float maxHealth {get; set;}
    public float currentHealth {get; set;}
    public float movementSpeed {get; set;}

    void Start()
    {
        this.maxHealth = 50;
        this.currentHealth = maxHealth;
        this.movementSpeed = 2;
    }
    void Update()
    {
        if(this.currentHealth <= 0){
            Destroy(gameObject);
        }
    }
}
