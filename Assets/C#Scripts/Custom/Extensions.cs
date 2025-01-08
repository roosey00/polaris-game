using System;

/// <summary>
/// ��ü�� �Լ��� �μ��� �����Ͽ� �۾��� �����ϰ� ����� ��ȯ�մϴ�. <br />
/// ��: someCoroutine.Let(StartCoroutine) => StartCoroutine(someCoroutine)
/// </summary>
public static class Extensions
{
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
