using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    private List<Enemy> enemyArray = new List<Enemy>();

    public string targetTag = "Enemy";

    public float timer = 5f;
    public float damage = 90f;

    Action<GameObject> startFunc = null;
    Action<GameObject> endFunc = (obj) => {
        RangeAttack ra = obj.GetComponent<RangeAttack>();
        foreach (Enemy e in ra.enemyArray)
        {
            e.GetDamage(ra.damage);
        }
    };
    Action<GameObject> tirggerFunc = null;

    // Start is called before the first frame update
    void Start()
    {
        startFunc(gameObject);
    }

    private void FixedUpdate() {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            endFunc(gameObject);

            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(targetTag))
        {
            tirggerFunc(gameObject);
            enemyArray.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(targetTag))
        {
            enemyArray.Remove(other.GetComponent<Enemy>());
        }
    }
}
