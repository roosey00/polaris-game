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
        hpBarSynchronizer = null;
    }

    private void Start()
    {
        hpBarSynchronizer = Instantiate(GameManager.Instance.FollowHealthBar, GameManager.Instance.CanvasUI).GetComponent<HealthBarSynchronizer>();
        hpBarSynchronizer.Owner = this;
        target = GameManager.Instance.PlayerObj;
        StartCoroutine(TargetFollowCoroutine(1f));
    }

    new private void Update()
    {
        base.Update();
        hpBarSynchronizer.UpdatePosition();
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
        hpBarSynchronizer.gameObject?.Let(hpBar => Destroy(hpBar));
    }
}