using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterController : MonoBehaviour
{
    [Range(0, 1)]
    public float movementSpeed;
    public Rigidbody rb;

    public float health = 100;
    public Text text;


    void Start()
    {
        //this.textmeshPro = this.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update() {
       
        if (this.health <= 0) {
            GameObject.Destroy(this.gameObject);
        }

        this.text.text = "" + health;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        this.rb.transform.LookAt(mousePosition);
        Debug.Log(Input.mousePosition);

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
        Vector3 scaledDirectionInput = normalizedDirectionInput * movementSpeed;

        return scaledDirectionInput;
    }

    void FixedUpdate() {
        this.MoveCharacter();
    }

    private void MoveCharacter() {
        this.rb.position += this.GetScaledDirectionInput();
    }
}
