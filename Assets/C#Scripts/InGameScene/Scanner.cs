using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Creature creature = null;

    public Transform Target
    {
        get => target;
        set => SetTarget(value);
    }

    private Transform target = null;
    private bool isScanned = false;

    public bool IsScanned => isScanned;

    private List<GameObject> objList = new List<GameObject>();

    private void Awake()
    {
        if (creature == null)
        {
            creature = transform.parent.GetComponent<Creature>();
            if (creature == null)
            {
                Debug.LogError($"Creature component not found on parent of {gameObject.name}");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(creature.targetTag))
        {
            if (!objList.Contains(other.gameObject))
                objList.Add(other.gameObject);
            UpdateScanState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(creature.targetTag))
        {
            objList.Remove(other.gameObject);
            UpdateScanState();
        }
    }

    private void SetTarget(Transform newTarget)
    {
        if (newTarget == null || newTarget.CompareTag("Ground"))
        {
            target = null;
            isScanned = false;
            return;
        }

        target = newTarget;
        UpdateScanState();
    }

    private void UpdateScanState()
    {
        if (target != null)
        {
            isScanned = objList.Any(obj => obj.GetInstanceID() == target.GetInstanceID());
        }
        else
        {
            isScanned = false;
        }
    }
}