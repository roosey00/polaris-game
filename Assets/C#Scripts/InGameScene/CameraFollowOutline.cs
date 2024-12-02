using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowOutline : MonoBehaviour
{
    public Vector3 Size = new Vector3(1.1f, 0.1f, 1.1f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ???? ???? ?????? ????? ???????, ??????? ???????, scale??? ???
       transform.localScale = new Vector3(Mathf.Cos(Camera.main.transform.rotation.x) * Size.x,
           Mathf.Cos(Camera.main.transform.rotation.y) * Size.y,
           Mathf.Cos(Camera.main.transform.rotation.z) * Size.z);
    }
}
