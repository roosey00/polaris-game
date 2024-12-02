using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

// use Start, FixedUpdate
// component : Rigidbody
public class Moveable : MonoBehaviour
{
    protected Rigidbody rigid = null;
    public Scanner attackScanner = null;
    public Vector3 targetPos
    {
        get { return _targetPos; }
        set { 
            float fixedY = _targetPos.y;
            _targetPos = value;
            snaped = false;
            _targetPos.y = fixedY; 
        }
    }
    private Vector3 _targetPos = Vector3.positiveInfinity;

    // Setting Value
    public float snapGap = 0.1f;
    public float speed = 3f;
    public float speedRate = 1f;

    public float rotateSnapGap = 5f;
    public float rotateSpeed = 100f;

    public bool slowRotate = false;
    public bool isPause = false;
    //public bool isForceMove = false;
    public bool _isForceMove = false;

    public Action snapedFunc;

    public bool snaped
    {
        get { return _snaped; }
        set { _snaped=value; 
            if (value)
            {
                if (snapedFunc != null)
                    snapedFunc();
            }
        }
        
    }
    protected bool _snaped = true;


    public IEnumerator ForceMove(Vector3 dir, float speed, float moveTime, float curvePerSec = 0.0f, float maxSpeed = float.PositiveInfinity)
    {
        if (!_isForceMove)
        {
            dir = Vector3Modifier.ChangeY(dir, 0);
            _isForceMove = true;
            while (moveTime > 0f)
            {
                speed = Math.Min(speed * (1 + curvePerSec * Time.fixedDeltaTime), maxSpeed);
                transform.Translate(dir.normalized * speed * Time.fixedDeltaTime, Space.World);
                moveTime -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            transform.Translate(Vector3.zero, Space.World);
            targetPos = transform.position + transform.forward.normalized * snapGap * 0.1f;
            _isForceMove = false;
        }
        yield return null;
    }

    public void Awake()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
        
        if (attackScanner == null)
        {
            attackScanner = transform.Find("AttackRangeScanner").GetComponent<Scanner>();
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        _targetPos = transform.position;
    }

    public void FixedUpdate()
    {
        if  (attackScanner.target != null)
        {
            targetPos = attackScanner.target.position;
        }

        if (!isPause && !_isForceMove && !(targetPos == Vector3.positiveInfinity))
        {
            Vector3 targetVec = Vector3Modifier.ChangeY(targetPos-transform.position, 0).normalized;

            /// ������ �κ�
            if (!snaped)
            {
                float nDist = Vector3.Distance(Vector3Modifier.ChangeY(transform.position, 0), Vector3Modifier.ChangeY(targetPos, 0));
                if (nDist > snapGap)
                {
                    if (nDist - speed * speedRate * Time.fixedDeltaTime < snapGap || attackScanner.isScanned)
                    {
                        transform.position = targetPos;
                        targetPos += transform.forward * snapGap * 0.1f;
                        snaped = true;
                    }
                    else
                    {
                        transform.Translate(targetVec * speed * speedRate * Time.fixedDeltaTime, Space.World);
                    }
                }
            }

            float theta = Mathf.Atan2(targetVec.x, targetVec.z) / Mathf.PI * 180f;
            if (theta < 0)
            {
                theta += 360f;
            }
            // ȸ�� �κ�
            if (theta != transform.eulerAngles.y)
            {
                if (slowRotate)
                {
                    if (Mathf.Abs(transform.eulerAngles.y - theta) - rotateSpeed * Time.fixedDeltaTime < rotateSnapGap)
                        transform.localRotation = Quaternion.Euler(0, theta, 0);
                    else if (theta - 180f <= 0)
                    {
                        transform.Rotate(new Vector3(0, ((transform.eulerAngles.y <= theta || theta + 180 <= transform.eulerAngles.y) ? 1 : -1) * rotateSpeed, 0) * Time.fixedDeltaTime);
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, ((transform.eulerAngles.y <= theta - 180f || theta <= transform.eulerAngles.y) ? -1 : 1) * rotateSpeed, 0) * Time.fixedDeltaTime);
                    }
                }
                else
                {
                    transform.LookAt(targetPos);// = Quaternion.Euler(0, theta, 0);
                }
            }      
        }
    }
}