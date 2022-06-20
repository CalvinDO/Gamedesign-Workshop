using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWMapMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Material standardMat;
    [SerializeField] private GWDistrictScript[] districts;
    private Color[] corColors = {new Color(0.9f,0.9f,0.4f), new Color(0.5f,0.5f,0.5f), new Color(0.8f,0.7f,1f), new Color(0.7f,0.5f,1f), new Color(0.5f,0.2f,1f), new Color(0.3f,0f,0.8f)};
    private List<Material> corruptionMaterials;
    private Shader shader;
    private bool open = true;
    // Start is called before the first frame update
    void Start()
    {
        shader = standardMat.shader;
        corruptionMaterials = new List<Material>();
        for(int i = 0; i < corColors.Length; i++)
        {
            Material mat = new Material(shader);
            mat.color = corColors[i];
            corruptionMaterials.Add(mat);
        }
        Debug.Log(corruptionMaterials);
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
    }

    private void showCorruption()
    {
        foreach(GWDistrictScript district in districts)
        {
            district.gameObject.GetComponent<Renderer>().material = corruptionMaterials[(district.getCorruption() + 1)];
        }
    }

    private void hideCorruption()
    {
        foreach(GWDistrictScript district in districts)
        {
            district.gameObject.GetComponent<Renderer>().material = standardMat;
        }
    }
}
