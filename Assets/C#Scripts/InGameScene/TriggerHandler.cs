using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public HashSet<GameObject> TriggeredObjects { get; private set; } = new HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TriggeredObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TriggeredObjects.Remove(collision.gameObject);
        }
    }
}
