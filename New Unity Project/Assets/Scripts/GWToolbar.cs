using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GWToolbar : MonoBehaviour {
    public GameObject formListGO;
    public GameObject active;
    public GameObject attackForms;
    private List<GameObject> formList = new List<GameObject>();
    private List<GameObject> attackFormList = new List<GameObject>();
    private int formidx = 0;

    void Start() {
        foreach (Transform child in this.formListGO.transform) {
            this.formList.Add(child.gameObject);
        }
        foreach (Transform child in this.attackForms.transform) {
            this.attackFormList.Add(child.gameObject);
        }
        this.attackFormList[formidx].SetActive(true);
    }

    void Update() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0) {
            if (scroll > 0) {
                this.attackFormList[formidx].SetActive(false);
                this.formidx--;
                if (this.formidx < 0) {
                    this.formidx = this.formList.Count - 1;
                }
                this.attackFormList[formidx].SetActive(true);
            }
            else {
                this.attackFormList[formidx].SetActive(false);
                this.formidx++;
                if (this.formidx > this.formList.Count - 1) {
                    this.formidx = 0;
                }
                this.attackFormList[formidx].SetActive(true);
            }
            this.active.transform.position = this.formList[formidx].transform.position;

        }
    }
}
