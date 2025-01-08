using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Creature creature = null;

    [ReadOnly] public Transform Target
    {
        get => target;
        set => SetTarget(value);
    }

    [ReadOnly] private Transform target = null;
    [ReadOnly] private bool isScanned = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(creature.targetTag))
        {
            if (!objList.Contains(collision.gameObject))
                objList.Add(collision.gameObject);
            UpdateScanState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(creature.targetTag))
        {
            objList.Remove(collision.gameObject);
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