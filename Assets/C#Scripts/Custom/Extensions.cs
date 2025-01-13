using System;

/// <summary>
/// 객체를 함수의 인수로 전달하여 작업을 수행하고 결과를 반환합니다. <br />
/// 예: someCoroutine.Let(StartCoroutine) => StartCoroutine(someCoroutine)
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Extension method to perform an action if the object is not null.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to check.</param>
    /// <param name="action">The action to execute if the object is not null.</param>
    public static void Let<T>(this T obj, Action<T> action)
    {
        if (obj != null)
        {
            action(obj);
        }
    }

    /// <summary>
    /// 객체를 함수에 전달하여 특정 작업을 수행하고 결과를 반환합니다.
    /// </summary>
    /// <typeparam name="T">입력 객체의 타입</typeparam>
    /// <typeparam name="TResult">결과 객체의 타입</typeparam>
    /// <param name="obj">작업의 대상 객체</param>
    /// <param name="block">작업을 수행하는 함수</param>
    /// <returns>함수 실행의 결과</returns>
    /// <exception cref="ArgumentNullException">block 함수가 null일 때</exception>
    public static TResult Let<T, TResult>(this T obj, Func<T, TResult> block)
    {
        if (block == null) throw new ArgumentNullException(nameof(block));
        return block(obj);
    }
}
