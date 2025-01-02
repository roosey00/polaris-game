
public class TickObject<T>
{
    public TickObject(T obj, float Tick = 1f)
    {
        this.Tick = Tick;
        this.obj = obj;
    }

    public float Tick;
    public T obj;
}