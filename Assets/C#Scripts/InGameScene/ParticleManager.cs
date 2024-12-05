using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ParticleName
{
    None, Click
}

public class ParticleManager : Singleton<ParticleManager>
{
    public GameObject ClickParticle;

    // Start is called before the first frame update
    void Start()
    {
        if (ClickParticle == null)
        {
            ClickParticle = Resources.Load<GameObject>("Prefab/Click Particle");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MakeParticle(ParticleName.Click, GameManager.Instance.groundMouseHit.MousePos);
        }
    }

    public void MakeParticle(ParticleName name, Vector3 pos)
    {
        GameObject obj;
        switch (name)
        {
            case ParticleName.Click:
                obj = Instantiate(ClickParticle, pos, Quaternion.identity, transform);
                break;
            case ParticleName.None:
            default:
                Debug.LogError("None Particle Called!");
                return;
        }
        StartCoroutine(ParticleDestroy(obj));
    }
    private IEnumerator ParticleDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(obj.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
        Destroy(obj);
    }
}
