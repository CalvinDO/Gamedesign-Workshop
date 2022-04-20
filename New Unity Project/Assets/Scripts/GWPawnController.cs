using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GWPawnController : MonoBehaviour {

    public Rigidbody rb;
    public Text text;
    public GWPawnStats stats;
    public GameObject movingPawn;

    public static GWPawnController instance;

    void Awake() {
        GWPawnController.instance = this;
    }

    void Start() {
        this.stats = this.gameObject.GetComponent<GWPawnStats>();
        //this.textmeshPro = this.GetComponentInChildren<TextMeshPro>();
    }

    void Update() {

        if (this.stats.currentHealth <= 0) {
            GameObject.Destroy(this.gameObject);
        }

        this.text.text = "" + this.stats.currentHealth;

        this.LookatMouse();

        Debug.Log(this.stats.currentHealth + "  vs:  " + GWPawnController.instance.stats.currentHealth);
    }

    private void LookatMouse() {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mouseCenterVector = mousePosition - screenCenter;
        Vector3 lookatVector = new Vector3(mouseCenterVector.x, 0, mouseCenterVector.y);
        
        this.movingPawn.transform.LookAt(lookatVector);
    }

    private Vector3 GetScaledDirectionInput() {
        Vector3 directionInput = Vector3.zero;

        if (Input.GetKey(KeyCode.D)) {
            directionInput += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S)) {
            directionInput += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A)) {
            directionInput += Vector3.left;
        }
        if (Input.GetKey(KeyCode.W)) {
            directionInput += Vector3.forward;
        }

        Vector3 normalizedDirectionInput = directionInput.normalized;
        Vector3 scaledDirectionInput = normalizedDirectionInput * this.stats.movementSpeed / 10;

        return scaledDirectionInput;
    }

    void FixedUpdate() {
        this.MovePawn();
    }

    private void MovePawn() {
        this.rb.position += this.GetScaledDirectionInput();
    }
}
