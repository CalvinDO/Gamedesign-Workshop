using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public GameObject formListGO;
    public GameObject active;
    public PlayerController player;
    public GameObject attackForms;
    private List<GameObject> formList = new List<GameObject>();
    private List<GameObject> attackFormList = new List<GameObject>();
    private int formidx = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in formListGO.transform)
        {
            formList.Add(child.gameObject);
        }
        foreach (Transform child in attackForms.transform)
        {
            attackFormList.Add(child.gameObject);
        }
        attackFormList[formidx].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            if (scroll > 0)
            {
                attackFormList[formidx].SetActive(false);
                formidx--;
                if (formidx < 0)
                {
                    formidx = formList.Count - 1;
                }
                attackFormList[formidx].SetActive(true);
            }
            else
            {
                attackFormList[formidx].SetActive(false);
                formidx++;
                if (formidx > formList.Count - 1)
                {
                    formidx = 0;
                }
                attackFormList[formidx].SetActive(true);
            }
            active.transform.position = formList[formidx].transform.position;

        }
    }
}
