using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyManager : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private string tagName = "Environment";

    [SerializeField] private string[] layer;

    private List<Renderer> lastRenderers;


    void Start() {
        this.lastRenderers = new List<Renderer>();
    }
    // Update is called once per frame
    void Update() {


        foreach (Renderer renderer in this.lastRenderers) {

            Debug.Log("make visible");
            Color color = renderer.material.color;
            color.a = 1;

            renderer.material.color = color;
        }


        this.lastRenderers = new List<Renderer>();



        //var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;


        Debug.DrawLine(Camera.main.transform.position, GWPawnController.instance.transform.position, Color.red);

        int layerMask = LayerMask.GetMask(layer);


        Vector3 direction = GWPawnController.instance.transform.position - Camera.main.transform.position;

        Collider[] collidedObjects = Physics.OverlapCapsule(Camera.main.transform.position, GWPawnController.instance.transform.position, GWPawnController.instance.characterCollider.radius, layerMask);


        foreach (Collider collider in collidedObjects) {
            Renderer selectionRenderer = collider.gameObject.GetComponent<Renderer>();

            if (selectionRenderer) {

                Debug.Log("make invisible");
                Color color = selectionRenderer.material.color;
                color.a = 0;
                selectionRenderer.material.color = color;

                this.lastRenderers.Add(selectionRenderer);
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
