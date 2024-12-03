using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;


public class Player : Creature
{
    new private void Start()
    {
        base.Start();

        weapon = new HolySword(this);
        //st = new State(100f, 1f, 2f, 1.0f, 2f);
        //TargetTag = "Enemy";
    }

    new void Update()
    {
        base.Update();
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            nav.SetDestination(GameManager.Instance.groundMouseHit.MousePos);
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.CompareTag(targetTag))
                {                    
                    SetAttackMode();
                    attackScanner.target = hit.transform;
                    nav.SetDestination(hit.transform.position);
                    //nav.snapedFunc = () => { if (!isAttack) StartCoroutine(TargetAttack(gameObject)); };
                }
                else
                {
                    SetMoveMode();
                    nav.SetDestination(GameManager.Instance.groundMouseHit.MousePos);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!weapon.skill[0].isLock) weapon.skill[0].obj(this);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!weapon.skill[1].isLock) weapon.skill[1].obj(this);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!weapon.skill[2].isLock) weapon.skill[2].obj(this);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!weapon.skill[3].isLock) weapon.skill[3].obj(this);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!weapon.skill[4].isLock) weapon.skill[4].obj(this);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            transform.position = new Vector3(0, 1, 0);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
        {
            nav.speed *= 3;
        }
        if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
        {
            nav.speed /= 3;
        }
    }


    
}
