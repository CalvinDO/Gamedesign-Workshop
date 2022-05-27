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

    public Vector3 velocity;

    public GWAttackState attackState;

    public GWAttackor activeAttackor;

    public float cooldownTime;
    public float remainingTime;

    public float loadTime;
    public float remainingLoadTime;

    private bool isMovementBlocked;


    private GWInventorySlot attackingInventorySlot;



    public Transform upperBoundTransform;
    public CapsuleCollider characterCollider;



    void Awake() {
        GWPawnController.instance = this;
    }

    void Start() {
        this.stats = this.gameObject.GetComponent<GWPawnStats>();
        //this.textmeshPro = this.GetComponentInChildren<TextMeshPro>();

        this.remainingLoadTime = this.loadTime;
    }

    void Update() {

        if (this.stats.currentHealth <= 0) {
            GameObject.Destroy(this.gameObject);
        }

        this.text.text = "" + this.stats.currentHealth;

        this.LookatMouse();

        this.ControlAttack();
    }

    void ControlAttack() {




        switch (this.attackState) {

            case GWAttackState.Roaming:

                Material weaponMat = this.activeAttackor.visualAttackor.material;
                Color weaponColor = weaponMat.color;
                weaponColor.a = 0;
                weaponMat.color = weaponColor;


                this.attackingInventorySlot = GWAttackSlotContainer.GetPressedAttackSlot();



                if (this.attackingInventorySlot == null) {
                    break;
                }




                if (this.attackingInventorySlot.state != GWInventorySlot.SpellState.READY) {

                    Debug.Log("spell slot is not ready!!!");
                    break;
                }

                try {
                    if (!this.attackingInventorySlot.Spell) {
                        break;
                    }
                }
                catch (Exception e) {
                    break;
                }

                this.attackingInventorySlot.SwitchToActive();

                //this.activeAttackor.gameObject.SetActive(true);

                this.isMovementBlocked = true;

                this.attackState = GWAttackState.Loading;
                this.attackingInventorySlot.state = GWInventorySlot.SpellState.ACTIVE;

                break;

            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                if (this.remainingLoadTime <= 0) {

                    this.attackState = GWAttackState.Attacking;
                    this.attackingInventorySlot.state = GWInventorySlot.SpellState.COOLDOWN;


                    this.remainingLoadTime = this.loadTime;
                }

                float factor = this.remainingLoadTime / this.loadTime;

                weaponMat = this.activeAttackor.visualAttackor.material;
                weaponColor = weaponMat.color = this.attackingInventorySlot.Spell.color;
                weaponColor.a = 1 - factor;
                weaponMat.color = weaponColor;



                //this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y,  0);
                // this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Attacking:

                try {
                    this.activeAttackor.Attack(this.attackingInventorySlot);
                }
                catch (Exception e) {
                    Debug.LogWarning(e.Message);
                }
                this.attackState = GWAttackState.Roaming;

                this.isMovementBlocked = false;

                break;
            default:
                break;

        }
    }

    public void Hurt(float damage) {
        this.stats.currentHealth -= damage;
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


    //float timeLastFrame = (float) Time.timeAsDouble;

    void FixedUpdate() {
        //Debug.Log(Time.timeAsDouble - timeLastFrame);
        //timeLastFrame = (float)Time.timeAsDouble;
        this.MovePawn();
    }

    private void MovePawn() {

        if (this.isMovementBlocked) {
            return;
        }

        this.velocity = this.GetScaledDirectionInput();

        this.rb.position += this.velocity;
    }
}
