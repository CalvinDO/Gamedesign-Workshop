using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWMapMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GWElementColorTable table;
    [SerializeField] private GWDistrictScript[] districts;
    [SerializeField] private GameObject[] districtMeshes;
    private Color[] corColors = {new Color(0.9f,0.9f,0.4f), new Color(0.5f,0.5f,0.5f), new Color(0.8f,0.7f,1f), new Color(0.7f,0.5f,1f), new Color(0.5f,0.2f,1f), new Color(0.3f,0f,0.8f), new Color(0.8f,0.1f,0.1f)};
    private bool open = false;
    private bool corrupt = false;
    // Start is called before the first frame update
    void Start()
    {
        toggleColors(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            open = !open;
            if(open)
            {
                showCorruption();
            }else{
                hideCorruption();
            }
            ui.SetActive(open);
        }
        if(Input.GetKeyDown(KeyCode.T) && open)
        {
            corrupt = !corrupt;
            toggleColors(corrupt);
        }
    }

    private void showCorruption()
    {
        toggleColors(true);
    }

    private void hideCorruption()
    {
        toggleColors(false);
    }

    private void toggleColors(bool corrupt)
    {
        if(corrupt)
        {
            int triggered = 0;
            foreach(GWDistrictScript district in districts)
            {
                if(district.livingLeader)
                {
                    Renderer disRender = districtMeshes[triggered].GetComponent<Renderer>();
                    Color color = corColors[(corColors.Length - 1)];
                    color.a = 1;
                    disRender.material.color = color;  
                }else{
                    Renderer disRender = districtMeshes[triggered].GetComponent<Renderer>();
                    Color color = corColors[(district.getCorruption() + 1)];
                    color.a = 1;
                    disRender.material.color = color;  
                }
                triggered++;
            }
            corrupt = true;
        }else{
            int triggered = 0;
            foreach(GWDistrictScript district in districts)
            {
                Renderer disRender = districtMeshes[triggered].GetComponent<Renderer>();
                Color color = table.color[(int)district.getElement()];
                color.a = 1;
                disRender.material.color = color;
                triggered++;
            }
            corrupt = false;
        }
    }
}
