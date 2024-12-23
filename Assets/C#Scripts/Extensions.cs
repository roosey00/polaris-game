using System;


/// <summary>
/// 다른 변수가 어떠한 함수를 실행하게한다 <br />
/// 예를 들어 someCoroutine.Let(StartCoroutine)이면, <br />
/// StartCoroutine(someCoroutine)과 같은 의미이다.
/// </summary>
public static class Extensions
{
    public static TResult Let<T, TResult>(this T obj, Func<T, TResult> block)
    {
        return block(obj);
    }
}

