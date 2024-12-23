using System;


/// <summary>
/// �ٸ� ������ ��� �Լ��� �����ϰ��Ѵ� <br />
/// ���� ��� someCoroutine.Let(StartCoroutine)�̸�, <br />
/// StartCoroutine(someCoroutine)�� ���� �ǹ��̴�.
/// </summary>
public static class Extensions
{
    public static TResult Let<T, TResult>(this T obj, Func<T, TResult> block)
    {
        return block(obj);
    }
}

