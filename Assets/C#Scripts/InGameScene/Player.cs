using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;


public class Player : Creature
{
    new private void Awake()
    {
        base.Awake();

        weapon = new HolySword(this);
        st = new State(100f, 1f, 2f, 1.0f, 2f);
        _targetTag = "Enemy";
    }

    new void Update()
    {
        base.Update();
        if (Input.GetMouseButton(1))
        {
            move.targetPos = GameManager.instance.ground.MousePos;
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.CompareTag(_targetTag))
                {                    
                    SetAttackMode();
                    attackScanner.target = hit.transform;
                    move.attackScanner.target = hit.transform;
                    move.snapedFunc = () => { if (!isAttack) StartCoroutine(TargetAttack(gameObject)); };
                }
                else
                {
                    SetMoveMode();
                    move.snapedFunc = null;
                    move.targetPos = GameManager.instance.ground.MousePos;
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
            move.speed *= 3;
        }
        if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
        {
            move.speed /= 3;
        }
    }


    
}
