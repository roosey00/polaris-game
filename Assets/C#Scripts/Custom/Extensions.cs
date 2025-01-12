using System;

/// <summary>
/// ��ü�� �Լ��� �μ��� �����Ͽ� �۾��� �����ϰ� ����� ��ȯ�մϴ�. <br />
/// ��: someCoroutine.Let(StartCoroutine) => StartCoroutine(someCoroutine)
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
    /// ��ü�� �Լ��� �����Ͽ� Ư�� �۾��� �����ϰ� ����� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T">�Է� ��ü�� Ÿ��</typeparam>
    /// <typeparam name="TResult">��� ��ü�� Ÿ��</typeparam>
    /// <param name="obj">�۾��� ��� ��ü</param>
    /// <param name="block">�۾��� �����ϴ� �Լ�</param>
    /// <returns>�Լ� ������ ���</returns>
    /// <exception cref="ArgumentNullException">block �Լ��� null�� ��</exception>
    public static TResult Let<T, TResult>(this T obj, Func<T, TResult> block)
    {
        if (block == null) throw new ArgumentNullException(nameof(block));
        return block(obj);
    }
}
