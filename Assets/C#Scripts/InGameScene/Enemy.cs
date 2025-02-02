using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    public Item[] dropItem;

    override protected void InitalizeComponent()
    {
        base.InitalizeComponent();
        //nav. .slowRotate = true;
        Status = Status.LoadFromJson("Enemy1", "Data/CreatureData");
        HpBarSynchronizer = null;
    }

    private void Start()
    {
        HpBarSynchronizer = Instantiate(GameManager.Instance.FollowHealthBar,
            GameManager.Instance.CanvasUI).GetComponent<HealthBarSynchronizer>();
        HpBarSynchronizer.Owner = this;
        HpBarSynchronizer.UpdateHpBar();
        target = GameManager.Instance.PlayerObj;
        StartCoroutine(TargetFollowCoroutine(1f));        
    }

    new private void Update()
    {
        base.Update();
        HpBarSynchronizer.UpdatePosition();
    }

    IEnumerator TargetFollowCoroutine(float delay)
    {
        while (true)
        {
            _movementController.MoveTo(target.transform.position);
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDisable()
    {
        if (HpBarSynchronizer != null)
        {
            Destroy(HpBarSynchronizer.gameObject);
        }
        //hpBarSynchronizer?.gameObject.Let(hpBar => Destroy(hpBar));
    }
}