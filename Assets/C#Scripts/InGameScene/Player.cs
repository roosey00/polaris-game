using OpenCover.Framework.Model;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Creature
{
    public Equipment equipment;

    private string CharacterName = "Character";

    override protected void Awake()
    {
        base.Awake();
        characterAnimator ??= transform.Find(CharacterName).GetComponent<Animator>();
        targetTag = "Enemy";
        equipment ??= new Equipment(this);
        equipment.Weapon = new HolySword(gameObject);
    }

    private void Start()
    {
        Status = Status.LoadFromJson("Player", "Data/CreatureData");
    }

    override protected void Update()
    {
        base.Update();
        KeyControlUpdate();
        AnimationValueUpdate();
    }

    protected void KeyControlUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            if (_movementController.IsNavMove)
            {
                _movementController.MoveTo(GameManager.Instance.GroundMouseHit.MousePos);

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    if (hit.collider.CompareTag(targetTag))
                    {
                        Debug.Log($"dest : {hit.transform.name}");
                        //attackScanner.Target = hit.transform;
                        _movementController.MoveTo(hit.transform.position);
                        //nav.snapedFunc = () => { if (!isAttack) StartCoroutine(TargetAttack(gameObject)); };
                    }
                    else
                    {
                        _movementController.MoveTo(GameManager.Instance.GroundMouseHit.MousePos);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!equipment.Weapon.Skill[0].isLock) equipment.Weapon.Skill[0].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!equipment.Weapon.Skill[1].isLock) equipment.Weapon.Skill[1].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!equipment.Weapon.Skill[2].isLock) equipment.Weapon.Skill[2].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!equipment.Weapon.Skill[3].isLock) equipment.Weapon.Skill[3].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!equipment.Weapon.Skill[4].isLock) equipment.Weapon.Skill[4].obj(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rolling();
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

    protected void AnimationValueUpdate()
    {
        characterAnimator.SetBool("isWalk", _movementController.isMove);
    }

    protected void Rolling()
    {
        taskQueue.EnqueueTaskOnIdle(() => _movementController.ForceMove(_movementController.MousePointDirNorm, 20f, 0.3f), 0.3f);
        taskQueue.EnqueueTask(() => _movementController.StopMoveInSec(0.01f), 0.01f);
    }
}