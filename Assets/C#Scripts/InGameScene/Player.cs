using OpenCover.Framework.Model;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : Creature
{
    override protected void Awake()
    {
        base.Awake();
        targetTag = "Enemy";
        equipment.Weapon = new HolySword(gameObject);
    }

    private void Start()
    {
        Status = Status.LoadFromJson("Player", "Data/CreatureData");
    }

    override protected void Update()
    {
        base.Update();
        
        if (Input.GetMouseButton(1))
        {
            if (movementController.IsNavMoveMode)
            {
                movementController.MoveTo(GameManager.Instance.GroundMouseHit.MousePos);
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    if (hit.collider.CompareTag(targetTag))
                    {
                        Debug.Log($"dest : {hit.transform.name}");
                        attackScanner.Target = hit.transform;
                        movementController.MoveTo(hit.transform.position);
                        //nav.snapedFunc = () => { if (!isAttack) StartCoroutine(TargetAttack(gameObject)); };
                    }
                    else
                    {
                        movementController.MoveTo(GameManager.Instance.GroundMouseHit.MousePos);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!equipment.Weapon.skill[0].isLock) equipment.Weapon.skill[0].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!equipment.Weapon.skill[1].isLock) equipment.Weapon.skill[1].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!equipment.Weapon.skill[2].isLock) equipment.Weapon.skill[2].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!equipment.Weapon.skill[3].isLock) equipment.Weapon.skill[3].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!equipment.Weapon.skill[4].isLock) equipment.Weapon.skill[4].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Rolling
            movementController.ForceMove(movementController.MousePointDirNorm, 20f, 0.3f, ()=> {
                movementController.StopMoveInSec(0.01f);
            }
            );
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            transform.position = new Vector3(0, 1, 0);
        }

        //if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    status.Speed *= 3;
        //}
        //if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    status.Speed /= 3;
        //}
    }
}
