using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicatorMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.groundMouseHit.MousePos != Vector3.positiveInfinity)
        {
            transform.position = new Vector3(GameManager.Instance.groundMouseHit.MousePos.x, transform.position.y, GameManager.Instance.groundMouseHit.MousePos.z);
        }
    }
}
