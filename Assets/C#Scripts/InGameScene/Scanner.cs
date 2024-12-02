
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Creature creature = null;
    public Transform target {
        get 
        {
            return _target;
        }
        set 
        {
            _target = value.CompareTag("Ground") ? null : value;
            if (_isScanned && _target != null && objList.Count != 0)
            {
                foreach (var obj in objList)
                {
                    if (_target.GetInstanceID() == obj.GetInstanceID())
                    {
                        _isScanned = true;
                        return;
                    }
                }
                _isScanned = false;
            }
        }
    }
    protected Transform _target = null;
    public bool isScanned{
        get {return _isScanned;}
    }
    protected bool _isScanned = false;

    private List<GameObject> objList = new List<GameObject>();

    private void Awake() {
        if (creature == null)
        {
            creature = transform.parent.GetComponent<Creature>();
            // if (creature == null)
            // {

            // }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_target != null && !_isScanned)
        {
            if (target.GetInstanceID() == other.GetInstanceID())
            {
                _isScanned = true;
            }
        }
        if (other.CompareTag(creature.targetTag))
        {   
            objList.Append(other.transform.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(creature.targetTag))
        {
            objList.Remove(other.transform.gameObject);
        }
    }
}
