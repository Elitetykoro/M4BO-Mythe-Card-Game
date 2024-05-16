using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] GameObject point1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    Ray ray;
    RaycastHit hit;
    void Update()
    {
        var pointRenderer = point1.GetComponent<Renderer>();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (hit.collider.gameObject == point1)
        {
            pointRenderer.material.SetColor("_Color", Color.green);
        }
    }
}
