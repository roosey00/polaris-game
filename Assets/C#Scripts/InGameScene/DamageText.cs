using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    static public float destroyTime = 1f;

    [SerializeField, ReadOnly] public float damage;

    [SerializeField, ReadOnly] protected BaseDestroyTimer _baseDestroyTimer = null;
    public BaseDestroyTimer BaseDestroyTimer
    {
        get { return _baseDestroyTimer; }
        set { _baseDestroyTimer = value; }
    }

    private void Start()
    {
        var textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = $"-{damage}";
        _baseDestroyTimer = new BaseDestroyTimer(this, destroyTime);        
    }

    private void Update()
    {
        transform.position = GameManager.ChangeY(transform.position, transform.position.y+1);
    }
}
