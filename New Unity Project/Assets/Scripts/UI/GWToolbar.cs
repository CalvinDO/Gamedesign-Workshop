using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GWToolbar : MonoBehaviour {
    public GameObject formListGO;
    public GameObject active;
    public GameObject attackorsContainer;
    private List<GameObject> formList = new List<GameObject>();
    private List<GWAttackor> attackors = new List<GWAttackor>();
    private int formidx = 0;



    void Start() {

        foreach (Transform child in this.formListGO.transform) {
            this.formList.Add(child.gameObject);
        }
        foreach (Transform child in this.attackorsContainer.transform) {
            this.attackors.Add(child.GetComponent<GWAttackor>());
        }

        //new spell attack system

        return;

        this.attackors[this.formidx].gameObject.SetActive(true);
    }

    void Update() {

        //new spell attack system
        return;
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0) {
            this.attackors[formidx].gameObject.SetActive(false);

            if (scroll > 0) {
                this.formidx--;
            }
            else {
                this.formidx++;
                if (this.formidx > this.formList.Count - 1) {
                    this.formidx = 0;
                }
            }


            if (this.formidx < 0) {
                this.formidx = this.formList.Count - 1;
            }

            this.attackors[this.formidx].gameObject.SetActive(true);
        }

        // if (!this.attackors[formidx].hidden) {

        // }


        this.active.transform.position = this.formList[formidx].transform.position;

        GWPawnController.instance.activeAttackor = this.attackors[this.formidx].GetComponent<GWAttackor>();


    }
}
