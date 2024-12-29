
public class LockObject<T>
{
    public LockObject(T obj, bool isLock = true)
    {
        this.isLock = isLock;
        this.obj = obj;
    }

    public bool isLock;
    public T obj;
}