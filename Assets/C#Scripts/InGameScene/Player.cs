using Unity.VisualScripting;
using UnityEngine;


public class Player : Creature
{
    override protected void Awake()
    {
        targetTag = "Enemy";
        equipment.Weapon = new HolySword(gameObject);
        status = new CreatureStatus
        {
            MaxHp = 100f,
            AttackDamage = 1f,
            AttackSpeed = 1.0f,
            AttackRange = 2f
        };
    }

    override protected void Update()
    {
        base.Update();
        
        if (Input.GetMouseButton(1))
        {
            if (movementController.isMove)
            {
                movementController.MoveTo(GameManager.Instance.groundMouseHit.MousePos);
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    if (hit.collider.CompareTag(targetTag))
                    { 
                        attackScanner.Target = hit.transform;
                        movementController.MoveTo(hit.transform.position);
                        //nav.snapedFunc = () => { if (!isAttack) StartCoroutine(TargetAttack(gameObject)); };
                    }
                    else
                    {
                        movementController.isMove = true;
                        movementController.MoveTo(GameManager.Instance.groundMouseHit.MousePos);
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
        if (Input.GetKeyDown(KeyCode.F5))
        {
            transform.position = new Vector3(0, 1, 0);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
        {
            status.Speed *= 3;
        }
        if (UnityEngine.Input.GetKeyUp(KeyCode.LeftShift))
        {
            status.Speed /= 3;
        }
    }


    
}
