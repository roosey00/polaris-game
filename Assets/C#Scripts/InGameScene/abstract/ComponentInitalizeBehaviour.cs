using UnityEngine;

abstract public class ComponentInitalizeBehaviour : MonoBehaviour
{
    abstract protected void InitalizeComponent();

    protected void Awake()
    {
        InitalizeComponent();
    }

    protected void Reset()
    {
        InitalizeComponent();
    }
}
