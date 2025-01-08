
public class TickObject<T>
{
    public TickObject(T obj, float Tick = 1f)
    {
        this.afterWaitTick = Tick;
        this.obj = obj;
    }

    public float afterWaitTick;
    public T obj;
}