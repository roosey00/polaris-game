using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskQueue : MonoBehaviour
{
    private Queue<TickObject<Action>> taskQueue = new Queue<TickObject<Action>>();
    private bool isProcessing = false;
    public bool IsProcessing => isProcessing;


    /// <summary>
    /// 작업 추가(isProcessing이 false일때만)
    /// </summary>
    /// <param name="task">작업 함수</param>
    /// <param name="tick">그 후에 실행될 시간</param>
    public void EnqueueTaskOnIdle(Action task, float tick)
    {        
        if (!isProcessing)
        {
            EnqueueTask(task, tick);
        }
    }

    // 작업 추가
    public void EnqueueTask(Action task, float tick)
    {
        taskQueue.Enqueue(new TickObject<Action>(task, tick));
        if (!isProcessing)
        {
            StartCoroutine(ProcessQueue());
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

            yield return new WaitForSeconds(currentTask.Tick); // 한 프레임 대기 (비동기 처리)
        }

        isProcessing = false;
    }
}