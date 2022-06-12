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

    public GWAttackor summoningAttackor;

    public float cooldownTime;
    public float remainingTime;

    public float loadTime;
    public float remainingLoadTime;

    private bool isMovementBlocked;


    private GWInventorySlot attackingInventorySlot;



    public Transform upperBoundTransform;
    public CapsuleCollider characterCollider;

    public GameObject AttackorsContainer;

    void Awake() {
        GWPawnController.instance = this;
    }

    void Start() {
        this.stats = this.gameObject.GetComponent<GWPawnStats>();
        //this.textmeshPro = this.GetComponentInChildren<TextMeshPro>();

        this.remainingLoadTime = this.loadTime;
    }

    void Update() {

        this.text.text = "" + this.stats.currentHealth;

        this.LookatMouse();

        this.ControlAttack();
    }

    void ControlAttack() {

        switch (this.attackState) {

            case GWAttackState.Roaming:


                this.attackingInventorySlot = GWAttackSlotContainer.GetPressedAttackSlot();

                if (!this.IsAttackingAllowed()) {

                    break;
                }


                foreach (Transform child in this.AttackorsContainer.transform) {

                    GWAttackor currentAttackor = child.GetComponent<GWAttackor>();
                    if (currentAttackor.form == this.attackingInventorySlot.uiSpell.spellInstance.form) {
                        this.summoningAttackor = currentAttackor;
                        break;
                    }
                }


                Material weaponMat = this.summoningAttackor.visualAttackor.material;
                Color weaponColor = weaponMat.color;
                weaponColor.a = 0;
                weaponMat.color = weaponColor;


                this.summoningAttackor.gameObject.SetActive(true);

                this.isMovementBlocked = true;

                this.attackState = GWAttackState.Loading;
                

                break;

            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                if (this.remainingLoadTime <= 0) {

                    this.SwitchToAttacking();
                }

                float factor = this.remainingLoadTime / this.loadTime;

                weaponMat = this.summoningAttackor.visualAttackor.material;
                weaponColor = weaponMat.color = this.attackingInventorySlot.Spell.color;
                weaponColor.a = 1 - factor;
                weaponMat.color = weaponColor;

                //this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y,  0);


                break;
            case GWAttackState.Active:

                try {
                    this.summoningAttackor.Activate(this.attackingInventorySlot);
                    this.attackingInventorySlot.SwitchToActive();
                }
                catch (Exception e) {
                    Debug.LogWarning(e.Message);
                }

                this.attackState = GWAttackState.Roaming;
                this.isMovementBlocked = false;
                this.summoningAttackor.gameObject.SetActive(false);

                break;
            default:
                break;

        }
    }



    private void SwitchToAttacking() {

        this.attackState = GWAttackState.Active;
        this.attackingInventorySlot.state = GWInventorySlot.SpellState.ACTIVE;

        this.remainingLoadTime = this.loadTime;
    }

    private bool IsAttackingAllowed() {

        if (this.attackingInventorySlot == null) {
            return false;
        }

        Debug.Log(this.attackingInventorySlot.state);
        if (this.attackingInventorySlot.state != GWInventorySlot.SpellState.READY) {

            Debug.Log("pawncontroller says: Attacking NOT allowed!");

            return false;
        }

        try {
            if (!this.attackingInventorySlot.Spell) {
                Debug.Log("pawncontroller says: Attacking NOT allowed!");
                return false;
            }
        }
        catch (Exception e) {
            Debug.Log("pawncontroller says: Attacking NOT allowed!");
            return false;
        }


        return true;
    }

    public void Hurt(float damage) {

        this.attackState = GWAttackState.Roaming;
        //this.activeAttackor.gameObject.SetActive(false);
        this.isMovementBlocked = false;

        if (this.attackState == GWAttackState.Loading) {
            this.AbortAttack();
        }

        this.stats.currentHealth -= damage;

        if (this.stats.currentHealth <= 0) {
            this.Die();
        }
    }

    private void SetTimes() {
        this.remainingLoadTime = this.loadTime;
    }

    public void AbortAttack() {

        this.attackState = GWAttackState.Roaming;

        this.SetTimes();

        this.attackingInventorySlot.AbortAttack();

        this.summoningAttackor.gameObject.SetActive(false);
        this.summoningAttackor = null;
    }

    public void Die() {
        GameObject.Destroy(this.gameObject);
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
