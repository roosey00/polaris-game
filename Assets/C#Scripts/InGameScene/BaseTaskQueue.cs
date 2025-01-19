using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseTaskQueue
{
    protected Creature creature;
    [SerializeField, ReadOnly] protected Queue<TickObject<Action>> taskQueue = new Queue<TickObject<Action>>();
    [SerializeField, ReadOnly] protected bool isProcessing = false;
    public bool IsProcessing => isProcessing;

    public BaseTaskQueue(Creature creature)
    {
        this.creature = creature;
    }

    /// <summary>
    /// 작업 추가(isProcessing이 false일때만)
    /// </summary>
    /// <param name="task">작업 함수</param>
    /// <param name="afterWaitTick">그 후에 실행될 시간</param>
    public void EnqueueTaskOnIdle(Action task, float afterWaitTick = 0f)
    {        
        if (!isProcessing)
        {
            EnqueueTask(task, afterWaitTick);
        }
    }

    // 작업 추가
    public void EnqueueTask(Action task, float afterWaitTick = 0f)
    {
        taskQueue.Enqueue(new TickObject<Action>(task, afterWaitTick));
        if (!isProcessing)
        {
            creature.StartCoroutine(ProcessQueue());
        }
    }

    // 작업 실행
    private System.Collections.IEnumerator ProcessQueue()
    {
        isProcessing = true;

        while (taskQueue.Count > 0)
        {
            TickObject<Action> currentTask = taskQueue.Dequeue();
            currentTask.obj?.Invoke(); // 작업 실행

            yield return new WaitForSeconds(currentTask.afterWaitTick); // 한 프레임 대기 (비동기 처리)
        }

        isProcessing = false;
    }
}