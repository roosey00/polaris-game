using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Vector3 MousePos;
    
    private Ray ray;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        MousePos = Physics.Raycast(ray, out hit) ?
            hit.point :
            Vector3.positiveInfinity;
    }
}
