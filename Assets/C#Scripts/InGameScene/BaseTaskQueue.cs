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
    /// �۾� �߰�(isProcessing�� false�϶���)
    /// </summary>
    /// <param name="task">�۾� �Լ�</param>
    /// <param name="afterWaitTick">�� �Ŀ� ����� �ð�</param>
    public void EnqueueTaskOnIdle(Action task, float afterWaitTick = 0f)
    {        
        if (!isProcessing)
        {
            EnqueueTask(task, afterWaitTick);
        }
    }

    // �۾� �߰�
    public void EnqueueTask(Action task, float afterWaitTick = 0f)
    {
        taskQueue.Enqueue(new TickObject<Action>(task, afterWaitTick));
        if (!isProcessing)
        {
            creature.StartCoroutine(ProcessQueue());
        }
    }

    // �۾� ����
    private System.Collections.IEnumerator ProcessQueue()
    {
        isProcessing = true;

        while (taskQueue.Count > 0)
        {
            TickObject<Action> currentTask = taskQueue.Dequeue();
            currentTask.obj?.Invoke(); // �۾� ����

            yield return new WaitForSeconds(currentTask.afterWaitTick); // �� ������ ��� (�񵿱� ó��)
        }

        isProcessing = false;
    }
}