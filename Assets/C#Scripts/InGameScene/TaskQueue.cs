using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskQueue : MonoBehaviour
{
    private Queue<TickObject<Action>> taskQueue = new Queue<TickObject<Action>>();
    private bool isProcessing = false;
    public bool IsProcessing => isProcessing;


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
            StartCoroutine(ProcessQueue());
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