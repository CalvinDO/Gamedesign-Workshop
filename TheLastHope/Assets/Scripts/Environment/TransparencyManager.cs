using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyManager : MonoBehaviour {
    // Start is called before the first frame update
    //[SerializeField] private string tagName = "Environment";

    [SerializeField] private string[] layer;

    private List<Renderer> lastRenderers;
    public List<Material> originalMaterials;

    public Material transparentHouseMaterial;


    void Start() {
        this.lastRenderers = new List<Renderer>();
        this.originalMaterials = new List<Material>();
    }
    // Update is called once per frame
    void Update() {


        foreach (Renderer renderer in this.lastRenderers) {
            /*
            Color color = renderer.material.color;
            //renderer.material.re
            color.a = 1;

            renderer.material.color = color;
            */
            renderer.material = this.originalMaterials[this.lastRenderers.IndexOf(renderer)];
        }


        this.lastRenderers = new List<Renderer>();
        this.originalMaterials = new List<Material>();


        //var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //RaycastHit hit;


        Debug.DrawLine(Camera.main.transform.position, GWPawnController.instance.transform.position, Color.red);

        int layerMask = LayerMask.GetMask(layer);


        Vector3 direction = GWPawnController.instance.transform.position - Camera.main.transform.position;

        Collider[] collidedObjects = Physics.OverlapCapsule(Camera.main.transform.position, GWPawnController.instance.transform.position, GWPawnController.instance.characterCollider.radius, layerMask);


        foreach (Collider collider in collidedObjects) {
            Renderer selectionRenderer = collider.gameObject.GetComponent<Renderer>();

            if (selectionRenderer) {

                /*
                Color color = selectionRenderer.material.color;
                color.a = 0;
                selectionRenderer.material.color = color;
                */

                this.lastRenderers.Add(selectionRenderer);
                this.originalMaterials.Add(selectionRenderer.material);

                selectionRenderer.material = this.transparentHouseMaterial;
            }
        }




        /*
        if (Physics.Linecast(Camera.main.transform.position, GWPawnController.instance.transform.position, out hit, layerMask)) {
            Transform selection = hit.transform;

            Renderer selectionRenderer = selection.gameObject.GetComponent<Renderer>();

            if (selectionRenderer) {

                Debug.Log("make invisible");
                Color color = selectionRenderer.material.color;
                color.a = 0;
                selectionRenderer.material.color = color;

                this.lastRenderers.Add(selectionRenderer);
            }



            
            if (selection.CompareTag(tagName))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer)
                {
                    Debug.Log("make invisible");
                    Color color = selectionRenderer.material.color;
                    color.a = 0;
                    selectionRenderer.material.color = color;
                }

                _lastObstacle = selection;
            }
          
        }
              */
    }
}
