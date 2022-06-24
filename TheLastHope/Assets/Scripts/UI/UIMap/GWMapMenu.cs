using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWMapMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GWDistrictScript[] districts;
    [SerializeField] private Material[] elementalMaterials;
    private Color[] corColors = {new Color(0.9f,0.9f,0.4f), new Color(0.5f,0.5f,0.5f), new Color(0.8f,0.7f,1f), new Color(0.7f,0.5f,1f), new Color(0.5f,0.2f,1f), new Color(0.3f,0f,0.8f)};
    private List<Material> corruptionMaterials;
    private Shader shader;
    private bool open = false;
    private bool corrupt = false;
    // Start is called before the first frame update
    void Start()
    {
        shader = elementalMaterials[0].shader;
        corruptionMaterials = new List<Material>();
        for(int i = 0; i < corColors.Length; i++)
        {
            Material mat = new Material(shader);
            mat.color = corColors[i];
            corruptionMaterials.Add(mat);
        }
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
            foreach(GWDistrictScript district in districts)
            {
                district.gameObject.GetComponent<Renderer>().material = corruptionMaterials[(district.getCorruption() + 1)];
            }
            corrupt = true;
        }else{
            foreach(GWDistrictScript district in districts)
            {
                Material mat;
                switch(district.getElement())
                {
                    case GWEType.EARTH:
                        mat = elementalMaterials[0];
                        break;
                    case GWEType.FIRE:
                        mat = elementalMaterials[1];
                        break;
                    case GWEType.WATER:
                        mat = elementalMaterials[2];
                        break;
                    default:
                        mat = elementalMaterials[3];
                        break;
                }
                district.gameObject.GetComponent<Renderer>().material = mat;
            }
            corrupt = false;
        }
    }
}
