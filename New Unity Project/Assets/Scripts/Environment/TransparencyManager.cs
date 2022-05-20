using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string tagName = "Environment";
    private Transform _lastObstacle;

    // Update is called once per frame
    void Update()
    {
        if(_lastObstacle != null)
        {
            Debug.Log("make visible");
            var lastRenderer = _lastObstacle.GetComponent<Renderer>();
            Color color = lastRenderer.material.color;
            color.a = 1;
            lastRenderer.material.color = color;
            _lastObstacle = null;
        }
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            Debug.Log(selection.tag);
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
    }
}
