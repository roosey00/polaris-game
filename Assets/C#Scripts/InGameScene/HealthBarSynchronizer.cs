using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class HealthBarSynchronizer : ComponentInitalizeBehaviour
{
    [ReadOnly(true)] public Creature Owner = null;
    [SerializeField, ReadOnly] protected Slider HpBarSlider = null;
    [SerializeField, ReadOnly] protected TextMeshProUGUI HpBarText = null;

    public Vector3 GapFromCenter = new Vector3(0, 100, 0);

    override protected void InitalizeComponent()
    {
        //Owner ??= GameObject.Find("ForUseXYZ").transform.Find("Player").GetComponent<Player>();
        HpBarSlider ??= GetComponent<Slider>();
        HpBarText ??= transform.Find("Text (TMP)")?.GetComponent<TextMeshProUGUI>();
        if (!(Owner is null) && !(HpBarText is null))
        {
            UpdateHpBar();
        }
    }

    public void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(Owner.transform.position) + GapFromCenter;
    }

    public void UpdateHpBar()
    {
        HpBarSlider.value = Owner.Status.CurrentHp;
        HpBarSlider.maxValue = Owner.Status.MaxHp;
        HpBarText.text = $"{HpBarSlider.value} / {HpBarSlider.maxValue}";
    }
}
