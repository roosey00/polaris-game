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
    /// <param name="tick">�� �Ŀ� ����� �ð�</param>
    public void EnqueueTaskOnIdle(Action task, float tick)
    {        
        if (!isProcessing)
        {
            EnqueueTask(task, tick);
        }
    }

    // �۾� �߰�
    public void EnqueueTask(Action task, float tick)
    {
        taskQueue.Enqueue(new TickObject<Action>(task, tick));
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

            yield return new WaitForSeconds(currentTask.Tick); // �� ������ ��� (�񵿱� ó��)
        }

        isProcessing = false;
    }
}