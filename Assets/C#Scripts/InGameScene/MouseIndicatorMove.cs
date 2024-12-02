using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicatorMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.ground.MousePos != Vector3.positiveInfinity)
        {
            transform.position = new Vector3(GameManager.instance.ground.MousePos.x, transform.position.y, GameManager.instance.ground.MousePos.z);
        }
    }
}
