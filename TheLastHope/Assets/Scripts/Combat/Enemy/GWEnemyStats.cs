using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWEnemyStats : MonoBehaviour, GWIStats {
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public float movementSpeed { get; set; }
    public bool isDisarmed { get; set; }

    //Order: Earth, fire, water, air
    public Vector4 sensibilities = new Vector4(0.8f, 0.4f, 0.1f, 0.15f);

    public float manualMaxHealth;

    void Start() {
        if (this.manualMaxHealth == 0) {
            this.maxHealth = 50;
        }
        else {
            this.maxHealth = this.manualMaxHealth;
        }

        this.currentHealth = maxHealth;
    }
    void Update() {
        if (this.currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
